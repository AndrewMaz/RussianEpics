using Abstracts;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class BossYaga : Enemy
{
    [SerializeField] private int _health = 5;
    [SerializeField] private Transform _model;
    [SerializeField] private float _invTime = 1f;
    [SerializeField] private BossShield _bossShield;

    public event Action<int> IsDamaged;
    public event Action IsDead;

    private float _timer;
    private bool _isInvulnerable;

    //X (-2, 4);
    //Y (-1, 3);

    private CapsuleCollider2D capsuleCollider;
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
                AddScore();
                gameObject.SetActive(false);
            }
            else
            {
                _isInvulnerable = true;
                Move();
            }
        }
    }
    private new void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
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
        _animator.SetTrigger("takeDamage");
        Health -= damage;
        IsDamaged?.Invoke(Health);
    }

    private void Move()
    {
        Vector2 offset = new(UnityEngine.Random.Range(-2f, 4f), UnityEngine.Random.Range(-1f, 3f));
        _model.position += new Vector3(offset.x, offset.y, 0f);
        capsuleCollider.offset += offset;
    }

    public void Apply()
    {
        _motor.enabled = true;
        capsuleCollider.enabled = true;
        _bossShield.gameObject.SetActive(false);
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
        capsuleCollider.enabled = false;
        _timer -= Time.deltaTime;
        _bossShield.gameObject.SetActive(true);

        if (_timer <= 0)
        {
            _isInvulnerable = false;
            _timer = _invTime;
            capsuleCollider.enabled = true;
            _bossShield.gameObject.SetActive(false);
        }
    }
}