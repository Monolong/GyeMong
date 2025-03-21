using System.Collections;
using playerCharacter;
using Unity.VisualScripting;
using UnityEngine;

namespace Creature.Minion.Slime
{
    public class Slime : SlimeBase
    {
        public class  SlimeRangedAttackState : RangedAttackState { }
        public class SlimeMeleeAttackState : MeleeAttackState {}
        public class DieState : SlimeDieState { }
        public class MoveState : SlimeMoveState { }
    }
}
