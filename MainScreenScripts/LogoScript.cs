using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    Color alpha;
    public float speed;
    public float alphaView;
    void Awake()
    {
        alpha = GetComponent<SpriteRenderer>().color;
        alpha.a = 0;
        GetComponent<SpriteRenderer>().color = alpha;
    }

    private void Update()
    {
        if (alpha.a < 1f)
        {
            alpha.a = alpha.a + Time.deltaTime * speed;
        }

        alphaView = alpha.a;
        GetComponent<SpriteRenderer>().color = alpha;
    }
}
