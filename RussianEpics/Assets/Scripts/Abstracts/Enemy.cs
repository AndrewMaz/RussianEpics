using Assets.Scripts.Interfaces;
using SpawnElements;
using UnityEngine;

namespace Abstracts
{
    public abstract class Enemy : SpawnElement
    {
        [SerializeField] private DamageArea _damageArea;
        [SerializeField] private int _damage;

        private void OnEnable()
        {
            _damageArea.IsDamageDealt += OnIsDamageDealt;
        }

        private void OnDisable()
        {
            _damageArea.IsDamageDealt -= OnIsDamageDealt;
        }

        protected virtual void OnIsDamageDealt(IDamageable target)
        {
            target.GetDamage(_damage);
        }
    }
}
