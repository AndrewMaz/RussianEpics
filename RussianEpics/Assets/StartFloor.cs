using Abstracts;
using SpawnElements;

public class StartFloor : Floor
{
    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer)
    {
        base.Initialize(scoreSystem, speedControlService, timer);
        return this;
    }
}
