using Abstracts;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using UnityEngine;

public class PlayerCharacteristicsService
{
    private Weapon _weapon;

    public event Action IsPlayerDead;
    public event Action<int> IsPlayerDamaged;

    public PlayerCharacteristicsService(Weapon weapon) // hp // armor // 
    {
        _weapon = weapon;
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
            case BossRune _:  /// Giper
                Debug.Log("Points added");
                //statPoints.AddPoints(); ?
                break;
        }
    }

    public void Attack(Vector2 firePosition, float time)
    {
        _weapon.Attack(firePosition, time);
    }
    
    public void GetDamage(int damage, object sender)
    {
        IsPlayerDamaged?.Invoke(damage);
    }

}
