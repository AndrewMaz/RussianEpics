using Abstracts;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> chunks;
    [SerializeField] private Chunk[] bosschunks;
    [SerializeField] private ChunkEnd chunkEnd;
    [SerializeField] private Transform spawnObject;

    private Queue<Chunk> _chunkFeed = new Queue<Chunk>();
    private int _currentStage = 0;

    private void Start()
    {
        SpawnNextChunk();
    }

    private void FillChunkFeed()
    {
        chunks.Shuffle();

        foreach(Chunk chunk in chunks) 
        {
            _chunkFeed.Enqueue(chunk);
        }
       
        _chunkFeed.Enqueue(bosschunks[_currentStage]);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out ChunkEnd _))
            SpawnNextChunk();
    }

    private void SpawnNextChunk()
    {
        if(_chunkFeed.Count == 0)
        {
            FillChunkFeed();
            _currentStage++;
            if (_currentStage == bosschunks.Length)
            {
                _currentStage = 0;
            }
        }

        var x = 0;
        var position = transform.position;
        var chunk = _chunkFeed.Dequeue();

        foreach (var segment in chunk.Segments)
        {
            x++;
            var y = 0;
            foreach (var element in segment.Spawnables)
            {
                y++;
                if(element == null)
                    continue;

                var instantiate = Instantiate(element.gameObject, spawnObject, true);
              
                instantiate.transform.position =
                    new Vector3(position.x + x, position.y + y, 0.0f);
                instantiate.GetComponent<SpawnElement>().SetStage(_currentStage);
            }
        }
        
        var instantiate1 = Instantiate(chunkEnd);
        
        instantiate1.transform.position =
            new Vector3(position.x + x + 1, position.y, 0.0f);
    }
}
