using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MatheController : MonoBehaviourPunCallbacks
{

    public Text matheText;

    public int operation;

    private int num1;
    private int num2;
    private int answer;

    public bool answered = false;
    private string question;
    private string temp;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        AskQuestion();
    }

    public void AskQuestion()
    {
        operation = Random.Range(1,4);
        switch(operation) 
        {
        case 1:
            num1 = Random.Range(1,99);
            num2 = Random.Range(1,99);
            answer = num1 + num2;
            question = "Kolik je " + num1.ToString() + " + " + num2.ToString() + "?";
            view.RPC("DisplayQuestion", RpcTarget.All,question);
            break;
        case 2:
            num1 = Random.Range(1,99);
            do {
            num2 = Random.Range(1,99);
            } while (num2 > num1);
            answer = num1 - num2;
            question = "Kolik je " + num1.ToString() + " - " + num2.ToString() + "?";
            view.RPC("DisplayQuestion", RpcTarget.All,question);
            break;
        case 3:
            num1 = Random.Range(1,10);
            num2 = Random.Range(1,10);
            answer = num1 * num2;
            question = "Kolik je " + num1.ToString() + " * " + num2.ToString() + "?";
            view.RPC("DisplayQuestion", RpcTarget.All,question);
            break;
        case 4:
            num2 = Random.Range(1,10);
            answer = Random.Range(1,10);
            num1 = answer * num2;
            question = "Kolik je " + num1.ToString() + " / " + num2.ToString() + "?";
            view.RPC("DisplayQuestion", RpcTarget.All,question);
            break;
        }
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
    public IEnumerator DisplayQuestion(string text)
    {
        answered = false;
        matheText.text = text;
        Debug.Log("aaa");
        yield return new WaitForSeconds(6f);
        if (!answered)
        {
            answered = true;
            temp = ("Nikdo neodpověděl! Správná odpověď je " + answer);
            matheText.text = text; 
            yield return new WaitForSeconds(2.5f);
            AskQuestion();
        }
    }

    [PunRPC]
    IEnumerator DisplayText(string text)
    {
        matheText.text = text;
        yield return new WaitForSeconds(2.5f);
        AskQuestion();
    }
}
