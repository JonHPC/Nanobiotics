using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public int health = 200;
    public float moveSpeed = 2f;
    public int score = 10000;


    public Transform bossCannon;

    //private float moveSpeed;
    private bool moveUp;
    private bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f;
        moveUp = true;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {

            //gameController.GetComponent<GameController>().score += score; //accesses the GameController script and adds the 'score' value of the enemy
            GameController.instance.score += score; //accesses the GameController instance and adds the 'score' value of the enemy
            GameController.instance.lifeBonus += score; //adds the score amount to the lifeBonus
            GameController.instance.untilNextDose -= score;
            GameController.instance.spawnBossDrops(transform);//runs this function for a chance to drop items
            GameController.instance.bossDeathParticles(transform);
            GameController.instance.shakeNow();
            GameController.instance.boss2Alive = false;
            Destroy(gameObject);//if the health of this enemy drops to or below 0, destroy this gameObject
        }


        movePattern();

      

    }



    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "BasicShot")
        {
            health -= other.gameObject.GetComponent<BasicShot>().damage; //subtracts health based on the damage of the shot received
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "SpreadShot")
        {
            health -= other.gameObject.GetComponent<SpreadShot>().damage; //subtracts health based on the damage of the spread shot received
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "LaserShot")
        {
            health -= other.gameObject.GetComponent<LaserShot>().damage; //subtracts health based on the damage of the laser shot received
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "HomingShot")
        {
            //health -= other.gameObject.GetComponent<HomingShot>().damage; //subtracts health based on the damage of the homing shot received
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "BackShot")
        {
            health -= other.gameObject.GetComponent<BackShot>().damage; //subtracts health based on the damage of the back shot received
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController>().isInvincible == false)
        {
            //gameController.GetComponent<GameController>().lives -= 1;//subtracts one life upon colliding with the player
            GameController.instance.lives -= 1;//subtracts one life upon colliding with the player
            GameController.instance.isDead = true; //changes the isDead bool to true when the player dies
            GameController.instance.shakeNow();
            GameController.instance.playerDeathParticles(other.transform);
            Destroy(other.gameObject);//destroys the player upon collision

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HomingShot")
        {
            health -= other.gameObject.GetComponent<HomingShot>().damage; //subtracts health based on the damage of the homing shot received
            GameController.instance.enemyHitParticles(other.transform);
            GameController.instance.HomingExplosion(transform);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "HomingExplosion")
        {
            health -= other.gameObject.GetComponent<HomingExplosion>().damage;
            GameController.instance.enemyHitParticles(other.transform);
        }
        else if (other.gameObject.tag == "DetectionRadius" && other.gameObject.GetComponent<DetectionRadius>().lockedOn == false)
        {
            other.gameObject.GetComponent<DetectionRadius>().homingShot.target = transform;
            other.gameObject.GetComponent<DetectionRadius>().lockedOn = true;
        }
        else if (other.gameObject.tag == "Bomb")
        {
            health -= 10;
        }
    }

    void movePattern(){

        if (transform.position.y > 4.5f)
        {
            moveUp = false;
        }
        else if (transform.position.y < -4.5f)
        {
            moveUp = true;
        }

        if (moveUp == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }

        if (transform.position.x > 8f)
        {
            moveRight = false;
        }
        else if (transform.position.x < 2f)
        {
            moveRight = true;
        }

        if (moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
