using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField roomCodeInput;
    public InputField nickInput;

    public void CreateRoom()
    {
        PhotonNetwork.NickName = nickInput.text;
        PhotonNetwork.CreateRoom(roomCodeInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName = nickInput.text;
        PhotonNetwork.JoinRoom(roomCodeInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Test");
    }

}
