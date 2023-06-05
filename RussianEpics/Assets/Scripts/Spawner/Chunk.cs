using System;
using System.Collections.Generic;
using Abstracts;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Chunk", order = 1)]
public class Chunk: ScriptableObject
{
    [Serializable]
    public struct ChunkSegment
    {
        [SerializeReference] public SpawnElement[] Spawnables;
    }
    
    [SerializeField] public List<ChunkSegment> Segments = new ();
}
