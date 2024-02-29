using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class ProximitySensor : MonoBehaviour
{
    EnemyAI LinkedAI;

    void Start()
    {
        LinkedAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        for (int index=0; index < DetectableTargetManager.Instance.AllTargets.Count; index++)
        {
            var candidateTarget = DetectableTargetManager.Instance.AllTargets[index];

            // Skip Ourself
            if (candidateTarget.gameObject == gameObject)
                continue;

            if (Vector3.Distance(LinkedAI.EyeLocation, candidateTarget.transform.position) <= LinkedAI.ProximityDetectionRange)
                LinkedAI.ReportInProximity(candidateTarget);
        }
    }
}