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
    public bool isInvincible = true;



    private SpriteRenderer spriteColor;
    private float timer;
    private float fireRate = 0.1f;
    private Rigidbody2D rb;
    private Transform shotSpawnTransform;
    private Transform shotSpawnTransform1;
    private Transform shotSpawnTransform2;
    private Transform shotSpawnTransformBack;

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
    }

    void movePlayer(){

        //checks the player input to move the player forward/backwards
        if (Input.GetKey("d")){
            //Debug.Log("Move forward");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
            transform.position += move * playerSpeed * Time.deltaTime;

        }
        else if(Input.GetKey("a")){
            //Debug.Log("Move backwards");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;
        }
        //checks player input to move the player up/down
        else if (Input.GetKey("w"))
        {
            //Debug.Log("Move up");
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * playerSpeed * Time.deltaTime;
            moveUp = true;
            moveDown = false;
            anim.SetBool("moveUp", true);
            anim.SetBool("moveDown", false);
        }
        else if (Input.GetKey("s")){
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
                GameObject shot0 = Instantiate(spreadShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;
                GameObject shot1 = Instantiate(spreadShot, shotSpawnTransform1.position, Quaternion.identity) as GameObject;
                GameObject shot2 = Instantiate(spreadShot, shotSpawnTransform2.position, Quaternion.identity) as GameObject;
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




    //saving grace, have one item slot available which upgrades your shot to shoot behind you as well, clearing out the blockage
    //if you miss enemies, they pile up behind you, reducing your screen space and increasing danger
}
