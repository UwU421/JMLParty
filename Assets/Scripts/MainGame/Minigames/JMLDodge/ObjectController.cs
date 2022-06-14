using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public int[] bounceRange = {3,5};
    public int[] speedRange = {15,20};
    public int[] spawnRange = {33,16};
    public float[] rotateRange = {2,10};
    public int[] sizeRange = {1,3};

    public GameObject sprite;

    private int bounceAmount;
    private int speedX;
    private int speedY;
    private int[] spawnMax = {33,16};
    private float rotateAmt;
    private int speed;
    private int size;

    void Start()
    {
        speed = Random.Range(speedRange[0], speedRange[1]);
        spawnMax[0] = 480;
        spawnMax[1] = 258;
        bounceAmount = Random.Range(bounceRange[0], bounceRange[1]);
        speedX = speed;
        speedY = speed;
        rotateAmt = Random.Range(rotateRange[0], rotateRange[1]);
        speed = Random.Range(speedRange[0], speedRange[1]);
        size = Random.Range(sizeRange[0],sizeRange[1]);
        transform.localScale = new Vector3(size,size,1);
    }


    void Update()
    {
        if (bounceAmount > 0)
        {
            //if (transform.position.x > (spawnMax[0]+443) || transform.position.x < (-spawnMax[0]+443))
           // {
            //    speedX = -speedX;
           //     Debug.Log(speedX);
           //     bounceAmount--;
           // }
           // if (transform.position.y > (spawnMax[1]+263) || transform.position.y < (-spawnMax[1]+263))
           // {
           //     speedY = -speedY;
           //     Debug.Log(speedY);
           ///    bounceAmount--;
           // }
           if (transform.position.x > (spawnRange[0]+1) || transform.position.x < (-spawnRange[0]-1))
            {
                speedX = -speedX;
                bounceAmount--;
            }
            if (transform.position.y > (spawnRange[1]+1) || transform.position.y < (-spawnRange[1]-1))
            {
                speedY = -speedY;
               bounceAmount--;
            }
            transform.Translate(new Vector3(speedX,speedY,0) * Time.deltaTime);
            sprite.gameObject.transform.Rotate(0.0f, 0.0f, rotateAmt, Space.Self);
        } else {
            Destroy(gameObject);
        }
    }

}
