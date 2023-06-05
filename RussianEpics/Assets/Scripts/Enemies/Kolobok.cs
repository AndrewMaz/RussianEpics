using Abstracts;
using System;
using UnityEngine;

public class Kolobok : Enemy
{
    [SerializeField] private float newSpeed = 5f;

    private float _startSpeed;

    private void Start()
    {
        _startSpeed = _motor.Speed;
    }
    public override void React()
    {
        _motor.Speed = newSpeed;
    }
    public override void GetDamage(int damage, object sender)
    {
        if (sender is ExplosionArrow || sender is Hammer)
        {
            base.GetDamage(damage, sender);
            _motor.Speed = _startSpeed;
        }
    }
}
