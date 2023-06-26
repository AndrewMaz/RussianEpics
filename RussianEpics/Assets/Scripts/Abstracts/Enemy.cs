using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : SpawnElement, IDamageable
    {
        [SerializeField] private DamageArea _damageArea;
        [SerializeField] private int _damage;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _points = 0f;
        [SerializeField] private int _health = 1;
        public float Points { get => _points; }

        private Rigidbody2D _rb;
        private CapsuleCollider2D _capsuleCollider;

        public event Action<int> IsDamaged;
        public event Action IsDead;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    _health = 0;
                    AddScore(Points);
                    IsDead?.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }
        private new void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
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
        protected virtual void OnDamaged(int value)
        {
            IsDamaged?.Invoke(value);
        }
        protected virtual void OnDamageDealt(IDamageable target)
        {
            target.GetDamage(_damage, this);
        }
        public virtual void GetDamage(int damage, object sender)
        {
            SetAnimatorTrigger("takeDamage");
            SetRbStatic();
            AddColiderOffset(Vector2.down);
            AddScore(_points);
        }
        public virtual void React()
        {

        }
        protected void SetRbStatic()
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
        protected void SetRbDinamic()
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
        protected void SwitchColider(bool value)
        {
            _capsuleCollider.enabled = value;
        }
        protected void AddColiderOffset(Vector2 offset)
        {
            _capsuleCollider.offset += offset;
        }
        protected void SetAnimatorTrigger(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
        protected void Jump(float minForce, float maxForce)
        {
            _rb.AddForce(new Vector2(0, UnityEngine.Random.Range(minForce, maxForce)));
        }
        protected void SetHealth(int value)
        {
            _health = value;
        }
    }
}
