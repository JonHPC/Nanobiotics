using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
    public HomingShot homingShot;
    public bool lockedOn = false;

    void Start()
    {
        homingShot = transform.parent.GetComponent<HomingShot>();
    }

    void Update(){

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy" && lockedOn == true)
        {

            //Debug.Log("Lock on");
            //lockedOn = true;
            //Upon colliding with a game object tagged "Enemy", assign its transform to its parent for tracking

            //homingShot.target = other.transform;
        }
    }

    /*void OnTriggerExit2D(Collider2D other){

        if(other.gameObject.tag == "Enemy"){
            lockedOn = false;
        }

    }*/
}
