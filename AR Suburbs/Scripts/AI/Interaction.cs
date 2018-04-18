#region Using Directives

using System;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Assets.Scripts.AI
{
    [Serializable]
    public class Interaction
    {
        #region Fields

        #region  Public Fields

        public float Deviation;
        public float Duration;
        public string Name;
        public bool ShouldRotate = true;
        public InteracteeType Type;
        public string NextInteraction;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void End(ActorBase left, ActorBase right)
        {
            if(string.IsNullOrEmpty(Name))
                return;
            var leftFsm = left.GetComponent<PlayMakerFSM>();
            if(leftFsm)
                leftFsm.GetComponent<PlayMakerFSM>().Fsm.Event(string.Format("End_{0}", Name));
            left.IsInteracting = false;
            if(Type != InteracteeType.None && right && right.InteracteeType == Type && right.AllowedInteractions.Contains(Name))
            {
                var rightFsm = right.GetComponent<PlayMakerFSM>();
                if(rightFsm)
                    rightFsm.Fsm.Event(string.Format("End_{0}", Name));
                right.IsInteracting = false;
                if(Type == InteracteeType.Passive)
                {
                    var actor = left as ActiveActor;
                    if(actor && actor.CurrentInteractionPoint)
                    {
                        actor.CurrentInteractionPoint.IsFree = true;
                        actor.CurrentInteractionPoint = null;
                    }
                }
            }
            if(!string.IsNullOrEmpty(NextInteraction) && (left as ActiveActor).Interactions.Weights.Any(x => x.Item.Name == NextInteraction))
            {
                var actor = left as ActiveActor;
                var interaction = actor.Interactions.GetFilteredList(x => x.Name == NextInteraction).FirstOrDefault();
                if(interaction != null)
                {
                    var possibleActors = actor.NearbyActors.OfType<PassiveActor>().Where(x => !x.IsInteracting && x.AllowedInteractions.Contains(NextInteraction)).ToList();
                    if(!possibleActors.Any())
                        return;
                    var selectedActor = possibleActors.MinBy(x => Math.Abs((actor.transform.position - x.transform.position).magnitude));
                    if(selectedActor)
                        actor.StartInteraction(interaction.Item,selectedActor);
                }

            }

        }

        public void Start(ActorBase left, ActorBase right)
        {
            if(string.IsNullOrEmpty(Name))
                return;
            if(left.IsInteracting)
                return;
            if(Type == InteracteeType.None)
                StartEvent(left);
            if(Type != InteracteeType.None && right && !right.IsInteracting && right.InteracteeType == Type)
            {
                //left.IsInteracting = true;
                var passive = right as PassiveActor;
                //if(passive.IsExclusive)
                right.IsInteracting = true; //TODO check if necessary
                if(Type == InteracteeType.Passive)
                    if(!passive.InteractionPoints.Any())
                    {
                        StartEvent(left, right);
                    }
                    else
                    {
                        var freeInteractionPoints = passive.InteractionPoints.Where(x => !passive.IsExclusive || x.IsFree).ToList();
                        if(freeInteractionPoints.Any())
                        {
                            var point = freeInteractionPoints.SelectRandomItem();
                            (left as ActiveActor).MoveToInteract(this, point, right);
                        }
                    }
                else
                    StartEvent(left,right);
            }
        }

        public void StartEvent(ActorBase actor)
        {
            var agent = actor.GetComponent<NavMeshAgent>();
            var fsm = actor.GetComponent<PlayMakerFSM>();
            if(agent && agent.enabled)
                agent.isStopped = true;
            if(fsm)
                fsm.Fsm.Event(string.Format("Start_{0}", Name));
            actor.IsInteracting = true;
        }

        public void StartEvent(ActorBase active, ActorBase passive)
        {
            //active.transform.parent = passive.transform;
            if(ShouldRotate && Type != InteracteeType.None)
            {
                if(Type == InteracteeType.Active)
                {
                    active.transform.LookAt(passive.transform);
                    passive.transform.LookAt(active.transform);
                }
                else
                    active.transform.rotation = (active as ActiveActor).CurrentInteractionPoint.Rotation;
            }
            StartEvent(active);
            StartEvent(passive);
            (active as ActiveActor).WaitForInteractionEnd();
        }

        #endregion

        #endregion
    }
}