using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject background;
    public GameObject camFollow;
    public float camDistance;
    public float camPosition;
    float playerY;
    float camSpeed;
    public float minCamSpeed = 0.3f;
    public float maxCamSpeed = 0.7f;
    float maxDistance = 0;
    bool inPos = true;

    // Start is called before the first frame update
    void Start()
    {
        playerY = player.transform.position.y;
        camFollow.transform.position = new Vector3(player.transform.position.x, playerY + camPosition, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        string playerOrientation = GameObject.Find("Player").GetComponent<characterController>().orientation;

        Vector2 playerPos = new Vector3(player.transform.position.x, playerY + camPosition, player.transform.position.z);
        Vector2 newCamPos = new Vector2(camFollow.transform.position.x, transform.position.y);
        Vector2 bgMin = background.GetComponent<BoxCollider2D>().bounds.min;
        Vector2 bgMax = background.GetComponent<BoxCollider2D>().bounds.max;

        float maxX = bgMax.x;
        float minX = bgMin.x;
        float maxY = bgMax.y;
        float minY = bgMin.y;

        if (playerPos.x < maxX && playerPos.x > minX)
        {
            newCamPos.x = playerPos.x;
        }

        if (playerPos.y < maxY && playerPos.y > minY)
        {
            newCamPos.y = playerPos.y;
        }

        if(playerOrientation == "Right")
        {
            newCamPos.x += camDistance;
        }
        else
        {
            newCamPos.x -= camDistance;
        }

        Vector3 target = new Vector3(newCamPos.x, newCamPos.y, camFollow.transform.position.z);
        float distance = Mathf.Abs(camFollow.transform.position.x - target.x);

        if (distance > maxDistance)
        {
            maxDistance = distance;
        }

        camSpeed = distance / maxDistance / 10;

        if (playerPos.x < maxX && playerPos.x > minX)
        {
            camFollow.transform.position = Vector3.Lerp(camFollow.transform.position, target, Mathf.Clamp(camSpeed, minCamSpeed, maxCamSpeed));
        }
    }
}