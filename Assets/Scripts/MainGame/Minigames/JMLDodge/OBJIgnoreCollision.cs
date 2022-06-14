using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJIgnoreCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.tag == "Dodge_Obstacle")
      {
          Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
      }
  }
}
