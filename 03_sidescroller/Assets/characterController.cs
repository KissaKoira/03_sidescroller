using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public float maxSpeed;
    public float smoothing = 5;
    public float jumpForce = 2;
    public float playerSlashSlow = 0;
    public string orientation = "Right";
    float speed = 0;
    public GameObject strikeHitBox;
    bool striking = false;
    float strikeCounter = 0;
    float strikeMomentum;

    private void Start()
    {
        strikeHitBox.SetActive(false);
    }

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
        animator.SetBool("Jumping", true);

        Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        if (Mathf.Abs(playerVelocity.y) < 0.1)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity.x, jumpForce);
        }
    }

    private void Strike()
    {
        strikeHitBox.SetActive(true);
        striking = true;
        animator.SetBool("Striking", true);
        strikeMomentum = GetComponent<Rigidbody2D>().velocity.x;
    }

    private void Flip(string str)
    {
        if (str != orientation)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Strike();
        }

        if(horizontal > 0)
        {
            if (striking == false)
            {
                Move("Right");
                Flip("Right");
                orientation = "Right";
            }
        }
        else if (horizontal < 0)
        {
            if (striking == false)
            {
                Move("Left");
                Flip("Left");
                orientation = "Left";
            }
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

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.1)
        {
            animator.SetBool("Jumping", false);
        }

        if (striking)
        {
            Vector3 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
            float newVelocity = playerVelocity.x;

            if(Mathf.Abs(playerVelocity.x) > playerSlashSlow * Time.fixedDeltaTime)
            {
                if (playerVelocity.x > 0)
                {
                    newVelocity = playerVelocity.x - (playerSlashSlow * Time.fixedDeltaTime);
                }
                else
                {
                    newVelocity = playerVelocity.x + (playerSlashSlow * Time.fixedDeltaTime);
                }
            }
            
            //use newVelocity for x velocity if player is not on horseback
            //use strikeMomentum for x velocity if player is on horseback
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(strikeMomentum, playerVelocity.y, 0f);

            strikeCounter += Time.fixedDeltaTime;
        }

        if(strikeCounter >= 0.5)
        {
            strikeHitBox.SetActive(false);
            striking = false;
            strikeCounter = 0;
            animator.SetBool("Striking", false);
        }
    }
}