using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBase : MonoBehaviour
{
    public float Health;
    public float MaxHealth;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= MaxHealth;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
    }

}
