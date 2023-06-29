using Assets.Scripts.Interfaces.Infrastructure;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [Header("Throw timings")]
    [SerializeField] private float minMultiplier = 0f;
    [SerializeField] private float maxMultiplier = 1f;
    [Space]
    [SerializeField] private Transform _trajectoryTransform;

    private float _multiplierTimer;
    private bool _isShootStarted = false, _isStanding;

    private SchemePlayerInput _input;
    private Camera _camera;
    private PlayerHUD _hud;
    private Trajectory _trajectory;
    private SpeedControlService _speedControlService;

    const float _boundaryX = -17f;

    public event Action IsJumped;
    public event Action<Vector2, float> IsShot;
    public event Action IsStance;

    public void Initialize(Camera camera, PlayerHUD playerHUD, Trajectory trajectory, SpeedControlService speedControlService)
    {
        _hud = playerHUD;
        _camera = camera;
        _trajectory = trajectory;
        _speedControlService = speedControlService;

        enabled = true;
    }
    private void Awake()
    {
        _input = new SchemePlayerInput();
        _multiplierTimer = 0f;
    }
    private void Update()
    {
        if (_isStanding) return;

        if (_isShootStarted)
        {
            _multiplierTimer += Time.deltaTime;
            if (_trajectory.gameObject.activeInHierarchy)
            {
                _trajectory.UpdateDots(new Vector3(_trajectoryTransform.position.x, _trajectoryTransform.position.y, _trajectoryTransform.position.z),
                    _multiplierTimer * 70f * (_camera.ScreenToWorldPoint(_input.Player.Tap.ReadValue<Vector2>()) - _trajectoryTransform.position).normalized);
            }
        }
        _hud.UpdatePowerSlider(_multiplierTimer);

        if (_multiplierTimer >= 1f)
            Shoot();
    }
    private void OnEnable()
    {
        if (_speedControlService!= null) 
        {
            _speedControlService.OnSpeedChange += ChangeControls;
        }

        _input.Enable();
        _hud.IsJumped += () => IsJumped?.Invoke();
        _input.Player.Jump.performed += Jump;
        _input.Player.Fire.performed += Fire;
        _input.Player.StartShooting.performed += StartShooting;
    }
    private void OnDisable()
    {
        if (_speedControlService != null)
        {
            _speedControlService.OnSpeedChange -= ChangeControls;
        }

        _input.Disable();
        _hud.IsJumped -= () => IsJumped?.Invoke();
        _input.Player.Jump.performed -= Jump;
        _input.Player.Fire.performed -= Fire;
        _input.Player.StartShooting.performed -= StartShooting;
    }
    private void StartShooting(InputAction.CallbackContext context)
    {
        if (_isStanding) return;

        if (_camera.ScreenToWorldPoint(_input.Player.Tap.ReadValue<Vector2>()).x < _boundaryX) return;
        _isShootStarted = true;
        IsStance?.Invoke();
        if (_trajectory != null)
        {
            _trajectory.Show();
        }
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
        if (_trajectory != null)
        {
            _trajectory.Hide();
        }

        IsShot?.Invoke(targetPosition, timing);
    }

    private void Jump(InputAction.CallbackContext context) => IsJumped?.Invoke();

    private void ChangeControls()
    {
        if (_speedControlService.Multiply == 0)
        {
            _isStanding = true;
        }
        else
        {
            _isStanding = false;
        }
    }
}
