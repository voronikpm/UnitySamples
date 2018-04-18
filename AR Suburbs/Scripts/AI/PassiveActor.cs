using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class PassiveActor : ActorBase
    {
        public override InteracteeType InteracteeType
        {
            get { return InteracteeType.Passive; }
        }

        public List<InteractionPoint> InteractionPoints;
        public bool IsExclusive = true;

        public override bool IsInteracting
        {
            get
            {
                //return InteractionPoints.Any() ? IsExclusive && InteractionPoints.All(x => !x.IsFree) : IsExclusive && base.IsInteracting;
                if(IsExclusive)
                {
                    return InteractionPoints.Any() ? InteractionPoints.All(x => !x.IsFree) : base.IsInteracting;
                }
                return false;
            }
            set { base.IsInteracting = value; }
        }
    }
}