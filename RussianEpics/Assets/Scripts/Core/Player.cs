using Abstracts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using UnityEngine;


public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private Animator _animator;
    
    private const float _rayDistance = 1f;
    private Rigidbody2D _rb;

    private PlayerCharacteristicsService _playerCharacteristics;
    private IPlayerInput _input;

    public Animator Animator { get { return _animator; } }

    public void Initialize(IPlayerInput input, PlayerCharacteristicsService playerCharacteristics)
    {
        _input = input;
        _playerCharacteristics = playerCharacteristics;
        enabled = true;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _input.IsStance += Stance;
        _input.IsShot += Fire;
        _input.IsJumped += Jump;
        _playerCharacteristics.IsPlayerDamaged += Damaged;
        _playerCharacteristics.IsPlayerDead += Dead;
    }
    private void OnDisable()
    {
        _input.IsStance -= Stance;
        _input.IsShot -= Fire;
        _input.IsJumped -= Jump;
        _playerCharacteristics.IsPlayerDamaged -= Damaged;
        _playerCharacteristics.IsPlayerDead -= Dead;
    }
    private void Update()
    {
        if (gameObject.transform.position.y <= -12f)
        {
            GetDamage(500, this);
        }
    }
    private void Fire(Vector2 firePosition, float time)
    {
        if (time == 1f)
        {
            _animator.SetBool("hammerThrow", true);
        }
        _playerCharacteristics.Attack(firePosition, time);
        _animator.SetBool("isStance", false);
    }
    private void Jump()
    {  
        if (IsGrounded)
            _rb.AddForce(new Vector2(0, _jumpForce));
    }
    private bool IsGrounded => Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _floorMask);

    public void GetDamage(int damage, object sender)
        => _playerCharacteristics.GetDamage(damage, sender);
    private void Stance()
    {
        _animator.SetTrigger("attack");
        _animator.SetBool("hammerThrow", false);
        _animator.SetBool("isStance", true);
    }
    private void Damaged(int damage)
    {
        _animator.SetTrigger("takeDamage");
    }
    private void Dead()
    {
        _animator.SetTrigger("die");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rune rune))
        {
            _playerCharacteristics.UseRune(rune);
            rune.gameObject.SetActive(false);
        }
    }
}
