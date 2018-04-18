#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

#endregion

namespace Assets.Scripts.AI
{
    public class ActiveActor : ActorBase
    {
        #region Fields

        #region  Private Fields

        private ActorBase _currentInteractee;
        private Interaction _currentInteraction;

        [SerializeField]
        [Range(0, 1)]
        private float _deviationChance = 0.02f;

        [SerializeField]
        private List<InteractionEntry> _interactions;

        private bool _isMoving;

        #endregion

        #region  Public Fields

        public readonly WeightedList<Interaction> Interactions = new WeightedList<Interaction>(new List<WeightWrapper<Interaction>>());
        public MovementType MovementType;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public InteractionPoint CurrentInteractionPoint { get; set; }

        #endregion

        #region Virtual Properties

        public virtual HashSet<ActorBase> NearbyActors { get; protected set; }

        #endregion

        #region Overriding Properties

        public override InteracteeType InteracteeType
        {
            get { return InteracteeType.Active; }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void MoveToInteract(Interaction interaction, InteractionPoint point, ActorBase other)
        {
            StartCoroutine(MoveCoroutine(interaction, point, other));
        }

        private bool CheckInteraction(Interaction interaction)
        {
            if(interaction.Type == InteracteeType.None)
                return true;
            var actors = NearbyActors.Where(x => !x.IsInteracting && x.InteracteeType == interaction.Type && x.AllowedInteractions.Contains(interaction.Name)).ToList();
            return actors.Any();
        }

        private IEnumerator MoveCoroutine(Interaction interaction, InteractionPoint point, ActorBase other)
        {
            point.IsFree = false;
            CurrentInteractionPoint = point;
            _isMoving = true;
            var agent = GetComponent<NavMeshAgent>();
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForFixedUpdate();
            //yield return new WaitForEndOfFrame();
            int area = agent.GetComponent<NavMeshAgent>().areaMask;
            NavMeshHit navMeshPos;
            var isNavMeshHit = NavMesh.SamplePosition(agent.transform.position, out navMeshPos, 1, area);
            agent.destination = point.Position;
            yield return new WaitForSeconds(0.5f);
            //yield return new WaitUntil(() => !GetComponent<NavMeshAgent>() || !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || Math.Abs(agent.velocity.sqrMagnitude) < float.Epsilon));
            yield return new WaitUntil(() => IsMoveFinished(agent));
            //if(interaction.ShouldRotate)
            //    transform.rotation = point.Rotation;
            interaction.StartEvent(this, other);
            _isMoving = false;
        }

        private bool IsMoveFinished(NavMeshAgent agent)
        {
            if (!GetComponent<NavMeshAgent>())
                return true;
            if (agent.pathPending)
                return false;
            if (!agent.hasPath /*|| Math.Abs(agent.velocity.sqrMagnitude) < float.Epsilon*/)
                return true;
            return agent.remainingDistance <= agent.stoppingDistance;
        }
        

        private ActorBase SelectActor(Interaction interaction)
        {
            var actors = NearbyActors.Where(x => !x.IsInteracting && x.InteracteeType == interaction.Type && x.AllowedInteractions.Contains(interaction.Name)).ToList();
            if(!actors.Any())
                return null;
            return actors.SelectRandomItem();
        }

        #endregion

        #region Virtual Methods

        public virtual void EndCurrentInteraction()
        {
            if(IsInteracting)
            {
                EndInteraction(_currentInteraction, _currentInteractee);
                if(_currentInteractee is ActiveActor)
                    (_currentInteractee as ActiveActor).EndCurrentInteraction();
            }
        }

        public virtual void EndInteraction(Interaction interaction, ActorBase other)
        {
            if(interaction != null)
                interaction.End(this, other);
            CurrentInteractionPoint = null;
            _currentInteractee = null;
            _currentInteraction = null;
        }

        public virtual void EndInteraction(string interactionName)
        {
            var interaction = Interactions.Weights.Select(x => x.Item).FirstOrDefault(x => x.Name == interactionName);
            if(interaction != null)
                interaction.End(this, null);
        }

        public virtual IEnumerator IneractionEnumerator(Interaction interaction, ActorBase other)
        {
            yield return new WaitForSeconds(interaction.Duration + Random.Range(-interaction.Deviation, interaction.Deviation));
            EndInteraction(interaction, other);
        }

        public virtual void StartInteraction(Interaction interaction, ActorBase other)
        {
            _currentInteraction = interaction;
            _currentInteractee = other;
            interaction.Start(this, other);
        }

        public virtual void WaitForInteractionEnd()
        {
            if(_currentInteraction != null && _currentInteraction.Duration > 0)
                StartCoroutine(IneractionEnumerator(_currentInteraction, _currentInteractee));
        }

        protected virtual void Awake()
        {
            Interactions.Weights.AddRange(_interactions.Select(x => new WeightWrapper<Interaction>(new Interaction {Name = x.Name, Deviation = x.Deviation, Duration = x.Duration, Type = x.Type, ShouldRotate = x.ShouldRotate, NextInteraction = x.NextInteraction}, x.Weight)));
            NearbyActors = new HashSet<ActorBase>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var actor = other.GetComponent<ActorBase>();
            if(actor)
                NearbyActors.Add(actor);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            var actor = other.GetComponent<ActorBase>();
            if(actor)
                NearbyActors.Remove(actor);
        }

        protected virtual void Update()
        {
            if(IsInteracting || _isMoving && !(_currentInteractee is PassiveActor && ((PassiveActor) _currentInteractee).IsExclusive))
                return;
            if(_isMoving && (Random.value > _deviationChance || Math.Abs(_deviationChance) < float.Epsilon))
                return;
            var interaction = Interactions.GetRandomFilteredItem(CheckInteraction);
            if(interaction == null)
                return;
            var actor = SelectActor(interaction);
            StartInteraction(interaction, actor);
        }
        
        #endregion

        #endregion
    }
}