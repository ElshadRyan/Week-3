using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float LeftWaypoint;
    [SerializeField] private float RightWaypoint;
    [SerializeField] private float JumpLength = 10f;
    [SerializeField] private float JumpHeight = 15f;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Rigidbody2D rigidbody;
    

    private bool FaceLeft = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetBool("Jump"))
        {
            if (rigidbody.velocity.y < 0.1f)
            {
                Debug.Log(rigidbody.velocity.y);
                animator.SetBool("Fall", true);
                animator.SetBool("Jump", false);
                Debug.Log("Jump");

            }
        }
        if (collider2D.IsTouchingLayers(Ground) && animator.GetBool("Fall"))
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Jump", false);
            Debug.Log("Land");
        }
    
}

    private void Move()
    {
        if (FaceLeft)
        {
            if (transform.position.x > LeftWaypoint)
            {
                if (transform.position.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (collider2D.IsTouchingLayers(Ground))
                {
                    rigidbody.velocity = new Vector2(-JumpLength, JumpHeight);
                    animator.SetBool("Jump", true);
                }
            }
            else
            {
                FaceLeft = false;
            }
        }
        else
        {
            if (transform.position.x < RightWaypoint)
            {
                if (transform.position.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (collider2D.IsTouchingLayers(Ground))
                {
                    rigidbody.velocity = new Vector2(JumpLength, JumpHeight);
                    animator.SetBool("Jump", true);
                }
            }
            else
            {
                FaceLeft = true;
            }
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    private void Explode()
    {
        Destroy(this.gameObject);
    }
}

