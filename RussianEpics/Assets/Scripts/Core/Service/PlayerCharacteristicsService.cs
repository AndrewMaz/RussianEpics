using Abstracts;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using UnityEngine;

public class PlayerCharacteristicsService
{
    [SerializeField] float pointsForRune = 150f;

    private Weapon _weapon;
    private SpeedControlService _speedControlService;

    private int _currentHealth, _maxHealth;

    public event Action IsPlayerDead;
    public event Action<int> IsPlayerDamaged;

    public PlayerCharacteristicsService(Weapon weapon, SpeedControlService speedControlService, int maxHealth) // armor // 
    {
        _weapon = weapon;
        _speedControlService = speedControlService;
        _currentHealth = _maxHealth = maxHealth;
    }
    public void UseRune(Rune rune)
    {
        switch (rune)
        {
            case HealthRune healthRune:  // BuffRune
                GetDamage(-Mathf.Abs(healthRune.HealAmount), healthRune);
                break;
            case WeaponRune arrowRune: // boostRune
                _weapon.ApplyRune(arrowRune);
                break;
            case BossRune bossRune:
                bossRune.AddScore(pointsForRune);
                break;
            case SlowRune:
                _speedControlService.ChangeSpeed();
                break;
        }
    }
    public void Attack(Vector2 firePosition, float time)
    {
        _weapon.Attack(firePosition, time);
    }
    public void GetDamage(int damage, object sender)
    {
        _currentHealth -= damage;
        IsPlayerDamaged?.Invoke(_currentHealth);
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        if (_currentHealth < 0)
        {
            IsPlayerDead?.Invoke();
        }
    }
}
