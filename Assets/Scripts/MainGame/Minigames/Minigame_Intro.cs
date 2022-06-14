using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Minigame_Intro : MonoBehaviourPunCallbacks
{

    public Text countDownText;
    public string minigameName;

    private int x;

    PhotonView view;
    
    void Start()
    {
        view = GetComponent<PhotonView>();
        Debug.Log(view.ViewID);
        if (view.IsMine)
        {
            StartCoroutine(Delay(5.0f));
        }
    }

    int GetID(int input)
    {
        int x = 0;
        while (input > 1000)
        {
            x++;
            input -= 1000;
        }
        x--;
        return x;
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        view.RPC("CountDown", RpcTarget.All,"ha");
    }

    [PunRPC]
    IEnumerator CountDown(string name)
    {
        x = 10;
        while (x > 0)
        {
            countDownText.text = "Hra začíná za " + x.ToString();
            yield return new WaitForSeconds(1.0f);
            x--;
        }
        yield return new WaitForSeconds(0.2f);
        PhotonNetwork.LoadLevel(minigameName);
    }
}
