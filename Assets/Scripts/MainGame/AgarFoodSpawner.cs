using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AgarFoodSpawner : MonoBehaviour
{
    
    public GameObject foodObj;
    public float minX = -95;
    public float minY = -95;
    public float maxX = 95;
    public float maxY = 95;

    PhotonView view;

    // Update is called once per frame
    void Start()
    {
        view = GetComponent<PhotonView>();
        for (int i = 0; i<100; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-100,100),Random.Range(-100,100),0);
            PhotonNetwork.Instantiate(foodObj.name, randomPosition, Quaternion.identity);
        }
        view.RPC("SpawnFood", RpcTarget.All,"a");
    }

    [PunRPC]
    IEnumerator SpawnFood(string text)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),0);
        PhotonNetwork.Instantiate(foodObj.name, randomPosition, Quaternion.identity);
        StartCoroutine("SpawnFood","hi");
    }
}
