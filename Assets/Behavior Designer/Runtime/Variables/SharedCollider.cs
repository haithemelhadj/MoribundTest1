using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedCollider : SharedVariable<Collider>
    {
        public static implicit operator SharedCollider(Collider value) { return new SharedCollider { mValue = value }; }
    }
    public class SharedCapsulCollider2D : SharedVariable<CapsuleCollider2D>
    {
        public static implicit operator SharedCapsulCollider2D(CapsuleCollider2D value) { return new SharedCapsulCollider2D { mValue = value }; }
    }

}