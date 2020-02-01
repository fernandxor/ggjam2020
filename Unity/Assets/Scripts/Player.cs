using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 2f;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float health = 100f;
    [SerializeField] float sunFactor = 10f;
    [SerializeField] Transform hands;
    [SerializeField] Car car;
    bool isAlive = true;
    Camera cam;

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

    private void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {

            DamagePlayer();
            if (!isDriving)
            {
                ProcessMove();
                ProcessAction();
                ProcessMount();

            }
            else
            {
                ProcessDriving();
                ProcessUnmount();
                ProcessRepair();
            }
        }
        else Debug.Log("Game Over!");
       
    }

    private void ProcessDriving()
    {  

    }

    private void ProcessRepair()
    {

    }
    
    private void DamagePlayer()
    {
        health -= Time.deltaTime * sunFactor;
        Debug.Log("Salud: " + health);
        if (health <= 0f)
        {
            isAlive = false;
        }
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
                CameraFollow.GetInstance().ZoomOut();
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
            CameraFollow.GetInstance().ZoomIn(collider.transform);
            
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
            CameraFollow.GetInstance().ZoomOut(transform);
            return;
        }

        var p = collider.GetComponentInParent<Pickable>();


        if (ReferenceEquals(p, pickable))
        {
            pickable = null;
        }
    }


}
