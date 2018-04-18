using System;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Collider))]
    public class TouchableObject : GameObjectBase
    {
        public virtual Action TouchAction { get; protected set; }
    }
}