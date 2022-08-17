using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponScript : MonoBehaviour
{
    public GameObject tamakiObject;
    public GameObject nanamiObject;
    public float timer;
    public bool hit;

    void Start()
    {
        hit = false;
        timer = 5f;
    }

    void Update()
    {
        if(hit)
        {
            timer = timer - Time.deltaTime;
            if(timer < 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tamaki")
        {
            Debug.Log("Colliding!");
            if (tamakiObject.GetComponent<BattleParryScript>().isBParrying)
            {
                Debug.Log("Tamaki is parrying!");
                tamakiObject.GetComponent<BattleParryScript>().isDown = false;
            }
            else if(!tamakiObject.GetComponent<BattleParryScript>().isDown)
            {
                nanamiObject.GetComponentInChildren<NanamiManager>().recoiling = true;
                tamakiObject.GetComponent<BattleParryScript>().hit = true;
                tamakiObject.GetComponent<BattleParryScript>().isDown = true;
                Debug.Log("Tamaki took a hit!");
                tamakiObject.GetComponent<Animator>().Play("Downed");
            }
            else if(tamakiObject.GetComponent<BattleParryScript>().isDown)
            {
                GameObject.Find("Canvas").transform.Find("LosePicBlackBG").gameObject.SetActive(true);
                hit = true;
                //Time.timeScale = 0;
            }
        }
    }
}
