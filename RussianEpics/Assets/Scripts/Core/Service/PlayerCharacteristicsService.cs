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
    private PlayerStats _playerStats;

    public event Action IsPlayerDead;
    public event Action<int> IsPlayerDamaged;
    public event Action<int> OnPlayerHealthChange;

    public PlayerCharacteristicsService(Weapon weapon, SpeedControlService speedControlService, int maxHealth, PlayerHUD playerHUD, ScoreSystem scoreSystem, PlayerStats playerStats) // armor // 
    {
        _weapon = weapon;
        _speedControlService = speedControlService;
        _currentHealth = _maxHealth = maxHealth;
        _playerHUD = playerHUD;
        _scoreSystem = scoreSystem;
        _playerStats = playerStats;
    }
    public void UseRune(Rune rune)
    {
        _playerHUD.AddToHUD(rune);

        switch (rune)
        {
            case HealthRune healthRune:  // BuffRune
                Heal(_playerStats.GetLvl(healthRune.GetType().ToString()) / 10f);
                break;
            case WeaponRune arrowRune: // boostRune
                _weapon.ApplyRune(arrowRune);
                break;
            case BossRune bossRune:
                _scoreSystem.AddPoints(bossRune);
                break;
            case SlowRune slowRune:
                _speedControlService.ChangeSpeed(_playerStats.GetLvl(slowRune.GetType().ToString()) * 2 + 1);
                break;
            case MaxHealthRune maxHealthRune:
                IncreaseHealth(maxHealthRune.HealthAmount); 
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
        OnPlayerHealthChange?.Invoke(_currentHealth);
        IsPlayerDamaged?.Invoke(_currentHealth);

        if (_currentHealth < 0)
        {
            _speedControlService.StopSpeed();
            IsPlayerDead?.Invoke();
        }
    }
    public void IncreaseHealth(int amount)
    {
        _maxHealth += amount;
        _currentHealth += amount;
        OnPlayerHealthChange?.Invoke(_currentHealth);
    }
    public void SubscribeToWeapon(Action methodName)
    {
        _weapon.OnDequeue += methodName;
    }
    public void UnsibscribeToWeapon(Action methodName)
    {
        _weapon.OnDequeue -= methodName;
    }
    private void Heal(float multiplier)
    {
        _currentHealth += (int)(_maxHealth * multiplier);
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        OnPlayerHealthChange?.Invoke(_currentHealth);
    }
}
