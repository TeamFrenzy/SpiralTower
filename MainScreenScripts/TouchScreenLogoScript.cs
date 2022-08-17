using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenLogoScript : MonoBehaviour
{
    Color alpha;
    public float speed;
    public float alphaView;
    bool up = false;
    void Awake()
    {
        alpha = GetComponent<SpriteRenderer>().color;
        alpha.a = 0;
        GetComponent<SpriteRenderer>().color = alpha;
    }

    private void Update()
    {
        if (alpha.a >=1f)
        {
            up = false;
        }
        else if(alpha.a<=0f)
        {
            up = true;
        }

        if(up)
        {
            alpha.a = alpha.a + Time.deltaTime * speed;
        }
        else
        {
            alpha.a = alpha.a - Time.deltaTime * speed;
        }

        alphaView = alpha.a;
        GetComponent<SpriteRenderer>().color = alpha;
    }
}
