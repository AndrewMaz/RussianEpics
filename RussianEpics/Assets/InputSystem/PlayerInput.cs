using Assets.Scripts.Interfaces.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [Header("Throw timings")]
    [SerializeField] private float minMultiplier = 0.1f;
    [SerializeField] private float maxMultiplier = 1f;

    private float _multiplierTimer;
    private bool _isShootStarted = false;
    private SchemePlayerInput _input;
    private Camera _camera;
    private PlayerHUD _hud;

    public event Action IsJumped;
    public event Action<Vector2, float> IsShot;
    public event Action IsStance;

    public void Initialize(Camera camera, PlayerHUD playerHUD)
    {
        _hud = playerHUD;
        _camera = camera;
    }

    private void Awake()
    {
        _input = new SchemePlayerInput();
        _multiplierTimer = 0f;
    }

    private void Update()
    {
        if (_isShootStarted)
            _multiplierTimer += Time.deltaTime;
        if (_multiplierTimer >= 1f)
            Shoot();
    }

    private void OnEnable()
    {
        _input.Enable();
        _hud.IsJumped += IsJumped;
        _input.Player.Jump.performed += Jump;
        _input.Player.Fire.performed += Fire;
        _input.Player.StartShooting.performed += StartShooting;
    }

    private void OnDisable()
    {
        _input.Disable();
        _hud.IsJumped -= IsJumped;
        _input.Player.Jump.performed -= Jump;
        _input.Player.Fire.performed -= Fire;
        _input.Player.StartShooting.performed -= StartShooting;
    }

    private void StartShooting(InputAction.CallbackContext context)
    {
        _isShootStarted = true;
        IsStance?.Invoke();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
    {
        var targetPosition = _camera.ScreenToWorldPoint(_input.Player.Tap.ReadValue<Vector2>());
        var timing = Mathf.Lerp(minMultiplier, maxMultiplier, _multiplierTimer);

        _isShootStarted = false;
        _multiplierTimer = 0f;

        IsShot?.Invoke(targetPosition, timing);
    }
    private void Jump(InputAction.CallbackContext context) => IsJumped?.Invoke();

}
