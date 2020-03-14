using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public int health = 2;
    public float moveSpeed = 5.0f;
    public int score = 100;

    //public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        //gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(-1, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if(health <= 0){

            //gameController.GetComponent<GameController>().score += score; //accesses the GameController script and adds the 'score' value of the enemy
            GameController.instance.score += score; //accesses the GameController instance and adds the 'score' value of the enemy
            GameController.instance.spawnUpgrade(transform);//runs this function for a chance to drop items
            Destroy(gameObject);//if the health of this enemy drops to or below 0, destroy this gameObject
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "BasicShot")
        {
            health -= other.gameObject.GetComponent<BasicShot>().damage; //subtracts health based on the damage of the shot received
        }
        else if(other.gameObject.tag == "SpreadShot")
        {
            health -= other.gameObject.GetComponent<SpreadShot>().damage; //subtracts health based on the damage of the spread shot received
        }
        else if(other.gameObject.tag == "LaserShot")
        {
            health -= other.gameObject.GetComponent<LaserShot>().damage; //subtracts health based on the damage of the laser shot received
        }
        /*else if(other.gameObject.tag == "HomingShot"){
            health -= other.gameObject.GetComponent<HomingShot>().damage; //subtracts health based on the damage of the homing shot received
        }*/
        else if(other.gameObject.tag == "BackShot"){
            health -= other.gameObject.GetComponent<BackShot>().damage; //subtracts health based on the damage of the back shot received
        }
        else if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().isInvincible == false)
        {
            //gameController.GetComponent<GameController>().lives -= 1;//subtracts one life upon colliding with the player
            GameController.instance.lives -= 1;//subtracts one life upon colliding with the player
            GameController.instance.isDead = true; //changes the isDead bool to true when the player dies
            Destroy(other.gameObject);//destroys the player upon collision

        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "HomingShot"){
            health -= other.gameObject.GetComponent<HomingShot>().damage; //subtracts health based on the damage of the homing shot received
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "DetectionRadius" && other.gameObject.GetComponent<DetectionRadius>().lockedOn == false){
            other.gameObject.GetComponent<DetectionRadius>().homingShot.target = transform;
            other.gameObject.GetComponent<DetectionRadius>().lockedOn = true;
        }
    }
}
