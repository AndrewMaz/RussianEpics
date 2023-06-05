using System;
public class BossCharacteristicsService
{
    public event Action<int> IsBossDamaged;
    public event Action<BossYaga> IsVisible;
    public event Action IsDead;

    EnemyChecker _enemyChecker;

    BossYaga _boss;
    public BossCharacteristicsService(EnemyChecker enemyChecker)
    {
        _enemyChecker = enemyChecker;
        _enemyChecker.IsBossShowed += ActivateUI;
    }

    public void GetDamage(int health)
    {
        IsBossDamaged?.Invoke(health);
    }
    public void ActivateUI(BossYaga boss)
    {
        IsVisible?.Invoke(boss);
        _boss = boss;
        _boss.IsDamaged += GetDamage;
        _boss.IsDead += DeactivateUI;
    }
    public void DeactivateUI()
    {
        IsDead?.Invoke();
        _enemyChecker.gameObject.SetActive(true);
        _boss.IsDamaged -= GetDamage;
        _boss.IsDead -= DeactivateUI;
    }
    ~BossCharacteristicsService()
    {
        _enemyChecker.IsBossShowed -= ActivateUI;
    }
}
