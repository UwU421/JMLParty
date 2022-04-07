using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Chat : MonoBehaviour
{
    
    public UnityEngine.UI.InputField ChatInputField;
    public Text SpeechBubble;
    private bool DisableSend = false;
    PhotonView view;

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
                    SpeechBubble.text = ChatInputField.text;
                    ChatInputField.text = "";
                    Debug.Log("hi");
                    StartCoroutine("Remove");
                }
            }
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        SpeechBubble.text = "";
        DisableSend = false;
    }
}
