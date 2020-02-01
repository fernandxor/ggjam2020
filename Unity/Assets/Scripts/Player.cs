﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMove();
        ProcessAction();
    }

    private void ProcessAction()
    {
       
    }

    private void ProcessMove()
    {
        var movement = Input.GetAxis("Horizontal");
        Vector2 newPos = new Vector2(transform.position.x + movement * movSpeed,transform.position.y);

    }



}