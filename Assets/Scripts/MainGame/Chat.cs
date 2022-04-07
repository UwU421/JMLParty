using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Chat : MonoBehaviour
{
    
    public UnityEngine.UI.InputField ChatInputField;
    public Text SpeechBubble;
    PhotonView view;

    private bool DisableSend = false;
    private string text;

    void Start()
    {
        view = GetComponent<PhotonView>();
        ChatInputField = GameObject.Find("input_chat").GetComponent<InputField>();
    }

    private void Update() 
    {
        if (view.IsMine)
        {
            Debug.Log("1");
            if (!DisableSend && ChatInputField.isFocused)
            {
                Debug.Log("2");
                if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown("space"))
                {
                    DisableSend = true;
                    text = ChatInputField.text;
                    ChatInputField.text = "";
                    view.RPC("OnInput", RpcTarget.All, text);
                    Debug.Log("8");
                }
            }
        }
    }

[PunRPC]
public void OnInput (string usedString)
{
    DisplayWord(usedString);
    Debug.Log("9");
}

public void DisplayWord (string usedString)
{
    SpeechBubble.text = usedString;
    StartCoroutine("Remove");
    Debug.Log("10");
}

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        SpeechBubble.text = "";
        DisableSend = false;
    }
}
