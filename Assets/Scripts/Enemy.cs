using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] CharacterAgent characterAgent;
    [SerializeField] EnemyAI enemyAI;

    public float health;

    void Start()
    {
        characterAgent = GetComponent<CharacterAgent>();
        enemyAI = GetComponent<EnemyAI>();

        EnemyManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.Deregister(this);
    }

}
