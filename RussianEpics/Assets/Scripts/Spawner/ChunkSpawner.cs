using Abstracts;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk[] _eventChunks;
    [SerializeField] private ChunkEnd _chunkEnd;
    [SerializeField] private Transform _spawnObject;
    [Space]
    [Header("Наборы чанков")]
    [SerializeField] private List<Chunk> _chunks1;
    [SerializeField] private List<Chunk> _chunks2;

    private Queue<Chunk> _chunkFeed = new();
    private List<List<Chunk>> _chunkSets = new();

    private int _currentStage = 0, _currentEvent = 0;

    public bool IsGameStarted { get; set; } = false;

    private ScoreSystem _scoreSystem;
    private SpeedControlService _speedControlService;
    private BossCharacteristicsService _bossCharacteristicsService;
    private Timer _timer;
    private DialogueSystem _dialogueSystem;
    private PlayerStats _playerStats;
    private void Start()
    {
        SpawnNextChunk();
        _chunkSets.Add(_chunks1);
        _chunkSets.Add(_chunks2);
    }
    public void Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer, DialogueSystem dialogueSystem, PlayerStats playerStats)
    {
        _scoreSystem = scoreSystem;
        _speedControlService = speedControlService;
        _timer = timer;
        _dialogueSystem = dialogueSystem;
        _playerStats = playerStats;

        enabled = true;
    }
    private void OnEnable()
    {
        _scoreSystem.OnThreshold += HandleEvent;
    }
    private void OnDisable()
    {
        _scoreSystem.OnThreshold -= HandleEvent;
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
    public void HandleEvent()
    {
        _chunkFeed.Clear();
        _chunkFeed.Enqueue(_eventChunks[_currentEvent]);
        _currentEvent++;
/*        foreach (var chunk in _eventChunks)
        {
            if (chunk.name == eventItem.GetType().ToString())
            {
                _chunkFeed.Enqueue(chunk);
                break;
            }
        }*/
        SetNewChunks(_chunkSets[_currentStage]);
        FillChunkFeed();
    }
    private void HandleIndex()
    {
        _currentStage++;
        if (_currentStage == _eventChunks.Length)
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

        float x = 0.0f;
        Transform lastTransform = transform;
        
        var chunk = _chunkFeed.Dequeue();

        foreach (var segment in chunk.Segments)
        {
            x++;
            float y = 0.0f;
            foreach (var element in segment.Spawnables)
            {
                y++;
                if(element == null)
                    continue;

                var instantiate = Instantiate(element.gameObject, _spawnObject, true);

                instantiate.name += $" {x} {y}";

                instantiate.transform.position =
                    new Vector3(lastTransform.position.x + x, lastTransform.position.y + y, 0.0f);
                var spElement = instantiate.GetComponent<SpawnElement>();
                spElement.Initialize(_scoreSystem, _speedControlService, _timer, _dialogueSystem, _playerStats).SetStage(_currentStage);
             
            }
        }
        
        var instantiate1 = Instantiate(_chunkEnd);
        instantiate1.transform.position =
        new Vector3(lastTransform.position.x + x + 1.0f, lastTransform.position.y, 0.0f);
        lastTransform = instantiate1.transform;

        if (IsGameStarted)
        {
            instantiate1.GetComponent<SpawnElement>().Initialize(_scoreSystem, _speedControlService, _timer, _dialogueSystem, _playerStats);
        }
    }
}
