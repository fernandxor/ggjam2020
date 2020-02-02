using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable
{

    [SerializeField] float integrity = 100f;
    [SerializeField] float damageFactor = 0.05f;
    float lastRotation;

    public override void Pick(Transform grabPos)
    {
        base.Pick(grabPos);
        Debug.Log("[Wheel] He sido recogido");
    }
 
    void Start() 
    {
        lastRotation = Rb.rotation;
    }

    private void FixedUpdate()
    {
        Debug.Log("[Salud Rueda]: " + integrity);
        integrity -= Mathf.Abs(lastRotation - Rb.rotation) * damageFactor;
        lastRotation = Rb.rotation;
        
    }

}
