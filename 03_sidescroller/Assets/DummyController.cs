using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public GameObject DummyBody;
    public GameObject DummyHead;

    // Start is called before the first frame update
    void Start()
    {
        DummyBody.SetActive(false);
        DummyHead.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);

        if (col.name == "StrikeHitBox")
        {
            gameObject.SetActive(false);
            DummyBody.SetActive(true);
            DummyHead.SetActive(true);

            DummyHead.GetComponent<Rigidbody2D>().velocity = new Vector3(4f, 2f, 0f);
        }
    }
}
