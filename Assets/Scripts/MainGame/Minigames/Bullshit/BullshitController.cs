using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BullshitController : MonoBehaviourPunCallbacks
{
    public int deadPlayers = 0;
    public bool[] deaths = new bool[PhotonNetwork.CurrentRoom.PlayerCount];
    public float[] spawnTimeRange = {2f,8f};

    public int currentDeath;
    public bool isDead = false;

    public int winnerIndex;

    PhotonView view;
    
    void Start()
    {     
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0;i<PhotonNetwork.CurrentRoom.PlayerCount;i++)
            {
                deaths[i] = false;
            }
            StartCoroutine("CountTime");
        }
    }

    IEnumerator CountTime()
    {
        while (deadPlayers <  PhotonNetwork.CurrentRoom.PlayerCount-1)
        {
            Debug.Log("e");
            yield return new WaitForSeconds(Random.Range(spawnTimeRange[0], spawnTimeRange[1]));
            currentDeath = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount-1);
            while(deaths[currentDeath-1] == true)
            {
            currentDeath = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount);
            }
            deaths[currentDeath-1] = true;
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("player");
            foreach(GameObject go in gameObjectArray)
            {
                Debug.Log("b");
                go.GetComponent<DeathChecker>().CheckDeath(currentDeath);
            }       
            deadPlayers++;
        }
        for (int i = 0; i < deaths.Length;i++)
        {
            if (deaths[i] == false) {
                winnerIndex = i;
                break;
            }
        }
    }
}
