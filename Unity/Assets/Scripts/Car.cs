using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    [SerializeField] Slot rearWheelSlot;
    [SerializeField] Slot frontWheelSlot;
    [SerializeField] Slot engineSlot;
    [SerializeField] Slot sailSlot;
    [SerializeField] Slot plowSlot;
    [SerializeField] Transform seat;

    [SerializeField] GameObject sail;

    Rigidbody2D rb;

    WheelJoint2D[] wheelJoints;

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
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();      
    }


    private void FixedUpdate()
    {
        
    }

    public void ProcessDriving(bool drive = true)
    {
    
        WheelJoint2D[] ms = GetComponents<WheelJoint2D>();
        ms[0].useMotor = drive;
        ms[1].useMotor = drive;


    }

    private void PruebasCoche()
    {
        WheelJoint2D[] wcs = gameObject.GetComponents<WheelJoint2D>();
        var wc1 = wcs[0];
        var wc2 = wcs[1];
        // Interaccion Hombre Maquina
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Rigidbody2D rb = wc1.connectedBody;
            wc1.connectedBody = null;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Rigidbody2D rb = wc2.connectedBody;
            wc2.connectedBody = null;
        }
        

    }

    void OnSlotPlugged(Pickable pickable, Slot slot)
    {


        if (pickable is Wheel && ReferenceEquals(slot, rearWheelSlot)) {

            //Debugpickable.transform.parent.name;
            pickable.Drop();
            pickable.transform.SetParent(slot.transform);
            pickable.transform.localPosition = Vector3.zero;
            wheelJoints[1].connectedBody = pickable.Rb;
           

            
        }
        else if (pickable is Wheel && ReferenceEquals(slot, frontWheelSlot))
        {

            pickable.Drop();
            pickable.transform.SetParent(slot.transform);
            pickable.transform.localPosition = Vector3.zero;
            wheelJoints[0].connectedBody = pickable.Rb;

        }
        else if (pickable is Engine && ReferenceEquals(slot, engineSlot))
        {


            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            pickable.Drop();
            pickable.Rb.isKinematic = true;
            pickable.transform.SetParent(engineSlot.transform);
            pickable.transform.localPosition = Vector2.zero;

        }
        else if (pickable is Sail && ReferenceEquals(slot, sailSlot))
        {

            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            /*
            pickable.Drop();
            pickable.Rb.isKinematic = true;
            pickable.transform.localEulerAngles = Vector3.zero;
            pickable.transform.SetParent(sailSlot.transform);
            pickable.transform.localPosition = Vector2.zero;
            */
            Destroy(pickable.gameObject);
            sail.SetActive(true);


        }
        else if (pickable is Plow && ReferenceEquals(slot, plowSlot))
        {

        }
        else if (pickable is Gas && ReferenceEquals(slot, engineSlot))
        {

            if (slot.transform.childCount > 0) 
            {
                //slot.transform.GetChild(0).GetComponent<Engine>().Use();
            } 
        }
        
    }
}
