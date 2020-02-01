using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    [SerializeField] Slot rearWheelSlot;
    [SerializeField]Slot frontWheelSlot;
    [SerializeField] Slot engineSlot;
    [SerializeField] Slot sailSlot;
    [SerializeField] Slot plowSlot;
    [SerializeField] Transform seat;

    Rigidbody2D rb;

    private void Awake()
    {
        rearWheelSlot.callBack = OnSlotPlugged;
        frontWheelSlot.callBack = OnSlotPlugged;
        engineSlot.callBack = OnSlotPlugged;
        sailSlot.callBack = OnSlotPlugged;
        plowSlot.callBack = OnSlotPlugged;

    }

    public Rigidbody2D Rb
    {
        get
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }

    public Transform Seat { get => seat; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSlotPlugged(Pickable pickable, Slot slot)
    {
        Debug.Log("Entro en OnSlotPlugged");
        if (pickable is Wheel && ReferenceEquals(slot, rearWheelSlot)) {
            Debug.Log("Pongo rueda trasera");

        } else if (true) 
        {

        }
        //else if (pickable is ) { }

        // conectar al wheel collider
        else Debug.Log("Pongo otra cosa");
        //if (pickable is Sail)

    }
}
