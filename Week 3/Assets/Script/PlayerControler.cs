using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    private float MoveSpeed = 5f;
    private float JumpPower = 10f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            Rigidbody2D.velocity = new Vector2(MoveSpeed, Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D.velocity = new Vector2(-MoveSpeed, Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpPower);
        }
        
    }
}
