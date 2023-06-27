using Abstracts;
using System;
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
    [SerializeField] private List<Chunk> _chunks1;
    [SerializeField] private List<Chunk> _chunks2;

    private Queue<Chunk> _chunkFeed = new();
    private List<List<Chunk>> _chunkSets = new();

    private int _currentStage = 0;

    public bool IsGameStarted { get; set; } = false;

    private ScoreSystem _scoreSystem;
    private SpeedControlService _speedControlService;
    private BossCharacteristicsService _bossCharacteristicsService;
    private Timer _timer;
    private void Start()
    {
        SpawnNextChunk();
        _chunkSets.Add(_chunks1);
        _chunkSets.Add(_chunks2);
    }
    private void OnEnable()
    {
        _scoreSystem.OnThreshold += HandleTreshold;
        _bossCharacteristicsService.IsDead += HandleIndex;
    }
    private void OnDisable()
    {
        _scoreSystem.OnThreshold -= HandleTreshold;
        _bossCharacteristicsService.IsDead -= HandleIndex;
    }
    public void Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, BossCharacteristicsService bossCharacteristicsService, Timer timer)
    {
        _scoreSystem = scoreSystem;
        _speedControlService = speedControlService;
        _bossCharacteristicsService = bossCharacteristicsService;
        _timer = timer;

        enabled = true;
    }
    public void StartGame()
    {
        IsGameStarted = true;
        SetNewChunks(_chunkSets[0]);
    }
    public void SetNewChunks(List<Chunk> chunks)
    {
        _chunks = chunks;
    }
    private void FillChunkFeed()
    {
        _chunks.Shuffle();

        foreach(Chunk chunk in _chunks) 
        {
            _chunkFeed.Enqueue(chunk);
        }
    }
    private void HandleTreshold()
    {
        _chunkFeed.Clear();
        _chunkFeed.Enqueue(_bossChunks[_currentStage]);

        SetNewChunks(_chunkSets[_currentStage]);
        FillChunkFeed();
    }
    private void HandleIndex()
    {
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
                spElement.Initialize(_scoreSystem, _speedControlService, _timer).SetStage(_currentStage);
            }
        }
        
        var instantiate1 = Instantiate(_chunkEnd);
        if (IsGameStarted)
        {
            instantiate1.GetComponent<SpawnElement>().Initialize(_scoreSystem, _speedControlService, _timer);
        }

        instantiate1.transform.position =
            new Vector3(position.x + x + 1, position.y, 0.0f);
    }
}
