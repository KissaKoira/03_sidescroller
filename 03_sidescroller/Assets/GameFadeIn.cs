using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFadeIn : MonoBehaviour
{
    public Image splashImage;

    void Start()
    {
        splashImage.canvasRenderer.SetAlpha(2.5f);

        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
