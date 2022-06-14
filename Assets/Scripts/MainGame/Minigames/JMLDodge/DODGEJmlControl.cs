using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class DODGEJmlControl : MonoBehaviourPunCallbacks
{

    public SpriteRenderer spriteRenderer;
    public Sprite jamal;
    public Sprite gecko;
    public Sprite dada;
    public Sprite coolJamal;
    public Sprite mladyJamal;
    public Sprite uwu;
    public Sprite catboyJamal;
    public Sprite holyJamal;

    private string[] skin = {"",""};

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        skin[0] = PlayerPrefs.GetString("skin");
        skin[1] = view.ViewID.ToString();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            spriteRenderer.sprite = jamal; 
        }
        else if (skinData[0] == "gecko")
        {
            spriteRenderer.sprite = gecko; 
        }
        else if (skinData[0] == "dada")
        {
            spriteRenderer.sprite = dada; 
        }
        else if (skinData[0] == "coolJamal")
        {
            spriteRenderer.sprite = coolJamal; 
        }
        else if (skinData[0] == "mladyJamal")
        {
            spriteRenderer.sprite = mladyJamal; 
        }
        else if (skinData[0] == "uwu")
        {
            spriteRenderer.sprite = uwu; 
        }
        else if (skinData[0] == "catboyJamal")
        {
            spriteRenderer.sprite = catboyJamal; 
        }
        else if (skinData[0] == "holyJamal")
        {
            spriteRenderer.sprite = holyJamal; 
        }
        else
        {
            spriteRenderer.sprite = jamal; 
        }
        } else if (skinData[1] == view.ViewID.ToString())
        {
            {
                if (skinData[0] == "jamal")
                {
                    spriteRenderer.sprite = jamal; 
                }
                else if (skinData[0] == "gecko")
                {
                    spriteRenderer.sprite = gecko; 
                }
                else if (skinData[0] == "dada")
                {
                    spriteRenderer.sprite = dada; 
                }
                else if (skinData[0] == "coolJamal")
                {
                    spriteRenderer.sprite = coolJamal; 
                }
                else if (skinData[0] == "mladyJamal")
                {
                    spriteRenderer.sprite = mladyJamal; 
                }
                else if (skinData[0] == "uwu")
                {
                    spriteRenderer.sprite = uwu; 
                }
                else if (skinData[0] == "catboyJamal")
                {
                    spriteRenderer.sprite = catboyJamal; 
                }
                else if (skinData[0] == "holyJamal")
                {
                    spriteRenderer.sprite = holyJamal; 
                }
                else
                {
                    spriteRenderer.sprite = jamal; 
                }
            }
        }
    }
}
