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
            
        }
        if (pickable is Wheel && ReferenceEquals(slot, frontWheelSlot))
        {
            Debug.Log("Pongo rueda delantera");
        }
        else if (pickable is Engine && ReferenceEquals(slot, engineSlot))
        {
            Debug.Log("Pongo motor");
        }
        else if (pickable is Sail && ReferenceEquals(slot, sailSlot))
        {
            Debug.Log("Pongo vela");
        }
        else if (pickable is Plow && ReferenceEquals(slot, plowSlot))
        {
            Debug.Log("Pongo quitanieves");
        }
        else if (pickable is Gas && ReferenceEquals(slot, engineSlot))
        {
            Debug.Log("Recargo deposito");
        }
        
    }
}
