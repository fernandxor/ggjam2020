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
    [SerializeField] ParticleSystem smokePS;

    Rigidbody2D rb;

    WheelJoint2D[] wheelJoints;

    bool hasMotor;

    private void Awake()
    {
        rearWheelSlot.callBack = OnSlotPlugged;
        frontWheelSlot.callBack = OnSlotPlugged;
        engineSlot.callBack = OnSlotPlugged;
        sailSlot.callBack = OnSlotPlugged;
        plowSlot.callBack = OnSlotPlugged;

        WheelJoint2D[] wheelJoints = GetComponents<WheelJoint2D>();
        
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
    
    public void ProcessDriving(bool drive = true)
    {
        if (!hasMotor)
            return;

        if(drive)
            smokePS.Play();
        else
            smokePS.Stop();

        if (wheelJoints[0].attachedRigidbody != null)
            wheelJoints[0].useMotor = drive;

        if (wheelJoints[1].attachedRigidbody != null)
            wheelJoints[1].useMotor = drive;

    }

    void OnSlotPlugged(Pickable pickable, Slot slot)
    {

        if (pickable is Wheel && ReferenceEquals(slot, rearWheelSlot) && !rearWheelSlot.IsPlugged) {


            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            pickable.IsPlaced = true;
            pickable.transform.SetParent(slot.transform);
            pickable.transform.localPosition = Vector3.zero;
            pickable.Rb.simulated = true;
            wheelJoints[1].connectedBody = pickable.Rb;
            slot.IsPlugged = true;

        }
        else if (pickable is Wheel && ReferenceEquals(slot, frontWheelSlot) && !frontWheelSlot.IsPlugged)
        {

            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            pickable.IsPlaced = true;
            pickable.transform.SetParent(slot.transform);
            pickable.transform.localPosition = Vector3.zero;
            pickable.Rb.simulated = true;
            wheelJoints[0].connectedBody = pickable.Rb;
            slot.IsPlugged = true;

        }
        else if (pickable is Engine && ReferenceEquals(slot, engineSlot))
        {

            hasMotor = true;
            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            pickable.Rb.simulated = false;
            pickable.transform.SetParent(engineSlot.transform);
            pickable.transform.localPosition = Vector2.zero;
            slot.IsPlugged = true;
            pickable.IsPlaced = true;

        }
        else if (pickable is Sail && ReferenceEquals(slot, sailSlot))
        {

            pickable.transform.parent.GetComponentInParent<Player>().Soltar();
            Destroy(pickable.gameObject);
            sail.SetActive(true);
            slot.IsPlugged = true;

        }
        /*
        else if (pickable is Plow && ReferenceEquals(slot, plowSlot))
        {

        }
        else if (pickable is Gas && ReferenceEquals(slot, engineSlot))
        {

        }
        */
        
    }
}
