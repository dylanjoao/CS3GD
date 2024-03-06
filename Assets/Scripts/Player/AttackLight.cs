using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLight : MonoBehaviour
{
    public float range;
    public float damage;
    public float angle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
            
    }

    void Attack()
    {
        for (int i = 0; i < EnemyManager.Instance.AllTargets.Count; i++)
        {
            Enemy target = EnemyManager.Instance.AllTargets[i];

            Vector3 targetPosition = target.transform.position - this.transform.position;


            // Not within distance
            if (targetPosition.sqrMagnitude > (range * range))
            {
                continue;
            }

            targetPosition.Normalize();

            // Not within vis cone
            if (Vector3.Dot(targetPosition, this.transform.eulerAngles) < angle)
            {
                continue;
            }

        }
    }
}
