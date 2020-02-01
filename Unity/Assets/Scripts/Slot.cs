using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    public System.Action<Pickable, Slot> callBack;

    public void Plug()
    {
        callBack?.Invoke(null, this);
    }

    public void Unplug()
    {

    }
    
}
