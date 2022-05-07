using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    
    public GameObject playerObj;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float Z = 0;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),Z);
        PhotonNetwork.Instantiate(playerObj.name, randomPosition, Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //print(newPlayer.nickName);
    }

}
