using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable
{

    [SerializeField] float integrity = 100f;
    [SerializeField] float damageFactor = 0.05f;
    float lastRotation;

    
    void Start() 
    {
        lastRotation = Rb.rotation;
    }

    private void FixedUpdate()
    {
        integrity -= Mathf.Abs(lastRotation - Rb.rotation) * damageFactor;
        lastRotation = Rb.rotation;
        
    }

}
