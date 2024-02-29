using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class VisionSensor : MonoBehaviour
{
    [SerializeField] LayerMask DetectionMask = ~0;

    EnemyAI LinkedAI;

    void Start()
    {
        LinkedAI = GetComponent<EnemyAI>();
    }

    void Update()
    {

        // Loop all detectable targets
        for (int index = 0; index < DetectableTargetManager.Instance.AllTargets.Count; index++)
        {
            var candidateTarget = DetectableTargetManager.Instance.AllTargets[index];

            // Skip ourself
            if (candidateTarget.gameObject == gameObject)
                continue;

            var targetPosition = candidateTarget.transform.position - LinkedAI.EyeLocation;

            // Not within distance
            if (targetPosition.sqrMagnitude > (LinkedAI.VisionConeRange * LinkedAI.VisionConeRange))
                continue;

            targetPosition.Normalize();

            // Not within vis cone
            if (Vector3.Dot(targetPosition, LinkedAI.EyeDirection) < LinkedAI.CosVisionConeAngle)
                continue;


            // Raycast
            RaycastHit hitResult;
            if (Physics.Raycast(LinkedAI.EyeLocation, targetPosition, out hitResult, LinkedAI.VisionConeRange, DetectionMask, QueryTriggerInteraction.Collide))
            {

                if (hitResult.collider.GetComponentInParent<DetectableTarget>() == candidateTarget)
                    LinkedAI.ReportCanSee(candidateTarget);
            }
        }
    }
}
