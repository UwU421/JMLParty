using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DODGEGameController : MonoBehaviourPunCallbacks
{

    private int bestTime;
    private string bestPlayerName;
    private int deadPlayers = 0;

    private int[] temp;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void PlayerDied(int maxTime)
    {
        temp[0] = view.ViewID;
        temp[1] = maxTime;
        view.RPC("CheckGameEnd", RpcTarget.All,temp);
    }

    [PunRPC]
    public void CheckGameEnd(int[] player)
    {
        deadPlayers++;
        if (bestTime < player[1])
        {
            bestTime = player[1];
            bestPlayerName = PhotonNetwork.PlayerList[player[0]].NickName;
        }
        if (deadPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Dodge_Obstacle");
            foreach(GameObject go in gameObjectArray)
        {
            go.SetActive (false);
        }
        }
    }
}
