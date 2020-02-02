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

        
    

    // Update is called once per frame
    void Update()
    {
       // PruebasCoche();
    }

    private void FixedUpdate()
    {
        
    }

    public void ProcessDriving()
    {
        float deltaX = 10f * Input.GetAxis("Horizontal");
        Rb.AddTorque(deltaX);
        float deltaY = 10f * Input.GetAxis("Vertical");
        //wheelJoints[1].motor.motorSpeed(deltaY;
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
        Debug.Log("Entro en OnSlotPlugged");
        if (pickable is Wheel && ReferenceEquals(slot, rearWheelSlot)) {

            //Debugpickable.transform.parent.name;
            pickable.Drop();
            pickable.Rb.MovePosition(slot.transform.position);
            wheelJoints[1].connectedBody = pickable.Rb;
            
           
            Debug.Log("Pongo rueda trasera");
            
        }
        else if (pickable is Wheel && ReferenceEquals(slot, frontWheelSlot))
        {
            Debug.Log("Pongo rueda delantera");
            pickable.Drop();
            pickable.Rb.MovePosition(slot.transform.position);
            wheelJoints[0].connectedBody = pickable.Rb;

        }
        else if (pickable is Engine && ReferenceEquals(slot, engineSlot))
        {
            Debug.Log("Pongo motor");
            Debug.Log("Leer aqui " + pickable.transform.parent.parent);
            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            pickable.Drop();
            pickable.Rb.isKinematic = true;
            pickable.transform.SetParent(engineSlot.transform);
            pickable.transform.localPosition = Vector2.zero;
            //pickable.Pick(slot.transform);

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
            Debug.Log("Quiero recargar deposito");
            if (slot.transform.childCount > 0) 
            {
                //slot.transform.GetChild(0).GetComponent<Engine>().Use();
            } 
        }
        
    }
}
