using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : Pickable
{

    [SerializeField] float capacity = 20f;

    public float Capacity { get => capacity; set => capacity = value; }

    public override void Pick(Transform grabPos)
    {
        base.Pick(grabPos);
        Debug.Log("[Gasolina] He sido recogido");
    }


}
