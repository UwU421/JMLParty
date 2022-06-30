using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DeathChecker : MonoBehaviour
{

    public PhotonView view;

    void Start()
    {     
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void CheckDeath(int playerIndex)
    {
        Debug.Log(view.ViewID);
        Debug.Log(playerIndex);
        if (((view.ViewID - 1) / 1000) == playerIndex)
        {
            this.gameObject.SetActive(false);
        }
    }

}
