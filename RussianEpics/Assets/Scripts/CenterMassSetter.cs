using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMassSetter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 mass;

    void Update()
    {
        rb.centerOfMass = mass;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rb.worldCenterOfMass, 0.1f);
    }
}
