using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Ёлементы сцены")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Finish _finish;
    [SerializeField] private ChunkSpawner _spawner;
    [SerializeField] private PlayerHUD _playerHUD;
    [SerializeField] private BossHUD _bossHUD;
    [SerializeField] private BossChecker _bossChecker;

    [Header("ѕлеера")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Weapon _playerWeapon;
   
    private PlayerCharacteristicsService _characteristicsService;
    private BossCharacteristicsService _bossService;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _characteristicsService = new PlayerCharacteristicsService(_playerWeapon);
        _bossService = new BossCharacteristicsService(_bossChecker);
        _playerInput.Initialize(_mainCamera, _playerHUD);
        _player.Initialize(_playerInput, _characteristicsService);
        _playerHUD.Initialize(_characteristicsService);
        _bossHUD.Initialize(_bossService);
    }
}
