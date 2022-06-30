using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MatheController2 : MonoBehaviourPunCallbacks
{

    public Text matheText;

    public int operation;

    private int num1;
    private int num2;
    public int answer;
    private int tempNum;

    public bool answered = false;
    private string question;
    private string temp;

    private bool inGame = true;

    public int points = 0;

    public GameObject resultObj;
    public Text resultText;
    public GameObject rewardObj;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        resultObj = GameObject.Find("results");
        resultText = GameObject.Find("txt_ResultDesc").GetComponent<Text>();
        rewardObj = GameObject.Find("txt_GotMoney");
        resultObj.gameObject.SetActive(false);
        rewardObj.gameObject.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("lll :)");
            StartCoroutine("MainLoop");
        }
    }

    public IEnumerator MainLoop()
    {
        while (inGame)
        {
            view.RPC("DisplayQuestion", RpcTarget.All,AskQuestion());
            yield return new WaitForSeconds(5.0f);
            if (!answered)
            {
                view.RPC("DisplayText", RpcTarget.All, "Nikdo neodpověděl! Správná odpověď je " + answer);
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

    public string AskQuestion()
    {
        operation = Random.Range(1,4);
        switch(operation) 
        {
        case 1:
            num1 = Random.Range(1,99);
            num2 = Random.Range(1,99);
            answer = num1 + num2;
            question = "Kolik je " + num1.ToString() + " + " + num2.ToString() + "?";
            //view.RPC("DisplayQuestion", RpcTarget.All,question);
            view.RPC("SetAnswer", RpcTarget.All,num1 + num2);
            return question;
        case 2:
            num1 = Random.Range(1,99);
            do {
            num2 = Random.Range(1,99);
            } while (num2 > num1);
            question = "Kolik je " + num1.ToString() + " - " + num2.ToString() + "?";
            //view.RPC("DisplayQuestion", RpcTarget.All,question);
            view.RPC("SetAnswer", RpcTarget.All,num1 - num2);
            return question;
        case 3:
            num1 = Random.Range(1,10);
            num2 = Random.Range(1,10);
            question = "Kolik je " + num1.ToString() + " * " + num2.ToString() + "?";
            //view.RPC("DisplayQuestion", RpcTarget.All,question);
            view.RPC("SetAnswer", RpcTarget.All,num1 * num2);
            return question;
        case 4:
            num2 = Random.Range(1,10);
            answer = Random.Range(1,10);
            tempNum = answer;
            num1 = answer * num2;
            question = "Kolik je " + num1.ToString() + " / " + num2.ToString() + "?";
            //view.RPC("DisplayQuestion", RpcTarget.All,question);
            view.RPC("SetAnswer", RpcTarget.All,tempNum);
            return question;
        }
        return "err";
    }

    public void Answered(string username,string playerAnswer)
    {
        if (playerAnswer == answer.ToString())
        {
            answered = true;
            temp = "Správně odpověděl " + username + "! Správná odpověď: " + answer;
            view.RPC("DisplayText", RpcTarget.All,temp);
        }
    }

    [PunRPC]
    public void DisplayQuestion(string text)
    {
        answered = false;
        matheText.text = text;      
    }

    [PunRPC]
    public void DisplayText(string text)
    {
        answered = true;
        matheText.text = text;
    }

    [PunRPC]
    IEnumerator SetAnswer(int num)
    {
        yield return new WaitForSeconds(0.1f);
        answer = num;
    }

    [PunRPC]
    IEnumerator GameEnd(string playerName)
    {
        inGame = false;
        yield return new WaitForSeconds(2f);
        resultObj.SetActive(true);
        resultText.text = "Winner: " + playerName;
        matheText.gameObject.SetActive(false);
        if(view.Owner.NickName == playerName)
            {
            PlayerPrefs.SetInt("jmlcoins", PlayerPrefs.GetInt("jmlcoins") + 5);
            rewardObj.gameObject.SetActive(true);
        }
        StartCoroutine("JoinLobby");
    }

    IEnumerator JoinLobby()
    {
        yield return new WaitForSeconds(6);
        PhotonNetwork.LoadLevel("TestNew");
    }
}
