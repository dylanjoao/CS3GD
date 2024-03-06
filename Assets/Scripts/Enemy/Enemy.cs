using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAI;

    void Start()
    {

        enemyAI = GetComponent<EnemyAI>();

        EnemyManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.Deregister(this);
    }

}