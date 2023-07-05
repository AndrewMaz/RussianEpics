
using System;
using UnityEngine.PlayerLoop;

public class OldManQuestEvent : Event
{
    ChunkSpawner _chunkSpawner;
    public OldManQuestEvent(ChunkSpawner chunkSpawner) 
    {
        _chunkSpawner = chunkSpawner;
    }
    public override void Start()
    {
        base.Start();
        _chunkSpawner.HandleEvent(this);
    }
    public override void Finish()
    {
        base.Finish();
    }
}
