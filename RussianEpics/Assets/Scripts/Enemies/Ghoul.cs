using Abstracts;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class Ghoul : Enemy
{
    protected override void OnDamageDealt(IDamageable target)
    {
        base.OnDamageDealt(target);
        SetAnimatorTrigger("attack");
    }
}
