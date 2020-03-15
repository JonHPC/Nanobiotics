using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public Vector3 snapshot;
    private float shotSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        /*if(GameController.instance.isDead == false){
            Vector3 playerTransform = PlayerController.instance.transform.position;
            snapshot = playerTransform;
        }
        else if(GameController.instance.isDead == true){
            snapshot = new Vector3(-1, 0, 0);
        }*/


    }

    // Update is called once per frame
    void Update()
    {

        /*if(GameController.instance.isDead == true){
            Vector3 move = new Vector3(-1, 0, 0);
            //transform.position += move * shotSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, move, shotSpeed * Time.deltaTime);
        }
        else if(GameController.instance.isDead == false){

            Vector3 move = snapshot;
            //transform.position += move * shotSpeed * Time.deltaTime;
            transform.position += Vector3.MoveTowards(transform.position, move, shotSpeed * Time.deltaTime);
        }*/

        //Vector3 move = new Vector3(1, 0, 0);
        //transform.position += move * shotSpeed * Time.deltaTime;


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Boundary")
        {
            //Destroys this  shot when it collides with a boundary
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().isInvincible == false)
        {

            GameController.instance.lives -= 1;//subtracts one life upon colliding with the player
            GameController.instance.isDead = true; //changes the isDead bool to true when the player dies
            GameController.instance.playerDeathParticles(other.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
