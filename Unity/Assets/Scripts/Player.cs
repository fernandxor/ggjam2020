using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 1f;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float health = 10000f;
    [SerializeField] float sunFactor = 1f;
    [SerializeField] Transform hands;
    [SerializeField] Car car;

    Camera cam;

    bool isNearCar = false;
    bool isAlive = true;
    bool canMount = false;
    bool isDriving = false;

    // Item en la mano
    Pickable picked;
    // Item en el suelo
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

    public float Health { get => health; private set => health = value; }

    private void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if(!isDriving)
            ProcessMove();
        else
            car.ProcessDriving();

    }

    private void Update()
    {
        //PruebasJugador();
        if (isAlive)
        {

            DamagePlayer();
            if (!isDriving)
            {
                
                ProcessAction();
                ProcessMount();
              //  ProcessIcons();

            }
            else
            {
                ProcessUnmount();
                ProcessRepair();
            }
        }
        else Debug.Log("Game Over!");

    }

    private void PruebasJugador()
    {
        // Interaccion Hombre Maquina
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        }
    }

    
    private void ProcessRepair()
    {

    }
    
    private void DamagePlayer()
    {
        Health -= Time.deltaTime * sunFactor;
        //Debug.Log("Salud: " + health);
        if (Health <= 0f)
        {
            isAlive = false;
        }
    }

    public void Soltar() 
    {
        picked = null;
    }

    private void ProcessIcons()
    {
        // Interaccion Hombre Maquina
        if (isNearCar)
        {
            int layer_mask = LayerMask.GetMask("Slot");

            RaycastHit2D hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, Vector3.forward, 1f, layer_mask);

            if (hit != false)
            {

                CarInteractionHUD.SetPlugIcon(true);



                var slot = hit.collider.GetComponent<Slot>();


                if (Input.GetMouseButtonDown(0))
                {

                    if (slot != null)
                    {

                        slot.Plug(picked);
                    }

                }
                // Do something with the object that was hit by the raycast.
            }
            else
                CarInteractionHUD.SetPlugIcon(false);

        }
    }

    private void ProcessAction()
    {

        //CAR
        // Interaccion Hombre Maquina
        if (isNearCar)
        {
            int layer_mask = LayerMask.GetMask("Slot");

            RaycastHit2D hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, Vector3.forward, 1f, layer_mask);

            if (hit != false)
            {

                CarInteractionHUD.SetPlugIcon(true);

                var slot = hit.collider.GetComponent<Slot>();


                if (Input.GetMouseButtonDown(0))
                {

                    if (slot != null)
                    {
                        slot.Plug(picked);
                    }

                }
                // Do something with the object that was hit by the raycast.
            }
            else
                CarInteractionHUD.SetPlugIcon(false);
        }
                
        //ITEMS
        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("Picked: " + picked + " /  Pickable: " + pickable);

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
                CarInteractionHUD.SetVisible(false);

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
            CarInteractionHUD.SetVisible(true);
            isDriving = false;
            transform.SetParent(null);
            Rb.isKinematic = false;
            Rb.velocity = car.Rb.velocity;
            transform.eulerAngles = Vector3.zero;
            Rb.MoveRotation(0);
            car.ProcessDriving(false);
            CameraFollow.GetInstance().ZoomIn(transform);
        }

    }



    private void ProcessMove()
    {

        float controlThrow = Input.GetAxis("Horizontal"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * movSpeed, Rb.velocity.y);
        Rb.velocity = playerVelocity;

        //Vector2 newPos = new Vector2(transform.position.x + inputX * Time.deltaTime * movSpeed, transform.position.y - 4f * Time.deltaTime);
        //Rb.MovePosition(newPos);
    }

    void OnTriggerEnter2D(Collider2D collider) {

        //if(collider.gameObject.name.Equals("Goal"))
            


        if (collider.CompareTag("Zoom"))
        {

            isNearCar = true;
            CameraFollow.GetInstance().ZoomIn(collider.transform);
            CarInteractionHUD.SetVisible(true);
        }

        if (collider.CompareTag("Car"))
        {
            canMount = true;           

            
            return;
        }
        
        var p = collider.GetComponentInParent<Pickable>();


        if (p!= null) {
            pickable = p;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.CompareTag("Zoom"))
        {
            isNearCar = false;
            CameraFollow.GetInstance().ZoomOut(transform);
            CarInteractionHUD.SetVisible(false);
        }

        if (collider.CompareTag("Car"))
        {
            canMount = false;
            return;
        }

        var p = collider.GetComponentInParent<Pickable>();


        if (ReferenceEquals(p, pickable))
        {
            pickable = null;
        }
    }


}
