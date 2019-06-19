using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFadeOut : MonoBehaviour
{
    public Image splashImage;

    float counter = 0;
    bool ending = false;

    private void Update()
    {
        if(ending == true)
        {
            counter += Time.deltaTime;
        }

        if(counter >= 2f)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            ending = true;

            splashImage.CrossFadeAlpha(2.5f, 1.5f, false);
        }
    }
}
