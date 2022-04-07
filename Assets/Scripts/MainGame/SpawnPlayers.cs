using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    
    public GameObject playerObj;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),0);
        PhotonNetwork.Instantiate(playerObj.name, randomPosition, Quaternion.identity);
    }

}
