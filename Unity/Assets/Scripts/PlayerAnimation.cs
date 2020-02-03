using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    SpriteRenderer sr;
    Animator anim;

    Rigidbody2D rb;
    Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {

        while (player.Health > 0)
        {

            anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
                sr.flipX = rb.velocity.x < 0;

            yield return null;
        }

        anim.SetBool("dead", true);

    }
}
