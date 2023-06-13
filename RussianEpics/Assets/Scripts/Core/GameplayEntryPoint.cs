using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [Header("Ёлементы сцены")]
    [SerializeField] private Camera _mainCamera;
    public ChunkSpawner _spawner;
    [SerializeField] private PlayerHUD _playerHUD;
    [SerializeField] private BossHUD _bossHUD;
    [SerializeField] private MenuHUD _menuHUD;
    [SerializeField] private EnemyChecker _enemyChecker;
    public SpeedControlService _speedControlService;
    [SerializeField] private StartFloor _startFloor;

    [Header("ѕлеера")]
    public Player _bowPlayer;
    public Player _hammerPlayer;
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] Weapon _bowWeapon;
    [SerializeField] Weapon _hammerWeapon;
    [SerializeField] Trajectory _trajectory;

    private PlayerCharacteristicsService _bowCharacteristicsService;
    private PlayerCharacteristicsService _hammerCharacteristicsService;
    private List<PlayerCharacteristicsService> _allCharacteristicsServices = new List<PlayerCharacteristicsService>();

    private BossCharacteristicsService _bossService;
    private ScoreSystem _scoreSystem;
    private void Awake()
    {
        _bossService = new BossCharacteristicsService(_enemyChecker);
        _scoreSystem = new ScoreSystem();
        _bossHUD.Initialize(_bossService);
        _spawner.Initialize(_scoreSystem, _speedControlService);
        _startFloor.Initialize(_scoreSystem, _speedControlService);
    }
    public void Initialize()
    {
        //Players Characteristics Services
        _allCharacteristicsServices.Add(_bowCharacteristicsService = new PlayerCharacteristicsService(_bowWeapon, _speedControlService, _maxHealth));
        _allCharacteristicsServices.Add(_hammerCharacteristicsService = new PlayerCharacteristicsService(_hammerWeapon, _speedControlService, _maxHealth));
        //Player HUD
        _playerHUD.Initialize(_allCharacteristicsServices.ToArray(), _maxHealth);
        _playerHUD.gameObject.SetActive(true);
        //Player Input
        _playerInput.Initialize(_mainCamera, _playerHUD, _trajectory, _speedControlService);
        //bowPlayer
        _bowPlayer.Initialize(_playerInput, _bowCharacteristicsService, _speedControlService);
        //hammerPlayer
        _hammerPlayer.Initialize(_playerInput, _hammerCharacteristicsService, _speedControlService);
        //Pause Menu
        _menuHUD.Initialize(_allCharacteristicsServices.ToArray(), _speedControlService);
        _spawner.StartGame();
    }
}
