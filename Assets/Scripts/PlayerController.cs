using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance = null; //declaring and initializing a public static GameController class to null. 
    public Transform playerTransform;

    void Awake()
    {
        //Determine if our instance is null
        if (instance == null)
        {
            instance = this; //assign instance to this instance of the class
        }
        //Determine if our instance is already assigned to something else   
        else if (instance != this)
        {
            Destroy(gameObject); //since we already have a GameController assigned somewhere else, we don't need a duplicate
        }

    }




    public float playerSpeed;
    public GameObject basicShot;
    public GameObject spreadShot;
    public GameObject laserShot;
    public GameObject homingShot;
    //public GameObject companion;
    public GameObject backShot;
    public GameObject shotSpawn;
    public GameObject shotSpawn1;
    public GameObject shotSpawn2;
    public GameObject shotSpawnBack;
    public GameObject companion;

    public GameObject bomb;
    public bool isInvincible = true;

    public float startAngle;
    public float endAngle;

    private SpriteRenderer spriteColor;
    private float timer;
    private float fireRate = 0.1f;
    private Rigidbody2D rb;
    private Transform shotSpawnTransform;
    private Transform shotSpawnTransform1;
    private Transform shotSpawnTransform2;
    private Transform shotSpawnTransformBack;
    private Vector2 bulletMoveDirection;
    public int bulletsAmount = 3;

    private bool moveUp = false;
    private bool moveDown = false;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 10.0f;
        shotSpawnTransform = this.gameObject.transform.GetChild(1);//gets the transform of the child(1)
        shotSpawnTransform1 = this.gameObject.transform.GetChild(2);
        shotSpawnTransform2 = this.gameObject.transform.GetChild(3);
        shotSpawnTransformBack = this.gameObject.transform.GetChild(4);
        playerTransform = transform;
        companion.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        spriteColor = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(spawnInvincible());
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        shoot();
        checkCompanion();
        explodeBomb();
    }

    void movePlayer(){

        //checks the player input to move the player forward/backwards
        if (Input.GetKey("d") && transform.position.x < 8f){
            //Debug.Log("Move forward");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
            transform.position += move * playerSpeed * Time.deltaTime;

        }
        else if(Input.GetKey("a") && transform.position.x > -8f){
            //Debug.Log("Move backwards");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;
        }
        //checks player input to move the player up/down
        else if (Input.GetKey("w") && transform.position.y < 4.5f )
        {
            //Debug.Log("Move up");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;
            moveUp = true;
            moveDown = false;
            anim.SetBool("moveUp", true);
            anim.SetBool("moveDown", false);
        }
        else if (Input.GetKey("s") && transform.position.y > -4.5f){
            //Debug.Log("Move down");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;
            moveDown = true;
            moveUp = false;
            anim.SetBool("moveDown", true);
            anim.SetBool("moveUp", false);
        }
        else{
            moveDown = false;
            moveUp = false;
            anim.SetBool("moveDown", moveDown);
            anim.SetBool("moveUp", moveUp);
        }
    }
    

    void shoot(){

        timer += Time.deltaTime;

        if(timer > fireRate){
            if (Input.GetKey("space") && GameController.instance.basicShotOn == true)
            {
                fireRate = 0.1f;
                GameObject shot = Instantiate(basicShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                timer = 0;

            }
            else if(Input.GetKey("space") && GameController.instance.spreadShotOn == true){

                fireRate = 0.2f;
                startAngle = 60f;
                endAngle = 120f;

                GameObject shot0 = Instantiate(spreadShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                GameObject shot1 = Instantiate(spreadShot, shotSpawnTransform1.position, Quaternion.identity) as GameObject;
                GameObject shot2 = Instantiate(spreadShot, shotSpawnTransform2.position, Quaternion.identity) as GameObject;
                /*float angleStep = (endAngle - startAngle) / bulletsAmount;
                float angle = startAngle;

                for (int i = 0; i < bulletsAmount + 1; i++)
                {
                    float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                    Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                    GameObject bul = PlayerBulletPool.bulletPoolInstance.GetBullet();
                    bul.transform.position = transform.position;
                    bul.transform.rotation = transform.rotation;
                    bul.SetActive(true);
                    bul.GetComponent<SpreadProjectile>().SetMoveDirection(bulDir);

                    angle += angleStep;
                }*/
                timer = 0;
            }
            else if(Input.GetKey("space") && GameController.instance.laserShotOn == true){

                fireRate = 0.3f;
                GameObject shot = Instantiate(laserShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                timer = 0;
            }
            else if(Input.GetKey("space") && GameController.instance.homingShotOn == true){
                fireRate = 0.3f;
                GameObject shot = Instantiate(homingShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                timer = 0;
            }
            else if(Input.GetKey("space") && GameController.instance.backShotOn == true){
                fireRate = 0.1f;
                GameObject shot = Instantiate(basicShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                GameObject shot1 = Instantiate(backShot, shotSpawnTransformBack.position, Quaternion.identity) as GameObject;
                timer = 0;
            }
            timer = 0;
        }

    }

    IEnumerator spawnInvincible(){

        isInvincible = true;
        anim.SetBool("isInvincible", true);
        
        yield return new WaitForSeconds(2f);
        isInvincible = false;
        anim.SetBool("isInvincible", false);
    }

    void checkCompanion(){
        if(GameController.instance.companionShotOn == true){
            companion.SetActive(true);
        }
    }

    void explodeBomb(){
        if(Input.GetKeyDown("b") && GameController.instance.bombs > 0){
            //Debug.Log("BOMB");
            GameController.instance.bombs -= 1;
            GameController.instance.bombs = Mathf.Clamp(GameController.instance.bombs, 0, 99);

            GameObject bomb1 = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;


            /*for (int i = 0; i < GameController.instance.currentEnemies.Length; i++)
            {
                Destroy(GameController.instance.currentEnemies[i]);
            }*/
        }
    }




    //saving grace, have one item slot available which upgrades your shot to shoot behind you as well, clearing out the blockage
    //if you miss enemies, they pile up behind you, reducing your screen space and increasing danger
}
