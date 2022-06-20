﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MatheChat : MonoBehaviourPunCallbacks
{
    
    public UnityEngine.UI.InputField ChatInputField;
    public Text SpeechBubble;
    public Text UsernameText;
    PhotonView view;

    private bool DisableSend = false;
    private string text;
    private int points = 0;

    public MatheController2 mathe;

    void Start()
    {
        view = GetComponent<PhotonView>();
        ChatInputField = GameObject.Find("input_chat").GetComponent<InputField>();
        mathe = (MatheController2) GameObject.Find("MatheManager").GetComponent(typeof(MatheController2));
        if(view.IsMine){
            //UsernameText.text = view.Owner.NickName;
            Debug.Log("ah " + view.Owner.NickName);
            view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
        }
    }

    private void Update() 
    {
        if (view.IsMine)
        {
            if (!DisableSend && ChatInputField.isFocused)
            {
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKey("return"))
                {
                    DisableSend = true;
                    text = ChatInputField.text;
                    ChatInputField.text = "";
                    checkAnswer(text);
                    view.RPC("OnInput", RpcTarget.All, text);                 
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
    }

    public void checkAnswer(string answr)
    {     
        if (!mathe.answered)
        {
            //mathe.Answered(view.Owner.NickName,answr);
            if (answr == mathe.answer.ToString())
            {
                mathe.answered = true;
                if (view.IsMine)
                {
                    Debug.Log("ohoho");
                    points++;
                    if (points >= 5)
                    {
                        Debug.Log("hi :))))");
                        mathe.view.RPC("GameEnd", RpcTarget.All, view.Owner.NickName);
                    }
                }
                mathe.view.RPC("DisplayText", RpcTarget.All,"Správně odpověděl " + view.Owner.NickName + "! Správná odpověď: " + answr);
            }
        }
    }


[PunRPC]
public void OnInput (string usedString)
{
    checkAnswer(text);
    DisplayWord(usedString);
}

public void DisplayWord (string usedString)
{
    SpeechBubble.text = usedString;
    StartCoroutine("Remove");
}

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        SpeechBubble.text = "";
        DisableSend = false;
    }

[PunRPC]
IEnumerator DisplayUsername(string text)
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("epic");
        UsernameText.text = text;
    }
}
