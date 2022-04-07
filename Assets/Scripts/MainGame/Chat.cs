using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Chat : MonoBehaviour
{
    
    public InputField ChatInputField;
    public Text SpeechBubble;
    private bool DisableSend = false;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update() 
    {
        if (view.IsMine)
        {
            if (!DisableSend && ChatInputField.isFocused)
            {
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
