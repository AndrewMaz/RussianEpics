using Abstracts;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class BossYaga : Enemy
{
    [SerializeField] private int _startHealth = 5;
    [SerializeField] private Transform _model;
    [SerializeField] private float _invTime = 1f;
    [SerializeField] private float newSpeed = 10f;
    [SerializeField] private BossShield _bossShield;

    private float _invTimer;
    private bool _isInvulnerable;

    //X (-2, 2);
    //Y (-1, 3);
    private void Start()
    {
        _invTimer = _invTime;
        Health = _startHealth;
    }
    private void Update()
    {
        if (_isInvulnerable)
        {
            MakeInvulnerable();
        }
    }
    public override void GetDamage(int damage, object sender)
    {
        if (_isInvulnerable) return;
        SetAnimatorTrigger("takeDamage");
        Health -= damage;
        if (Health > 0)
        {
            _isInvulnerable = true;
            Move();
        }
        base.OnDamaged(Health);
    }
    private void Move()
    {
        Vector2 offset = new(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-1f, 3f));
        _model.position += new Vector3(offset.x, offset.y, 0f);
        AddColiderOffset(offset);
    }

    public void Apply()
    {
        SetSpeed(newSpeed);
        SwitchColider(true);
        _bossShield.gameObject.SetActive(false);
    }

    public override void React()
    {
        SetSpeed(0f);
        gameObject.transform.position += Vector3.left * 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BossRune rune))
        {
            Apply();
            rune.gameObject.SetActive(false);
        }
    }

    private void MakeInvulnerable()
    {
        SwitchColider(false);
        _invTimer -= Time.deltaTime;
        _bossShield.gameObject.SetActive(true);

        if (_invTimer <= 0)
        {
            _isInvulnerable = false;
            _invTimer = _invTime;
            SwitchColider(true);
            _bossShield.gameObject.SetActive(false);
        }
    }
}