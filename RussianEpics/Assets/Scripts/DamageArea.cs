using Abstracts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public event Action<IDamageable> IsDamageDealt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            IsDamageDealt?.Invoke(player);
        }
        else
        if (collision.TryGetComponent(out Enemy enemy))
        {
            IsDamageDealt?.Invoke(enemy);
        } 
        else
        if (collision.TryGetComponent(out Arrow arrow))
        {
            IsDamageDealt?.Invoke(arrow);
        }
    }
}
