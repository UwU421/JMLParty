using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Minigame_SetUsername3D : MonoBehaviourPunCallbacks
{
    public TextMesh UsernameText;
    PhotonView view;

    private bool DisableSend = false;
    private string text;

    void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine){
            //UsernameText.text = view.Owner.NickName;
            view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
        }
    } 

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
    }

[PunRPC]
IEnumerator DisplayUsername(string text)
    {
        yield return new WaitForSeconds(0.2f);
        UsernameText.text = text;
    }
}
