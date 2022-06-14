using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class DodgeCollision : MonoBehaviourPunCallbacks
{

    public int maxHealth = 3;

    public Text timeText;
    public GameObject diedText;

    private int health;
    private int time = 0;

    //GAME CONTROLLER
    public GameObject resultObj;
    public Text resultText;
    public GameObject objSpawner;
    public GameObject rewardObj;

    private int bestTime;
    private string bestPlayerName;
    private int deadPlayers = 0;

    private int[] temp = {0,0};

    PhotonView view;


    void Start()
    {
        view = GetComponent<PhotonView>();
        timeText = GameObject.Find("txt_Time").GetComponent<Text>();
        diedText = GameObject.Find("deathText");
        resultObj = GameObject.Find("results");
        resultText = GameObject.Find("txt_ResultDesc").GetComponent<Text>();
        objSpawner = GameObject.Find("OBJSpawner");
        rewardObj = GameObject.Find("txt_GotMoney");
        if (GetID(view.ViewID) == 1)
        {
        diedText.gameObject.SetActive(false);
        resultObj.gameObject.SetActive(false);
        rewardObj.gameObject.SetActive(false);
        }
        health = maxHealth;
        StartCoroutine(CountTime());      
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dodge_Obstacle")
        {
            Destroy(collision.gameObject);
            health--;
            if (health < 1)
            {
                view.RPC("HidePlayer", RpcTarget.All,view.ViewID.ToString());
                this.gameObject.SetActive(false);
                PlayerDied(time);
            }
        }
    }

    IEnumerator CountTime()
    {
        while (health > 0)
        {
            yield return new WaitForSeconds(1f);
            time++;
            timeText.text = "Time: " + time.ToString();
        }
    }

    //GAME CONTROLLER
    void PlayerDied(int maxTime)
    {
        temp[0] = view.ViewID;
        temp[1] = maxTime;
        view.RPC("CheckGameEnd", RpcTarget.All,temp);
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

    [PunRPC]
    public void HidePlayer(string x)
    {
        if (health < 1)
        {
            if (view.IsMine)
            {
                diedText.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            } else if (x == view.ViewID.ToString())
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    [PunRPC]
    public void CheckGameEnd(int[] player)
    {
        Debug.Log("check");
        Debug.Log(view.ViewID);
        deadPlayers++;
        if (bestTime < player[1])
        {
            bestTime = player[1];
            bestPlayerName = PhotonNetwork.PlayerList[GetID(player[0])].NickName;
        }
        if (deadPlayers == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.IsMasterClient)
        {
            view.RPC("GameEnd", RpcTarget.All,view.ViewID.ToString());
        }
    }

    [PunRPC]
    public void GameEnd(string player)
    {
        diedText.gameObject.SetActive(false);
            objSpawner.SetActive(false);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Dodge_Obstacle");
            foreach(GameObject go in gameObjectArray)
            {
            go.SetActive(false);
            }
            resultObj.SetActive(true);
            resultText.text = "Winner: " + bestPlayerName + " - " + bestTime.ToString() + "s";
            Debug.Log("hi");
            if (view.Owner.NickName == bestPlayerName)
            {
                PlayerPrefs.SetInt("jmlcoins", PlayerPrefs.GetInt("jmlcoins") + 5);
                rewardObj.gameObject.SetActive(true);
        }
    }
}
