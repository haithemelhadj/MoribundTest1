using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class isTargetInDistance : Conditional
{
    public float fieldOfViewAngle;
    public string targetTag;
    public float detectionDistance;
    public SharedTransform target;

    private Transform[] possibleTargets;

    public override void OnAwake()
    {
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        possibleTargets = new Transform[targets.Length];
        for (int i = 0; i < targets.Length; ++i)
        {
            possibleTargets[i] = targets[i].transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i < possibleTargets.Length; ++i)
        {

            if (Vector2.Distance(possibleTargets[i].position, transform.position) < detectionDistance)
                if (WithinSight(possibleTargets[i], fieldOfViewAngle))
                {
                    target.Value = possibleTargets[i];
                    return TaskStatus.Success;
                }
        }
        return TaskStatus.Failure;
    }

    public bool WithinSight(Transform targetTransform, float fieldOfViewAngle)
    {
        Vector2 direction = targetTransform.position - transform.position;
        return Vector2.Angle(direction, transform.forward) < fieldOfViewAngle;
    }

}