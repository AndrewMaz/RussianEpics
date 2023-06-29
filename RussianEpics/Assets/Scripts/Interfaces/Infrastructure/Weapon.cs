using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces.Infrastructure
{
    public abstract class Weapon: MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _shootingDelay;
        [SerializeField] private float _shootingMinDistance;

        public float ShootingDelay => _shootingDelay;
        public float ShootingMinDistance => _shootingMinDistance;
        public float Force => _force;

        public event Action OnDequeue;
        
        protected void DequeueInvoke()
        {
            OnDequeue?.Invoke();
        }
        protected abstract bool CanShoot(Vector2 firePosition, float time);
        public abstract void Attack(Vector2 firePosition, float force);
        public abstract void ApplyRune(WeaponRune rune);
    }
}
