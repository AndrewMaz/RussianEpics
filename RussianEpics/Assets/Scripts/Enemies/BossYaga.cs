using Abstracts;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class BossYaga : Enemy
{
    [SerializeField] private int _health = 5;
    [SerializeField] private Transform _model;
    [SerializeField] private float _invTime = 1f;
    [SerializeField] private float newSpeed = 10f;
    [SerializeField] private BossShield _bossShield;

    public event Action<int> IsDamaged;
    public event Action IsDead;

    private float _timer;
    private bool _isInvulnerable;

    //X (-2, 2);
    //Y (-1, 3);
    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                IsDead?.Invoke();
                AddScore(Points);
                gameObject.SetActive(false);
            }
            else
            {
                _isInvulnerable = true;
                Move();
            }
        }
    }
    private void Start()
    {
        _timer = _invTime;
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
        IsDamaged?.Invoke(Health);
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
        _timer -= Time.deltaTime;
        _bossShield.gameObject.SetActive(true);

        if (_timer <= 0)
        {
            _isInvulnerable = false;
            _timer = _invTime;
            SwitchColider(true);
            _bossShield.gameObject.SetActive(false);
        }
    }
}