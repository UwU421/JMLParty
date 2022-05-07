using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JMLControl : MonoBehaviourPunCallbacks
{

    public RawImage spriteRenderer;
    public Texture jamal;
    public Texture gecko;
    public Texture dada;
    public Texture coolJamal;
    public Texture mladyJamal;
    public Texture uwu;
    public Texture catboyJamal;
    public Texture holyJamal;

    private string[] skin = {"",""};

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        skin[0] = PlayerPrefs.GetString("skin");
        skin[1] = view.ViewID.ToString();
        spriteRenderer = gameObject.GetComponent<RawImage>();
        if (view.IsMine)
        {
        view.RPC("ChangeSprite", RpcTarget.All,skin);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    { 
        view.RPC("ChangeSprite", RpcTarget.All,skin);
    }

    [PunRPC]
    IEnumerator ChangeSprite(string[] skinData)
    {
        yield return new WaitForSeconds(0.2f);
        if (view.IsMine)
        {     
        if (skinData[0] == "jamal")
        {
            spriteRenderer.texture = jamal; 
        }
        else if (skinData[0] == "gecko")
        {
            spriteRenderer.texture = gecko; 
        }
        else if (skinData[0] == "dada")
        {
            spriteRenderer.texture = dada; 
        }
        else if (skinData[0] == "coolJamal")
        {
            spriteRenderer.texture = coolJamal; 
        }
        else if (skinData[0] == "mladyJamal")
        {
            spriteRenderer.texture = mladyJamal; 
        }
        else if (skinData[0] == "uwu")
        {
            spriteRenderer.texture = uwu; 
        }
        else if (skinData[0] == "catboyJamal")
        {
            spriteRenderer.texture = catboyJamal; 
        }
        else if (skinData[0] == "holyJamal")
        {
            spriteRenderer.texture = holyJamal; 
        }
        else
        {
            spriteRenderer.texture = jamal; 
        }
        } else if (skinData[1] == view.ViewID.ToString())
        {
            {
                if (skinData[0] == "jamal")
                {
                    spriteRenderer.texture = jamal; 
                }
                else if (skinData[0] == "gecko")
                {
                    spriteRenderer.texture = gecko; 
                }
                else if (skinData[0] == "dada")
                {
                    spriteRenderer.texture = dada; 
                }
                else if (skinData[0] == "coolJamal")
                {
                    spriteRenderer.texture = coolJamal; 
                }
                else if (skinData[0] == "mladyJamal")
                {
                    spriteRenderer.texture = mladyJamal; 
                }
                else if (skinData[0] == "uwu")
                {
                    spriteRenderer.texture = uwu; 
                }
                else if (skinData[0] == "catboyJamal")
                {
                    spriteRenderer.texture = catboyJamal; 
                }
                else if (skinData[0] == "holyJamal")
                {
                    spriteRenderer.texture = holyJamal; 
                }
                else
                {
                    spriteRenderer.texture = jamal; 
                }
            }
        }
    }
}
