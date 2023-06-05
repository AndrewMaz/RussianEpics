using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk[] _bosschunks;

    private MainMenu _mainMenu;
    private GameplayEntryPoint _gameplayEntryPoint;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        //main Menu
        _mainMenu = FindObjectOfType<MainMenu>();
        _mainMenu.startButton.onClick.AddListener(StartGame);
        _mainMenu.IsPlayerChangedToBow += ChangePlayerToBow;
        _mainMenu.IsPlayerChangedToHammer += ChangePlayerToHammer;
        //Entry point
        _gameplayEntryPoint = FindObjectOfType<GameplayEntryPoint>();
    }
    private void OnDisable()
    {
        _mainMenu.IsPlayerChangedToBow -= ChangePlayerToBow;
        _mainMenu.IsPlayerChangedToHammer -= ChangePlayerToHammer;
    }
    private void StartGame()
    {
        _mainMenu.gameObject.SetActive(false);
        _gameplayEntryPoint._spawner.chunks = _chunks;
        _gameplayEntryPoint._spawner.bosschunks = _bosschunks;
        _gameplayEntryPoint.Initialize();
        _gameplayEntryPoint._bowPlayer.Animator.SetBool("start", false);
        _gameplayEntryPoint._hammerPlayer.Animator.SetBool("start", false);
        _gameplayEntryPoint._speedControlService.ResetSpeed();
    }
    private void ChangePlayerToBow()
    {
        _gameplayEntryPoint._bowPlayer.gameObject.SetActive(true);
        _gameplayEntryPoint._hammerPlayer.gameObject.SetActive(false);
    }
    private void ChangePlayerToHammer()
    {
        _gameplayEntryPoint._bowPlayer.gameObject.SetActive(false);
        _gameplayEntryPoint._hammerPlayer.gameObject.SetActive(true);
    }
}
