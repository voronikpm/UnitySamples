using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Collider))]
    public abstract class ActorBase : MonoBehaviour
    {
        public virtual bool IsInteracting { get; set; }
        public virtual InteracteeType InteracteeType { get; protected set; }
        public List<string> AllowedInteractions;
    }
}