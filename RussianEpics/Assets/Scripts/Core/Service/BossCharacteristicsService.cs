using System;


public class BossCharacteristicsService
{
    public event Action<int> IsBossDamaged;
    public event Action<BossYaga> IsVisible;

    BossChecker _bossChecker;

    public BossCharacteristicsService(BossChecker bossChecker)
    {
        _bossChecker = bossChecker;
        _bossChecker.IsBossShowed += ActivateUI;
    }
    ~BossCharacteristicsService()
    {
        _bossChecker.IsBossShowed -= ActivateUI;
    }
    public void GetDamage(int damage, object sender)
    {
        IsBossDamaged?.Invoke(damage);

    }
    public void ActivateUI(BossYaga boss)
    {
        IsVisible?.Invoke(boss);
    }
}
