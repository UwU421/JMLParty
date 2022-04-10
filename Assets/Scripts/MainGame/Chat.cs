using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Chat : MonoBehaviourPunCallbacks
{
    
    public UnityEngine.UI.InputField ChatInputField;
    public Text SpeechBubble;
    public Text UsernameText;
    PhotonView view;

    private bool DisableSend = false;
    private string text;

    void Start()
    {
        view = GetComponent<PhotonView>();
        ChatInputField = GameObject.Find("input_chat").GetComponent<InputField>();
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
                    view.RPC("OnInput", RpcTarget.All, text);
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("CUM");
        view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
    }


[PunRPC]
public void OnInput (string usedString)
{
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
