#region Using Directives

using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class NavAgentBase : MonoBehaviour
    {
        #region Fields

        #region  Public Fields

        public Transform Destination;

        #endregion

        #endregion

        #region Properties

        #region Virtual Properties

        public virtual bool IsPaused
        {
            get { return _MeshAgent.isStopped; }
            set { _MeshAgent.isStopped = value; }
        }

        protected virtual NavMeshAgent _MeshAgent { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Virtual Methods

        protected virtual void Awake()
        {
            _MeshAgent = GetComponent<NavMeshAgent>();
            _MeshAgent.destination = Destination.position;
        }

        #endregion

        #endregion
    }
}