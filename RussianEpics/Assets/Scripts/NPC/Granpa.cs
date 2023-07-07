using Abstracts;
using UnityEngine;
public class Granpa : NPC
{
    OldManQuestEvent _event;

    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer, DialogueSystem dialogueSystem, PlayerStats playerStats)
    {
        _event = new(dialogueSystem);

        return base.Initialize(scoreSystem, speedControlService, timer, dialogueSystem, playerStats);
    }
    private new void OnEnable()
    {
        base.OnEnable();
        SetEventItem(_event);
    }
}
