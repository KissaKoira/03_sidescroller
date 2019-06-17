using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed;
    public float smoothing = 5;
    public float jumpForce = 2;
    public string orientation = "Right";
    float speed = 0;

    private void Move(string str)
    {
        float horizontal = 0;

        switch (str)
        {
            case "Right":
                horizontal = 1;
                break;
            case "Left":
                horizontal = -1;
                break;
        }

        if(speed < maxSpeed)
        {
            speed += smoothing;
        }

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, player.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Jump()
    {
        Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        if (playerVelocity.y == 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity.x, jumpForce);
        }
    }

    private void Flip(string str)
    {
        if (str == "Right")
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
        else if (str == "Left")
        {
            Vector3 temp = transform.localScale;
            temp.x = Mathf.Abs(temp.x);
            transform.localScale = temp;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(horizontal > 0)
        {
            Move("Right");
            Flip("Right");
            orientation = "Right";
        }
        else if (horizontal < 0)
        {
            Move("Left");
            Flip("Left");
            orientation = "Left";
        }
        else
        {
            Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

            speed = 0;

            if (playerVelocity.x > 0.1)
            {
                playerVelocity.x -= smoothing * Time.fixedDeltaTime;
            }
            else if (playerVelocity.x < -0.1)
            {
                playerVelocity.x += smoothing * Time.fixedDeltaTime;
            }
            else
            {
                playerVelocity.x = 0;
            }

            player.GetComponent<Rigidbody2D>().velocity = playerVelocity;
        }
    }
}