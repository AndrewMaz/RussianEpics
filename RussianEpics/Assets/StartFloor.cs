using Abstracts;
using SpawnElements;

public class StartFloor : Floor
{
    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer, DialogueSystem dialogueSystem, PlayerStats playerStats)
    {
        base.Initialize(scoreSystem, speedControlService, timer, dialogueSystem, playerStats);
        return this;
    }
}
