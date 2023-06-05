using Abstracts;
using SpawnElements;

public class StartFloor : Floor
{
    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService)
    {
        base.Initialize(scoreSystem, speedControlService);
        return this;
    }
}
