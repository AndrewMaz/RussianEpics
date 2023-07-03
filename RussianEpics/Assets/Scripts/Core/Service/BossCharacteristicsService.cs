using Abstracts;
using System;
public class BossCharacteristicsService
{
    public event Action<int> IsBossDamaged;
    public event Action<Enemy> IsVisible;
    public event Action IsDead;

    private EnemyChecker _enemyChecker;
    private EventsSystem _eventsSystem;

    private Enemy _boss;
    public BossCharacteristicsService(EnemyChecker enemyChecker, EventsSystem eventsSystem)
    {
        _enemyChecker = enemyChecker;
        _eventsSystem = eventsSystem;

        _enemyChecker.IsBossShowed += ActivateUI;
    }
    public void GetDamage(int health)
    {
        IsBossDamaged?.Invoke(health);
    }
    public void ActivateUI(Enemy boss)
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
        _boss.IsDamaged -= GetDamage;
        _boss.IsDead -= DeactivateUI;
    }
}
