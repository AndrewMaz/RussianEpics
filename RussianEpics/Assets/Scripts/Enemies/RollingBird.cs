using Abstracts;
using System;
using UnityEngine;

public class RollingBird : Enemy
{
    [SerializeField] private float newSpeed = 5f;

    private float _startSpeed;

    private void Start()
    {
        _startSpeed = Speed;
    }
    public override void React()
    {
        if (!IsAlive) return;
        SetSpeed(newSpeed);
    }
    public override void GetDamage(int damage, object sender)
    {
        if (sender is ExplosionArrow || sender is Hammer)
        {
            base.GetDamage(damage, sender);
            SetSpeed(_startSpeed);
            SetDead();
        }
    }
}
