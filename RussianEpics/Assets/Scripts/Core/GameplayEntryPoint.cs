using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayEntryPoint : MonoBehaviour
{
    [Header("Элементы сцены")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private ChunkSpawner _spawner;
    [SerializeField] private PlayerHUD _playerHUD;
    [SerializeField] private BossHUD _bossHUD;
    [SerializeField] private MenuHUD _menuHUD;
    [SerializeField] private ScoreHUD _scoreHUD;
    [SerializeField] private EnemyChecker _enemyChecker;
    [SerializeField] private NPCChecker _npcChecker;
    [SerializeField] private SpeedControlService _speedControlService;
    [SerializeField] private StartFloor _startFloor;
    [SerializeField] private Timer _timer;
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private Image _playerImage;
    [SerializeField] private PlayerStats _playerStats;
    [Space]
    [Header("Плеера")]
    [SerializeField] private Player[] _players;
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Weapon _bowWeapon;
    [SerializeField] private Weapon _hammerWeapon;
    [SerializeField] private Trajectory _trajectory;
    [Space]
    [Header("Пороги очков")]
    [SerializeField] private float[] _thresholds;
    [Header("Префабы")]
    [SerializeField] Sprite[] _playerSprites;

    private PlayerCharacteristicsService _bowCharacteristicsService;
    private PlayerCharacteristicsService _hammerCharacteristicsService;
    private List<PlayerCharacteristicsService> _allCharacteristicsServices = new();

    private BossCharacteristicsService _bossService;
    private ScoreSystem _scoreSystem;
    private EventService _eventService;
    private void Awake()
    {
        _bowWeapon.Initialize(_playerStats);
        _hammerWeapon.Initialize(_playerStats);
        _dialogueSystem.Initialize(_dialogue, _speedControlService, _timer);
        _scoreSystem = new ScoreSystem(_thresholds);
        _eventService = new(_dialogueSystem, _scoreSystem);
        _enemyChecker.Initialize(_eventService);
        _npcChecker.Initialize(_eventService);
        _spawner.Initialize(_scoreSystem, _speedControlService, _timer, _dialogueSystem, _playerStats);
        _bossService = new BossCharacteristicsService(_enemyChecker, _dialogueSystem);
        _bossHUD.Initialize(_bossService);
        _startFloor.Initialize(_scoreSystem, _speedControlService, _timer, _dialogueSystem, _playerStats);
        SwitchPlayer(0);
    }
    public void Initialize()
    {
        //Players Characteristics Services
        _allCharacteristicsServices.Add(_bowCharacteristicsService = new PlayerCharacteristicsService(_bowWeapon, _speedControlService, _maxHealth, _playerHUD, _scoreSystem, _playerStats));
        _allCharacteristicsServices.Add(_hammerCharacteristicsService = new PlayerCharacteristicsService(_hammerWeapon, _speedControlService, _maxHealth, _playerHUD, _scoreSystem, _playerStats));
        //Player HUD
        _playerHUD.Initialize(_allCharacteristicsServices.ToArray(), _maxHealth);
        _playerHUD.gameObject.SetActive(true);
        //ScoreHUD
        _scoreHUD.Initialize(_scoreSystem);
        //Player Input
        _playerInput.Initialize(_mainCamera, _playerHUD, _trajectory, _speedControlService);
        //players
        int i = 0;
        foreach (var player in _players)
        {
            player.Initialize(_playerInput, _allCharacteristicsServices[i], _speedControlService);
            player.Animator.SetBool("start", false);
            i++;
        }
        //Pause Menu
        _menuHUD.Initialize(_allCharacteristicsServices.ToArray(), _scoreSystem);
        _spawner.StartGame();
        //SpeedControl Service
        _speedControlService.ResetSpeed();
    }

    public void SwitchPlayer(int playerIndex)
    {
        foreach (var player in _players)
        {
            player.gameObject.SetActive(false);
        }

        _players[playerIndex].gameObject.SetActive(true);
        if (_playerSprites.Length >= playerIndex)
        {
            _playerImage.sprite = _playerSprites[playerIndex];
        }
        else
        {
            _playerImage.sprite = _playerSprites[0];
        }
    }
}
