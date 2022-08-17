using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public static IEnumerator FadeOutObject(GameObject fadeOutObject, float fadeSpeed)
    {
        Debug.Log("InFadeOutObject");
        while (fadeOutObject.GetComponent<Renderer>().material.color.a > 0)
        {
            Debug.Log("Color:" + fadeOutObject.GetComponent<Renderer>().material.color.a);
            Color objectColor = fadeOutObject.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeOutObject.GetComponent<Renderer>().material.color = objectColor;

            yield return null;
        }
    }

    public static IEnumerator FadeOutObjectTwo(GameObject fadeOutObject, float fadeSpeed)
    {
        Debug.Log("InFadeOutObjectTwo"); 
        float change = 0.0f;
        Color fadeColor = new Color(0, 0, 0, 0);
        Color objectColor = fadeOutObject.GetComponent<Renderer>().material.color;

        // Loop until lerp value is 1 (fully changed)
        while (change < 1.0f)
        {
            //Debug.Log("Color:" + fadeOutObject.GetComponent<Renderer>().material.color.a);
            // Reduce change value by fadeSpeed amount.
            change += fadeSpeed * Time.deltaTime;

            fadeOutObject.GetComponent<Renderer>().material.color = Color.Lerp(objectColor, fadeColor, change);

            yield return null;
        }


    }

    public static IEnumerator FadeInObject(GameObject fadeInObject, float fadeSpeed)
    {
        Debug.Log("InFadeInObject");
        while (fadeInObject.GetComponent<Renderer>().material.color.a < 1)
        {
            // Debug.Log("Color:" + fadeInObject.GetComponent<Renderer>().material.color.a);
            Color objectColor = fadeInObject.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeInObject.GetComponent<Renderer>().material.color = objectColor;

            yield return null;
        }
    }
}
