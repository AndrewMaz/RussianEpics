using Abstracts;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public List<Chunk> chunks;
    public Chunk[] bosschunks;
    [SerializeField] private ChunkEnd chunkEnd;
    [SerializeField] private Transform spawnObject;

    private Queue<Chunk> _chunkFeed = new Queue<Chunk>();
    private int _currentStage = 0;

    public bool IsGameStarted { get; set; } = false;

    private ScoreSystem _scoreSystem;
    private SpeedControlService _speedControlService;
    private void Start()
    {
        SpawnNextChunk();
    }
    public void Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService)
    {
        _scoreSystem = scoreSystem;
        _speedControlService = speedControlService;
    }
    public void StartGame()
    {
        IsGameStarted = true;
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

                instantiate.name += $" {x} {y}";

                instantiate.transform.position =
                    new Vector3(position.x + x, position.y + y, 0.0f);
                var spElement = instantiate.GetComponent<SpawnElement>();
                spElement.Initialize(_scoreSystem, _speedControlService).SetStage(_currentStage);
            }
        }
        
        var instantiate1 = Instantiate(chunkEnd);
/*        if (IsGameStarted)
        {
            instantiate1.GetComponent<SpawnElement>().Initialize(_scoreSystem, _speedControlService);
        }*/
        
        instantiate1.transform.position =
            new Vector3(position.x + x + 1, position.y, 0.0f);
    }
}
