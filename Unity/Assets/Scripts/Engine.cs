using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Pickable
{
    [SerializeField] float capacity = 20f;

    public float Capacity { get => capacity; set => capacity = value; }

    public override bool IsPlaced { set { base.IsPlaced = value;GetComponent<SpriteRenderer>().sortingOrder = -10; } }

    public override void Pick(Transform grabPos)
    {
        base.Pick(grabPos);
        Debug.Log("[Motor] He sido recogido");
    }

    public void Fill(Gas pickable)
    {
        Debug.Log("[Motor] llenando deposito");
        Capacity += pickable.Capacity;
        Destroy(pickable);
    }

}
