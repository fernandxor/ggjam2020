using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slot : MonoBehaviour, Pluggable
{

    public enum SlotType { Wheel, Sail, Engine, Plow }

    [SerializeField] SlotType slotType;

    GameObject child;

    public GameObject Child { get => child; set => child = value; }

    public System.Action<Pickable, Slot> callBack;


    void Start()
    {
        var childs = transform.childCount;
        if (childs > 0)
        {
            Child = transform.GetChild(0).gameObject;
            Debug.Log(this.name + " tiene un hijo: " + Child.name);
        }
        else {
            Debug.Log(this.name + " no tiene hijos.");
        }
    }

    public void Plug(Pickable picked)
    {

        if (picked.Compare)

        Debug.Log("Entro en Plug");
        callBack?.Invoke(picked, this);
    }

    public void Unplug()
    {
        Debug.Log("Entro en unplug");
        //callBack?.Invoke(picked, this);
    }
    
}
