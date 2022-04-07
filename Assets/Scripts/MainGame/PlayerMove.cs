using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{

    public float speedX = 2;
    public float speedY = 2;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(speedX * inputX, speedY * inputY, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
