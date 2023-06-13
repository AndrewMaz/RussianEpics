using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Abstracts
{
    public abstract class Enemy : SpawnElement, IDamageable
    {
        [SerializeField] private DamageArea _damageArea;
        [SerializeField] private int _damage;
        [SerializeField] protected Animator _animator;

        protected Rigidbody2D _rb;
        protected CapsuleCollider2D _capsuleCollider;
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
        protected virtual void OnDamageDealt(IDamageable target)
        {
            target.GetDamage(_damage, this);
        }
        public virtual void GetDamage(int damage, object sender)
        {
            _animator.SetTrigger("takeDamage");
            _rb.bodyType = RigidbodyType2D.Static;
            _capsuleCollider.offset += Vector2.down;
            AddScore();
        }
        public virtual void React()
        {

        }
    }
}
