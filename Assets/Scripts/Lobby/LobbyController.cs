﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{

    public GameObject StartGameBtn;
    public Text StartText;
    private int x;

    PhotonView view;
    
    private void Start() 
    {
        view = GetComponent<PhotonView>();
        StartGameBtn.SetActive(false);
        StartText.gameObject.SetActive(false);
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            view.RPC("ShowBTN", RpcTarget.All,"ha");
        }
    }

  //  public override void OnPlayerEnteredRoom(Player newPlayer)
 //   {
  //      if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
  //      {
  //      StartGameBtn.SetActive(true);
  //      }
  //  }

    public void StartBtnClick()
    {
        if (view.IsMine)
        {
        view.RPC("JoinMainGame", RpcTarget.All,"ha");
        }
    }

    [PunRPC]
    IEnumerator JoinMainGame(string name)
    {
        x = 5;
        StartText.gameObject.SetActive(true);
        while (x > 0)
        {
            StartText.text = "Hra začíná za: " + x.ToString();
            yield return new WaitForSeconds(1f);
            x--;
        }
        yield return new WaitForSeconds(0.2f);
        Debug.Log("hi");
        PhotonNetwork.LoadLevel("room_DISCO");
    }

    [PunRPC]
    public void ShowBTN(string name)
    {
        StartGameBtn.SetActive(true);
    }
}
