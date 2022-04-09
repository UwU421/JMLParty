using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviourPunCallbacks
{
    public void LeaveGame()
    {
            Object.Destroy(this.gameObject);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");     
    }
}
