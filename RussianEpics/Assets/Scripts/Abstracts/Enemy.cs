using Assets.Scripts.Interfaces;
using SpawnElements;
using UnityEngine;

namespace Abstracts
{
    public abstract class Enemy : SpawnElement, IDamageable
    {
        [SerializeField] private DamageArea _damageArea;
        [SerializeField] private int _damage;
        [SerializeField] protected Animator _animator;

        private void OnEnable()
        {
            _damageArea.IsDamageDealt += OnDamageDealt;
        }

        private void OnDisable()
        {
            _damageArea.IsDamageDealt -= OnDamageDealt;
        }

        protected virtual void OnDamageDealt(IDamageable target)
        {
            target.GetDamage(_damage, this);
        }

        public virtual void GetDamage(int damage, object sender)
        {
            _animator.SetTrigger("takeDamage");
        }
    }
}
