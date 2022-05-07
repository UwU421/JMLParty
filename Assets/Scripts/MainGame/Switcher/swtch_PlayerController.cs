using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class swtch_PlayerController : MonoBehaviour
{
    public float moveForce;
    public float jumpForce;
    private int canJump = 0;
    public string nextLevel;
    public int trampolineForce = 650;
    public string thisLevel;

    private bool hasSwitchedLayers = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            this.transform.position += new Vector3(
                -moveForce * Time.deltaTime,
                0,
                0
            );
            this.transform.rotation = Quaternion.Euler(-90,0,-90);
        }
        if (Input.GetKey("d"))
        {
            this.transform.position += new Vector3(
                moveForce * Time.deltaTime,
                0,
                0
            );
            this.transform.rotation = Quaternion.Euler(-90,0,90);
        }
        if (Input.GetKeyDown("w") && canJump<1)
        {
            canJump++;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpForce,0));
        }

        if (Input.GetKeyDown("space"))
        {
            if (hasSwitchedLayers)
            {
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    0
                );
            }
            else
            {
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    10
                );
            }
            hasSwitchedLayers = !hasSwitchedLayers;
        }
        if (transform.position.y < -10)
        {
            hasSwitchedLayers = false;
            SceneManager.LoadScene(thisLevel);
        }
    }
    
    void OnCollisionEnter(Collision collision) 
    {
        canJump = 0; 
        if (collision.gameObject.name == "End")
        {
            SceneManager.LoadScene(nextLevel);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            hasSwitchedLayers = false;
            SceneManager.LoadScene(thisLevel);
        }
        if (collision.gameObject.tag == "Trampoline")
        {
            GetComponent<Rigidbody>().AddForce(-(collision.gameObject.transform.rotation.z * 5), trampolineForce, 0);
        }
    }

    public void RightBtn() {
        this.GetComponent<Rigidbody>().velocity = new Vector3(
                moveForce * Time.deltaTime,
                this.GetComponent<Rigidbody>().velocity.y,
                this.GetComponent<Rigidbody>().velocity.z
            );
    }

    public void LeftBtn() {
        this.GetComponent<Rigidbody>().velocity = new Vector3(
                -moveForce * Time.deltaTime,
                this.GetComponent<Rigidbody>().velocity.y,
                this.GetComponent<Rigidbody>().velocity.z
            );
    }
    
    public void JumpBtn() {
        if (canJump<1)
        {
            canJump++;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpForce,0));
        }
    }

    public void SwitchBtn() {
        if (hasSwitchedLayers)
            {
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    0
                );
            }
            else
            {
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    10
                );
            }
            hasSwitchedLayers = !hasSwitchedLayers;
    }
}
