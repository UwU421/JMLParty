using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class AgarController : MonoBehaviourPunCallbacks
{

    public GameObject playerCamera;
    public GameObject UsernameTextObj;
    public TextMesh UsernameText;

    public float increase = 0.5f;

    private float temp;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine){
            view.RPC("DisplayUsername", RpcTarget.All,view.Owner.NickName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
        playerCamera.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,playerCamera.transform.position.z);
        if (transform.position.x > 100)
        {
            transform.position = new Vector3(100,this.transform.position.y,this.transform.position.z);
        }
        if (transform.position.x < -100)
        {
            transform.position = new Vector3(-100,this.transform.position.y,this.transform.position.z);
        }
        if (transform.position.y > 100)
        {
            transform.position = new Vector3(this.transform.position.x,100,this.transform.position.z);
        }
        if (transform.position.y < -100)
        {
            transform.position = new Vector3(this.transform.position.x,-100,this.transform.position.z);
        }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "food")
        {
            this.transform.localScale += new Vector3(increase,increase,0);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x,playerCamera.transform.position.y,playerCamera.transform.position.z - increase);
            UsernameTextObj.transform.position = new Vector3(UsernameTextObj.transform.position.x,UsernameTextObj.transform.position.y - 0.5f,UsernameTextObj.transform.position.z);
            UsernameTextObj.transform.localScale += new Vector3(increase,increase,0);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "player")
        {
            if (other.gameObject.transform.localScale.x < this.transform.localScale.x)
            {
                temp = other.gameObject.transform.localScale.x;
                this.transform.localScale += new Vector3(temp,temp,0);
                other.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0);
            }
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
        Debug.Log("epic");
        UsernameText.text = text;
    }
}
