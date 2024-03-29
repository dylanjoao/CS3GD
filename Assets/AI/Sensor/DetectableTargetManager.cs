using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetManager : MonoBehaviour
{
    public static DetectableTargetManager Instance { get; private set; } = null;

    public List<DetectableTarget> AllTargets { get; private set; } = new List<DetectableTarget>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Singleton DetectableTargetManger found. Destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void Register(DetectableTarget target)
    {
        AllTargets.Add(target);
    }

    public void Deregister(DetectableTarget target)
    {
        AllTargets.Remove(target);
    }
}
