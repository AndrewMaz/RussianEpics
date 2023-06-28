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
        _eventsSystem.SetDialogue(_boss.Name + "Start");
    }
    public void DeactivateUI()
    {
        IsDead?.Invoke();
        _enemyChecker.gameObject.SetActive(true);
        if (!_boss.IsAlive)
        {
            _eventsSystem.SetDialogue(_boss.Name + "End");
        }

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
