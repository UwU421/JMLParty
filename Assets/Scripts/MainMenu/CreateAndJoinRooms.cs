using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField roomCodeInput;
    public InputField nickInput;

    private string roomCode;

    private void Start() {  
        nickInput.text = PlayerPrefs.GetString("prevNick");
    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = nickInput.text;
        PlayerPrefs.SetString("prevNick", nickInput.text);
        PlayerPrefs.Save();
        roomCode = roomCodeInput.text;
        PhotonNetwork.CreateRoom(roomCodeInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName = nickInput.text;
        PlayerPrefs.SetString("prevNick", nickInput.text);
        PlayerPrefs.Save();
        roomCode = roomCodeInput.text;
        PhotonNetwork.JoinRoom(roomCodeInput.text);
    }

    public override void OnJoinedRoom()
    {
        if (roomCode == "jamal")
        {
            PhotonNetwork.LoadLevel("room_JAMAL");
        }
        else if (roomCode == "party")
        {
            PhotonNetwork.LoadLevel("room_DISCO");
        }
        else if (roomCode == "agar")
        {
            PhotonNetwork.LoadLevel("room_AGAR");
        }
        else
        {
            PhotonNetwork.LoadLevel("Test");
        }
    }

}
