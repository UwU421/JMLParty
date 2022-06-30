using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullshitPlayer : MonoBehaviour
{

    public GameObject dieText;

    void Start()
    {
        dieText = GameObject.Find("deathText");
        dieText.gameObject.SetActive(false);
    }

    public void Die()
    {
        dieText.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        dieText.gameObject.SetActive(true);
    }   
}
