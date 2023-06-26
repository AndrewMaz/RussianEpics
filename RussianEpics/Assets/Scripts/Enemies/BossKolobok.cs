using Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossKolobok : Enemy
{
    [SerializeField] private float _minForce = 100f;
    [SerializeField] private float _maxForce = 1000f;
    [SerializeField] private float _jumpChance = 75f;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _startHealth = 6;

    private bool _shouldJump = true;
    private float _speed = 1.5f;

    void Start()
    {
        Health = _startHealth;
        StartCoroutine(SpeedChange());
    }
    void Update()
    {
        if (_shouldJump) 
        {
            Jump(_minForce, _maxForce);
            _shouldJump = false;
        }
    }
    public override void GetDamage(int damage, object sender)
    {
        SetAnimatorTrigger("takeDamage");
        Health -= damage;
        base.OnDamaged(Health);
    }
    public override void React()
    {
        SetSpeed(_speed);
        SetTimer(_lifeTime);
        StartTimer();
        StartCoroutine(ActionCooldown());
    }

    private IEnumerator ActionCooldown()
    {
        while (true) 
        {
            if (UnityEngine.Random.value <= _jumpChance / 100f)
            {
                _shouldJump = true;
            }

            yield return new WaitForSecondsRealtime(3f);
        }
    }
    private IEnumerator SpeedChange()
    {
        while (true) 
        {
            yield return new WaitForSeconds(3f);

            SetSpeed(_speed = -_speed);
        }
    }
    protected override void ReactToTimer()
    {
        SetSpeed(-3f);
        Destroy(this, 5);
    }
}
