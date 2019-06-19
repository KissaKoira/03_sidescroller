using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    public GameObject player;
    private shake shake;

    // Start is called before the first frame update
    void Start()
    {
        shake = GameObject.Find("Shake Manager").GetComponent<shake>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if(col.name != "Dummy")
            {
                shake.CamShake();

                float direction = 0f;
                float angularVel = 0f;

                if (player.transform.position.x < col.gameObject.transform.position.x)
                {
                    direction = 2f;
                    angularVel = -100f;
                }
                else
                {
                    direction = -2f;
                    angularVel = 100f;
                }
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(direction, 2f, 0f);
                col.gameObject.GetComponent<Rigidbody2D>().angularVelocity = angularVel;
            }
        }
    }
}
