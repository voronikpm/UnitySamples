using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Helpers
{
    [Serializable]
    public class InteractionEntry
    {
        public string Name;
        public int Weight;
        public float Duration;
        public float Deviation;
        public InteracteeType Type;
        public bool ShouldRotate;
        public string NextInteraction;
    }
}