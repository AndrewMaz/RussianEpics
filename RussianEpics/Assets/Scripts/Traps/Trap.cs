using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BackgroundElement
{
    [SerializeField] private int _damage;
    [SerializeField] private DamageArea _damageArea;

    private new void OnEnable()
    {
        base.OnEnable();
        _damageArea.IsDamageDealt += OnDamageDealt;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        _damageArea.IsDamageDealt -= OnDamageDealt;
    }

    private void OnDamageDealt(IDamageable target)
    {
        target.GetDamage(_damage, this);
    }

}
