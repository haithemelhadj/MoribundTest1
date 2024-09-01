//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SharedEnemyType : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedEnemyType : SharedVariable<EnemyType>
    {
        public static implicit operator SharedEnemyType(EnemyType value) { return new SharedEnemyType { Value = value }; }
    }

    //public class SharedPatrolType : SharedVariable<enum>
    //{
    //    public static implicit operator SharedEnemyType(EnemyType value) { return new SharedEnemyType { Value = value }; }
    //}
}