using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullshitPlayer : MonoBehaviour
{
    public void Die()
    {
        this.gameObject.SetActive(false);
    }   
}
