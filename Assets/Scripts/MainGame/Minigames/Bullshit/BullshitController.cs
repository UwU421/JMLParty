using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class BullshitController : MonoBehaviourPunCallbacks
{
    public int deadPlayers = 0;
    public bool[] deaths;
    public float[] spawnTimeRange = {2f,8f};

    public int currentDeath;
    public bool isDead = false;

    public GameObject resultObj;
    public Text resultText;
    public GameObject diedText;
    public GameObject titleText;

    public int winnerIndex;

    PhotonView view;
    
    void Start()
    {     
        view = GetComponent<PhotonView>();
        resultObj = GameObject.Find("results");
        resultText = GameObject.Find("txt_winner").GetComponent<Text>();
        diedText = GameObject.Find("deathText");
        titleText = GameObject.Find("title");
        resultObj.gameObject.SetActive(false);
        diedText.gameObject.SetActive(false);
        deaths = new bool[PhotonNetwork.CurrentRoom.PlayerCount];
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
            currentDeath = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount);
            while(deaths[currentDeath-1] == true)
            {
            currentDeath = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount);
            }
            deaths[currentDeath-1] = true;
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("player");
            foreach(GameObject go in gameObjectArray)
            {
                go.GetComponent<DeathChecker>().view.RPC("CheckDeath", RpcTarget.All,currentDeath);
            }   
            Debug.Log(deadPlayers);    
            deadPlayers++;
        }
        for (int i = 0; i < deaths.Length;i++)
        {
            if (deaths[i] == false) {
                winnerIndex = i;
                break;
            }
        }
        view.RPC("GameEnd", RpcTarget.All,winnerIndex);
    }

    [PunRPC]
    public void GameEnd(int winI)
    {
        diedText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        resultObj.SetActive(true);
        resultText.text = "Winner: " + PhotonNetwork.PlayerList[winI].NickName;
        Debug.Log("hi");
        StartCoroutine("JoinLobby");
    }

    IEnumerator JoinLobby()
    {
        yield return new WaitForSeconds(6);
        PhotonNetwork.LoadLevel("TestNew");
    }
}
