using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Part3 : MonoBehaviour
{
    public int health = 150;
    public int score = 10000;

    [SerializeField]
    private int bulletsAmount = 50;

    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;

    private Vector2 bulletMoveDirection;

    private bool sprayOn = false;
    private bool fireOn = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 3f, 0.2f);
        StartCoroutine(changePattern());
    }

    // Update is called once per frame
    void Update()
    {

        /*if (fireOn == true)
        {
            startAngle = 0f;
            endAngle = 360f;
            InvokeRepeating("Fire", 0f, 2f);

        }*/

        if (health <= 0)
        {

            //gameController.GetComponent<GameController>().score += score; //accesses the GameController script and adds the 'score' value of the enemy
            GameController.instance.score += score; //accesses the GameController instance and adds the 'score' value of the enemy
            GameController.instance.lifeBonus += score; //adds the score amount to the lifeBonus
            GameController.instance.untilNextDose -= score;
            GameController.instance.spawnBossDrops(transform);//runs this function for a chance to drop items
            GameController.instance.bossDeathParticles(transform);
            GameController.instance.shakeNow();
            Boss3.instance.part3dead = true;
            Boss3.instance.partsRemaining -= 1;
            Destroy(gameObject);//if the health of this enemy drops to or below 0, destroy this gameObject
        }
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

    private void Fire()
    {
        if (fireOn == true)
        {
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;

            for (int i = 0; i < bulletsAmount + 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<EnemyProjectile>().SetMoveDirection(bulDir);

                angle += angleStep;
            }
        }

    }

    private void Spray()
    {


        if (sprayOn == true)
        {
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;



            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<EnemyProjectile>().SetMoveDirection(bulDir);
            angle += angleStep;

            StartCoroutine(pause());
        }


    }

    IEnumerator changePattern()
    {

        yield return new WaitForSeconds(4f);

        CancelInvoke("Fire");
        startAngle = 0f;
        endAngle = 360f;

        //fireOn = false;
       
        //InvokeRepeating("pause", 0f, 8f);

        StartCoroutine(changePattern2());
    }

    IEnumerator pause()
    {

        yield return new WaitForSeconds(0.1f);
        startAngle -= 20;
        endAngle -= 20;
        Spray();
        //yield return angle;


    }

    IEnumerator changePattern2()
    {
        yield return new WaitForSeconds(8f);
        fireOn = true;
        startAngle = 0f;
        endAngle = 360f;
        InvokeRepeating("Fire", 0f, 0.2f);


        StartCoroutine(changePattern());
        //StartCoroutine(changePattern3());
    }

    IEnumerator changePattern3()
    {

        yield return new WaitForSeconds(6f);

        CancelInvoke("Fire");
        startAngle = 0f;
        endAngle = 360f;

        //fireOn = false;

        //InvokeRepeating("pause", 0f, 8f);

        StartCoroutine(changePattern2());
    }
}
