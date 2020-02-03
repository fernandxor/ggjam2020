using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 1f;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float sunFactor = 1f;
    [SerializeField] Transform hands;
    [SerializeField] Car car;
    float health = 100f;

    Camera cam;

    bool isNearCar = false;
    bool isAlive = true;
    bool canMount = false;
    bool isDriving = false;

    SpriteRenderer sr;

    // Item en la mano
    Pickable picked;
    // Item en el suelo
    Pickable pickable;

    bool canControl;

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


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        canControl = true;
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (canControl)
        {
            if (!isDriving)
                ProcessMove();
        }
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
            }
        }
        else Debug.Log("Game Over!");

    }

  
    private void DamagePlayer()
    {
        Health -= Time.deltaTime * (picked==null?sunFactor:sunFactor*2f);
        //Debug.Log("Salud: " + health);
        if (Health <= 0f)
        {
            Health = 0;

            if (isDriving)
                ProcessUnmount();

            isAlive = false;
            GameLoop.GameOver();
        }
    }

    public void Soltar() 
    {
        picked = null;
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
                

                var slot = hit.collider.GetComponent<Slot>();

                if(!slot.IsPlugged)
                    CarInteractionHUD.SetPlugIcon(true);
                else
                    CarInteractionHUD.SetPlugIcon(false);

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

                if (pickable != null && !pickable.IsPlaced)
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
                sr.flipX = false;
                sr.sortingOrder = -10;
                isDriving = true;
                car.ProcessDriving();
                Rb.simulated = false;
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
            sr.sortingOrder = 10;
            car.ProcessDriving(false);
            transform.SetParent(null);
            Rb.simulated = true;
            Rb.velocity = car.Rb.velocity;
            transform.eulerAngles = Vector3.zero;
            Rb.MoveRotation(0);
            CameraFollow.GetInstance().ZoomIn(transform);
        }

    }



    private void ProcessMove()
    {

        float controlThrow = Input.GetAxis("Horizontal"); // value is betweeen -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * movSpeed * (picked==null? 1f : 0.7f), Rb.velocity.y);
        Rb.velocity = playerVelocity;

        //Vector2 newPos = new Vector2(transform.position.x + inputX * Time.deltaTime * movSpeed, transform.position.y - 4f * Time.deltaTime);
        //Rb.MovePosition(newPos);
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if (collider.gameObject.name.Equals("GameLoop"))
        {
            GameLoop.YouWin();
            canControl = false;
        }


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
