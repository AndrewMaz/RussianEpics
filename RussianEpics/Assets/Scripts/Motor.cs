using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void FixedUpdate()
    {
        transform.localPosition += _speed * Time.fixedDeltaTime * Vector3.left;
    }
}
