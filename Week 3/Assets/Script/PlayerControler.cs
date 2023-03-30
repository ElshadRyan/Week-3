using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float JumpPower = 10f;
    [SerializeField] private int Cherry = 0;
    [SerializeField] private TextMeshProUGUI TextNummber;
    [SerializeField] private float HurtForce = 10f;
    [SerializeField] private AudioSource Footstep;
    [SerializeField] private AudioSource CherrySound;

    private Collider2D Collider2D;
    private enum State { Idle, IsRun, IsJump, Falling, Hurt }
    private State state = State.Idle;

    private void Start()
    {
        Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (state != State.Hurt)
        {
            Moving();
        }

        AnimationState();
        animator.SetInteger("State", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            CherrySound.Play();
            Destroy(collision.gameObject);
            Cherry++;
            TextNummber.text = Cherry.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.Falling)
            {
                enemy.Death();
                Jump();
            }
            else if (state != State.Falling)
            {
                state = State.Hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    
                    Rigidbody2D.velocity = new Vector2(-HurtForce, Rigidbody2D.velocity.y);
                }
                else
                {
                    Rigidbody2D.velocity = new Vector2(HurtForce, Rigidbody2D.velocity.y);
                }
            }
            
        }
    }

    private void Moving()
    {
        float Direction = Input.GetAxis("Horizontal");

        if (Direction > 0)
        {
            Rigidbody2D.velocity = new Vector2(MoveSpeed, Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        else if (Direction < 0)
        {
            Rigidbody2D.velocity = new Vector2(-MoveSpeed, Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }


        if (Input.GetButtonDown("Jump") && Collider2D.IsTouchingLayers(Ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpPower);
        state = State.IsJump;
    }
    private void AnimationState()
    {
        if (state == State.IsJump)
        {
            if(Rigidbody2D.velocity.y < .1f)
            {
                state = State.Falling;
            }
        }
        else if (state == State.Falling)
        {
            if (Collider2D.IsTouchingLayers(Ground))
            {
                state = State.Idle;
            }
        }
        else if (state == State.Hurt)
        {
            if (Mathf.Abs(Rigidbody2D.velocity.x) < 0.1f)
            {
                state = State.Idle;
            }
        }
        else if(Mathf.Abs(Rigidbody2D.velocity.x) > 2.3f)
        {
            state = State.IsRun;
        }
        else
        {
            state = State.Idle;
        }
    }

    private void FootstepSound()
    {
        Footstep.Play();
    }
}
