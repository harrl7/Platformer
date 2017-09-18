using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Level
    public Room room;
    private Game Game = Game.instance;

    // Movement controllers
    public float maxSpeed = 5f;
    public float jumpForce = 20f;
    public float jumpSpeed = 20f;
    public int TotalExtraJumps;
    private int extraJumps;
    private bool facingRight = true;

    // Ground check
    public Transform GroundCheck;
    public LayerMask Ground;
    private bool grounded;

    // Components
    private Animator anim;
    private Rigidbody2D rb2d;

    // Attack
    public GameObject oAttack;

    // Input
    private float inp_xAxis;
    private bool inp_jump = false;
    private bool inp_attack = false;

    // Use this for initialization
    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Movement
        extraJumps = TotalExtraJumps;
    }

    void Update()
    {
        // Fix rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }

 
    // === Update is called once per frame ===
    void FixedUpdate()
    {
        getInput();

        // Move
        checkIfGrounded();
        move();
        attack();

        // Animate
        anim.SetFloat("Speed", Mathf.Abs(inp_xAxis));
        flip();     
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        // Die
        if (coll.gameObject.tag == "Enemy")
        {
            // rb2d.position = level.Respawn.position;
            // rb2d.velocity = new Vector2(0, 0);

            Game.instance.SpawnPlayer();
            Destroy(gameObject);

        }

        // Exit level
        if (coll.gameObject.tag == "Exit")
        {
            Game.ChangeRoom(coll.gameObject.GetComponent<RoomExit>());
            rb2d.position = Game.instance.room.Respawn.position;
            rb2d.velocity = new Vector2(0, 0);
        }
    }


    // === Get input ===
    void getInput()
    {
        inp_xAxis = Input.GetAxis("Horizontal");
        inp_jump = Input.GetButtonDown("Jump");
        inp_attack = Input.GetButtonDown("Fire1");
    }


    // Flip player sprite
    void flip()
    {
        if((inp_xAxis > 0 && !facingRight) || (inp_xAxis < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }


    // === Ground check ===
    void checkIfGrounded()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground);
        anim.SetBool("Ground", grounded);
        if (grounded) extraJumps = TotalExtraJumps;
    }

    // === Attack ===
    void attack()
    {
        if(inp_attack)
        {
            int dirOffset = 1 * (facingRight ? 1 : -1);
            Vector2 pos = new Vector2(this.transform.position.x + dirOffset, this.transform.position.y);

            Blast blast = Instantiate(oAttack, pos, Quaternion.identity).GetComponent<Blast>();

            if (!facingRight)    // Flip shot to match player
            {
                Vector3 newScale = blast.GetComponent<Transform>().localScale;
                newScale.x *= -1;
                blast.GetComponent<Transform>().localScale = newScale;
            }
            
        }
    }

    // Move player
    void move()
    {
        // Player X movement
        rb2d.velocity = new Vector2(inp_xAxis * maxSpeed, rb2d.velocity.y);

        // Jump
        if (inp_jump)
        {
            if(grounded)
            {
                anim.SetTrigger("Jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            }
            else if(extraJumps > 0)
            {
                anim.SetTrigger("Jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                extraJumps--;
            }    
        }

        // Limit to max speed
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
    }

}

