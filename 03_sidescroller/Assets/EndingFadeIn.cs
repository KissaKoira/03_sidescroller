using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingFadeIn : MonoBehaviour
{
    public GameObject panel;
    public Image splashImage;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(2.5f);
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
        yield return new WaitForSeconds(2.5f);

        panel.SetActive(false);
    }
}
