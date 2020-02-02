﻿using System.Collections;
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

        }
        else {

        }
    }

    public void Plug(Pickable picked)
    {

       // if (picked.Compare)


        callBack?.Invoke(picked, this);
    }

    public void Unplug()
    {

        //callBack?.Invoke(picked, this);
    }
    
}
