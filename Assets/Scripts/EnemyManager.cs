using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; } = null;

    public List<Enemy> AllTargets { get; private set; } = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Singleton EnemyManager found. Destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void Register(Enemy target)
    {
        AllTargets.Add(target);
    }

    public void Deregister(Enemy target)
    {
        AllTargets.Remove(target);
    }
}
