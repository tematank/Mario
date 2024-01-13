using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 moveVector;
    public float speed = 10f;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    public Animator anim;
    public int jupmForce = 10;
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    public float GroundCheckRadius=0.03f;
    private Vector3 respawn;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawn = transform.position;
    }

    
    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x*speed,rb.velocity.y);
        anim.SetFloat("move",Mathf.Abs (moveVector.x));
        if (moveVector.x<0 && isFacingRight)
        {
            Flip();
        }
        if (moveVector.x > 0 && !isFacingRight)
        {
            Flip();
        }
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x,jupmForce);
            /*anim.SetBool("jump", onGround);*/
        }
        CheckingGround();
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
        transform.Rotate(0,180,0);
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position,GroundCheckRadius,Ground);
       /* anim.SetBool("jump",onGround);*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "deadZone")
        {
            transform.position = respawn;
        }
        else if (collision.tag == "checkpoint")
        {
            respawn = transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("platform"))
        {
            this.transform.parent = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("platform"))
        {
            this.transform.parent = collision.transform;
        }
    }
}
