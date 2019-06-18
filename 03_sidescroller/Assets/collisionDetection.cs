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
            shake.CamShake();

            float direction = 0f;

            if(player.transform.position.x < col.gameObject.transform.position.x)
            {
                direction = 2f;
            }
            else
            {
                direction = -2f;
            }
            col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(direction, 1f, 0f);
        }
    }
}
