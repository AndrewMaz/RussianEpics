using Abstracts;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk[] _bossChunks;
    [SerializeField] private ChunkEnd _chunkEnd;
    [SerializeField] private Transform _spawnObject;
    [Space]
    [Header("Наборы чанков")]
    [SerializeField] private List<Chunk> _startChunks;

    private Queue<Chunk> _chunkFeed = new Queue<Chunk>();
    private int _currentStage = 0;

    public bool IsGameStarted { get; set; } = false;

    private ScoreSystem _scoreSystem;
    private SpeedControlService _speedControlService;
    private void Start()
    {
        SpawnNextChunk();
    }
    private void OnEnable()
    {
        _scoreSystem.OnThreshold1 += HandleTreshold1;
    }
    private void OnDisable()
    {
        _scoreSystem.OnThreshold1 -= HandleTreshold1;
    }
    public void Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService)
    {
        _scoreSystem = scoreSystem;
        _speedControlService = speedControlService;

        enabled = true;
    }
    public void StartGame()
    {
        IsGameStarted = true;
        _chunks = _startChunks;
    }
/*    public void SetNewChunks(List<Chunk> chunks)
    {
        _chunks = chunks;
    }*/
    private void FillChunkFeed()
    {
        _chunks.Shuffle();

        foreach(Chunk chunk in _chunks) 
        {
            _chunkFeed.Enqueue(chunk);
        }
    }
    private void HandleTreshold1()
    {
        _chunkFeed.Clear();
        _chunkFeed.Enqueue(_bossChunks[_currentStage]);

        _currentStage++;
        if (_currentStage == _bossChunks.Length)
        {
            _currentStage = 0;
        }
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

                var instantiate = Instantiate(element.gameObject, _spawnObject, true);

                instantiate.name += $" {x} {y}";

                instantiate.transform.position =
                    new Vector3(position.x + x, position.y + y, 0.0f);
                var spElement = instantiate.GetComponent<SpawnElement>();
                spElement.Initialize(_scoreSystem, _speedControlService).SetStage(_currentStage);
            }
        }
        
        var instantiate1 = Instantiate(_chunkEnd);
        if (IsGameStarted)
        {
            instantiate1.GetComponent<SpawnElement>().Initialize(_scoreSystem, _speedControlService);
        }

        instantiate1.transform.position =
            new Vector3(position.x + x + 1, position.y, 0.0f);
    }
}
