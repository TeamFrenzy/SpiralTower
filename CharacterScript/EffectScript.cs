using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    [SerializeField]
    internal CharManagerScript managerScript;

    [SerializeField]
    internal GameObject[] blueCircle;

    public void BlueCircle(Vector3 contactPoint, bool trueFromInfalseFromOut)
    {
        if(!trueFromInfalseFromOut)
        {
            Debug.Log("InSpawnCircle");
            GameObject parryPoint = GameObject.Instantiate(blueCircle[0]);
            parryPoint.transform.position = new Vector3(contactPoint.x, contactPoint.y, 0);
            parryPoint.GetComponent<Animator>().Play("blueCircleAnim2");
            Destroy(parryPoint, 0.9f);
        }
        else if(trueFromInfalseFromOut)
        {
            Debug.Log("InSpawnCircle");
            GameObject parryPoint = GameObject.Instantiate(blueCircle[0]);
            parryPoint.transform.position = new Vector3(contactPoint.x, contactPoint.y, 0);
            parryPoint.GetComponent<Animator>().Play("blueCircleAnim");
            Destroy(parryPoint, 0.9f);
        }
    }
}
