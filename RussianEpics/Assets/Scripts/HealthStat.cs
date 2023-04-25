using UnityEngine;

public class HealtStat : MonoBehaviour
{
    [SerializeField] private int health;

    public void Heal(int value)
    {
        health += Mathf.Abs(value);
    }

    public void Hit(int value)
    {
        health -= Mathf.Abs(value);
    }
}
