using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 2f;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] Transform hands;
    [SerializeField] Car car;

    bool canMount = true;
    bool isDriving = false;

    Pickable picked;

    Pickable pickable;


    Rigidbody2D rb;
    float inputX;

    Rigidbody2D Rb
    {
        get
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }

    void FixedUpdate()
    {
        if (!isDriving)
        {
            ProcessMove();
            ProcessAction();
            ProcessMount();

        }
        else {
            ProcessDriving();
            ProcessUnmount();
        }
        
        
    }

    private void ProcessDriving()
    {  

    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    private void ProcessAction()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (picked != null)
            {
                picked.Drop();
                picked = null;
            }
            else
            {

                if (pickable != null)
                {
                    pickable.Pick(hands);
                    picked = pickable;
                }


            }
        }

        

    }

    private void ProcessMount()
    {
        if (canMount)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isDriving)
            {
                Debug.Log("Me subo al coche");
                isDriving = true;

                Rb.isKinematic = true;
                Rb.velocity = Vector2.zero;
                transform.SetParent(car.Seat);
                transform.localPosition = Vector2.zero;

            }
        }
    }

    private void ProcessUnmount()
    {  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Me bajo del coche");
            isDriving = false;
            transform.SetParent(null);
            Rb.isKinematic = false;
            Rb.velocity = car.Rb.velocity;
            transform.eulerAngles = Vector3.zero;
            Rb.MoveRotation(0);
        }
        
    }

    private void ProcessMove()
    {
        

        Vector2 newPos = new Vector2(transform.position.x + inputX * Time.deltaTime * movSpeed, transform.position.y - 4f * Time.deltaTime);
        Rb.MovePosition(newPos);
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if (collider.CompareTag("Car"))
        {
            canMount = true;

            Debug.Log("Coche");
            return;
        }

        var p = collider.GetComponentInParent<Pickable>();

        Debug.Log(p);

        if (p!= null) {
            pickable = p;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.CompareTag("Car"))
        {
            canMount = false;

            Debug.Log("Coche");
            return;
        }

        var p = collider.GetComponentInParent<Pickable>();


        if (ReferenceEquals(p, pickable))
        {
            pickable = null;
        }
    }


}
