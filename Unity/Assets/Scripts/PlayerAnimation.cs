using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    SpriteRenderer sr;
    Animator anim;

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if(Mathf.Abs(rb.velocity.x) > 0.1f)
            sr.flipX = rb.velocity.x < 0;
    }
}
