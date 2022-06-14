using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class OBJSpawn : MonoBehaviourPunCallbacks
{

    PhotonView view;

    public float[] spawnTimeRange = {0.2f,2f};
    public int[] spawnRange = {480,258};
    public GameObject[] objects;
    public GameObject objParent;

    private bool gameOn = false;
    //private Vector3[] spawnPos = {new Vector3(-480+443,258+263,0),new Vector3(-480+443,263,0),new Vector3(-480+443,-258+263,0),new Vector3(443,258+263,0),new Vector3(443,-258+263,0),new Vector3(480+443,258+263,0),new Vector3(480+443,263,0),new Vector3(480+443,-258+263,0)};
    private Vector3[] spawnPos = {new Vector3(-33,16,0),new Vector3(-33,0,0),new Vector3(-33,-16,0),new Vector3(0,16,0),new Vector3(0,-16,0),new Vector3(33,16,0),new Vector3(33,0,0),new Vector3(33,-16,0)};

    
    void Start()
    {
        //spawnPos = {new Vector3(-spawnRange[0]+443,spawnRange[1]+263,0),new Vector3(-spawnRange[0]+443,0,0),new Vector3(-spawnRange[0]+443,-spawnRange[1]+263,0),new Vector3(443,spawnRange[1]+263,0),new Vector3(443,-spawnRange[1]+263,0),new Vector3(spawnRange[0]+443,spawnRange[1]+263,0),new Vector3(spawnRange[0]+443,0,0),new Vector3(spawnRange[0]+443,-spawnRange[1]+263,0)};
        view = GetComponent<PhotonView>();
        Debug.Log(view.IsMine);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("ADMIN PLAYER INSTANCE");
            gameOn = true;
            StartCoroutine(MainLoop());
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


    IEnumerator MainLoop()
    {
        while (gameOn)
        {
        yield return new WaitForSeconds(Random.Range(spawnTimeRange[0], spawnTimeRange[1]));
        PhotonNetwork.Instantiate(objects[Random.Range(0, objects.Length)].name, spawnPos[Random.Range(0,spawnPos.Length)], Quaternion.identity);
        //GameObject newObject = PhotonNetwork.Instantiate(objects[Random.Range(0, objects.Length - 1)].name, spawnPos[Random.Range(0,spawnPos.Length - 1)], Quaternion.identity);
        //newObject.transform.SetParent(objParent.transform);
        }
    }
}
