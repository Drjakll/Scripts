using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Functions
{
    public enum Stuns { NotStunned, Hit, GuardAttacked, GuardDestroyed, FailHit, KnockBack}
    public delegate void Reactions(Player Target);
}
