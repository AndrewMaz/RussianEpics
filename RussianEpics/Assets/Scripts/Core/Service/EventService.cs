using Abstracts;

public class EventService
{
    private EnemyChecker _enemyChecker;
    private EventsSystem _eventsSystem;

    Enemy _boss = null;
    NPC _npc;
    public EventService(EnemyChecker enemyChecker, EventsSystem eventsSystem)
    {
        _enemyChecker = enemyChecker;
        _eventsSystem = eventsSystem;

        _enemyChecker.IsBossShowed += ActivateBossDialogue;
        _enemyChecker.OnEventShowed += StartEvent;
    }
    private void StartEvent(NPC npc)
    {
        _npc = npc;

        if (_npc.IsFailed == null)
        {
            _eventsSystem.SetDialogue(npc.Name);
            _npc.FailQuest();
            return;
        }
        if ((bool)_npc.IsFailed)
        {
            _eventsSystem.SetDialogue(npc.Name + "Failed");
        }
        else if (!(bool)_npc.IsFailed)
        {
            _eventsSystem.SetDialogue(npc.Name + "Succesed");
        }
    }
    private void ActivateBossDialogue(Enemy boss)
    {
        _boss = boss;
        _boss.IsDead += EndEvent;

        _eventsSystem.SetDialogue(_boss.Name + "Start");
    }
    private void EndEvent()
    {
        if (_boss != null && !_boss.IsAlive)
        {
            _eventsSystem.SetDialogue(_boss.Name + "End");

            _boss.IsDead -= EndEvent;
            _boss = null;
        }
    }
}
