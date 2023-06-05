using Assets.Scripts.Interfaces;
using SpawnElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour, IDamageable
{
    [SerializeField] protected DamageArea _damageArea;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _deflectChance;

    private readonly int damage = 1;

    protected bool isRotating = true;

    protected Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Fly(Vector2 from, Vector2 to, float force)
    {
        transform.position = from + new Vector2(0, 1);
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0.0f;

        Vector2 fireVector = to - from;
        Vector2 groundVector = new Vector2(0, from.y) - from;
        var angle = Vector2.Angle(fireVector, groundVector);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        _rb.AddForce(fireVector.normalized * force);
    }

    private void Update()
    {
        if (isRotating)
        {
            var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnEnable()
    {
        _damageArea.IsDamageDealt += OnIsDamageDealt;
        isRotating = true;
    }

    private void OnDisable()
    {
        _damageArea.IsDamageDealt -= OnIsDamageDealt;
    }

    protected virtual void OnIsDamageDealt(IDamageable target)
    {
        target.GetDamage(damage, this);
    }

    public void GetDamage(int damage, object sender)
    {
        if ((sender is Ghoul && Random.value < _deflectChance/100) || (sender is Kolobok && _deflectChance >= 0))
        {
            Deflect();
        }
    }
    public void Deflect()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetTrigger("deflected");
        _damageArea.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            _rb.bodyType = RigidbodyType2D.Static;
            isRotating = false;
            _damageArea.gameObject.SetActive(false);

            return;
        }
        gameObject.SetActive(false);
    }
}
