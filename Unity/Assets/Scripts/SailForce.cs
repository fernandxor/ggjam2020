using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailForce : MonoBehaviour
{

    [SerializeField]
    float force = 10;

    [SerializeField]
    Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * force, ForceMode2D.Force);
    }

}
