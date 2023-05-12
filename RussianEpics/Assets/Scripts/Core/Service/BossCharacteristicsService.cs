using System;
public class BossCharacteristicsService
{
    public event Action<int> IsBossDamaged;
    public event Action<BossYaga> IsVisible;
    public event Action IsDead;

    BossChecker _bossChecker;

    BossYaga _boss;
    public BossCharacteristicsService(BossChecker bossChecker)
    {
        _bossChecker = bossChecker;
        _bossChecker.IsBossShowed += ActivateUI;
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
        _bossChecker.gameObject.SetActive(true);
        _boss.IsDamaged -= GetDamage;
        _boss.IsDead -= DeactivateUI;
    }
    ~BossCharacteristicsService()
    {
        _bossChecker.IsBossShowed -= ActivateUI;
    }
}
