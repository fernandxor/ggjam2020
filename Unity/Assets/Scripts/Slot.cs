using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slot : MonoBehaviour, Pluggable
{

    public System.Action<Pickable, Slot> callBack;

    public bool IsPlugged { get; set; }  

    public void Plug(Pickable picked)
    {
        callBack?.Invoke(picked, this);
    }

   
    
}
