using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour, Pluggable
{

    public System.Action<Pickable, Slot> callBack;

    public void Plug(Pickable picked)
    {
        Debug.Log("Entro en Plug");
        callBack?.Invoke(picked, this);
    }

    public void Unplug()
    {
        Debug.Log("Entro en unplug");
        //callBack?.Invoke(picked, this);
    }
    
}
