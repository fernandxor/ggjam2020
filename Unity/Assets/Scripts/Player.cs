using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 2f;
    [SerializeField] float playerHeight = 2f;

    Rigidbody2D rb;
    float inputX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    Rigidbody2D Rb
    {
        get
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessMove();
        ProcessAction();
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    private void ProcessAction()
    {



    }

    private void ProcessMove()
    {
        

        Vector2 newPos = new Vector2(transform.position.x + inputX * Time.deltaTime * movSpeed, transform.position.y);
        Rb.MovePosition(newPos);
    }



}
