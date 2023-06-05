using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _multiply = 1f;
    public float Speed { get { return _speed; } set { _speed = value; } }
    public void FixedUpdate()
    {
        transform.localPosition += _speed * _multiply * Time.fixedDeltaTime * Vector3.left;
    }
    public void SetMultiply(float value)
    {
        _multiply = value;
    }
}
