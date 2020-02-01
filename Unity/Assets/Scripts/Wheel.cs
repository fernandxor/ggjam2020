using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable, Pluggable
{
    public override void Pick(Transform grabPos)
    {
        base.Pick(grabPos);
        Debug.Log("[Wheel] He sido recogido");
    }

    public void Plug()
    {
        
    }

    public void Unplug()
    {
        
    }
}
