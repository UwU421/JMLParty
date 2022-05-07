using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove3D : MonoBehaviour
{

    public float speedX = 2;
    public float speedY = 2;

    public GameObject voiceManager;
    public Camera camera;

    CharacterController controller;  

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        camera.enabled = view.IsMine;
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            //Vector3 move = new Vector3(Input.GetAxis("Horizontal") * speedX, 0, Input.GetAxis("Vertical") * speedY);
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speedX, 0, Input.GetAxis("Vertical") * speedY) * Time.deltaTime);
            
        }
    }

    void OnApplicationQuit()
    {
        if(view.IsMine)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
