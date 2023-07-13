using Abstracts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossKolobok : Boss
{
    [SerializeField] private float _minForce = 100f;
    [SerializeField] private float _maxForce = 1000f;
    [SerializeField] private float _jumpChance = 75f;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float jumpInterval = 2f;

    private bool _shouldJump = true;
    private float _speed = 1f;

    private KolobokEvent _event;
    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer, DialogueSystem dialogueSystem, PlayerStats playerStats)
    {
        _event = new(dialogueSystem);

        return base.Initialize(scoreSystem, speedControlService, timer, dialogueSystem, playerStats);
    }

    void Start()
    {
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
    private new void OnEnable()
    {
        SetEventItem(_event);
        base.OnEnable();
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
        StartCoroutine(ActionCooldown(jumpInterval));
    }
    private IEnumerator ActionCooldown(float interval)
    {
        while (true) 
        {
            if (UnityEngine.Random.value <= _jumpChance / 100f)
            {
                _shouldJump = true;
            }

            yield return new WaitForSecondsRealtime(interval);
        }
    }
    private IEnumerator SpeedChange()
    {
        while (true) 
        {
            yield return new WaitForSecondsRealtime(2.5f);

            SetSpeed(_speed = -_speed);
        }
    }
    public override void ReactToTimer()
    {
        StopAllCoroutines();
        SetSpeed(-3f);
        SetRbStatic();
        SwitchColider(false);
        Destroy(gameObject, 2f);
    }
}
