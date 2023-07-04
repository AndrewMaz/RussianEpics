using Abstracts;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCharacteristicsService
{
    private int _currentHealth, _maxHealth;

    private Weapon _weapon;
    private SpeedControlService _speedControlService;
    private PlayerHUD _playerHUD;
    private ScoreSystem _scoreSystem;

    public event Action IsPlayerDead;
    public event Action<int> IsPlayerDamaged;

    public PlayerCharacteristicsService(Weapon weapon, SpeedControlService speedControlService, int maxHealth, PlayerHUD playerHUD, ScoreSystem scoreSystem) // armor // 
    {
        _weapon = weapon;
        _speedControlService = speedControlService;
        _currentHealth = _maxHealth = maxHealth;
        _playerHUD = playerHUD;
        _scoreSystem = scoreSystem;
    }
    public void UseRune(Rune rune)
    {
        _playerHUD.AddToHUD(rune);

        switch (rune)
        {
            case HealthRune healthRune:  // BuffRune
                GetDamage(-Mathf.Abs(healthRune.HealAmount), healthRune);
                break;
            case WeaponRune arrowRune: // boostRune
                _weapon.ApplyRune(arrowRune);
                break;
            case BossRune bossRune:
                _scoreSystem.AddPoints(bossRune);
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
            _speedControlService.StopSpeed();
            IsPlayerDead?.Invoke();
        }
    }
    public void SubscribeToWeapon(Action methodName)
    {
        _weapon.OnDequeue += methodName;
    }
    public void UnsibscribeToWeapon(Action methodName)
    {
        _weapon.OnDequeue -= methodName;
    }
}
