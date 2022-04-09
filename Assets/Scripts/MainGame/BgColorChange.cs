using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BgColorChange : MonoBehaviour
{
    Color lerpedColor = Color.white;

    void Start()
    {
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.green, Mathf.PingPong(Time.time, 1));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1");
        GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.blue, Mathf.PingPong(Time.time, 1));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("2");
        GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("3");
        GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("4");
        StartCoroutine("ChangeColor");
    }
}