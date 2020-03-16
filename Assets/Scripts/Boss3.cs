using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    public static Boss3 instance = null;
    public bool part1dead = false;
    public bool part2dead = false;
    public bool part3dead = false;

    public Vector3 rotateAmount;

    public float moveSpeed;

    public int partsRemaining = 3;

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

    public bool moveUp;
    public bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        rotateAmount = new Vector3(0, 0, 1.5f);
        moveUp = true;
        moveRight = true;
        moveSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount);
        checkPartsDead();
        move();
    }

    void checkPartsDead(){

        if(partsRemaining == 3){
            rotateAmount = new Vector3(0, 0, 1.5f);
            moveSpeed = 2f;
        }
        else if(partsRemaining == 2){
            rotateAmount = new Vector3(0, 0, 2.0f);
            moveSpeed = 3f;
        }
        else if(partsRemaining == 1){
            rotateAmount = new Vector3(0, 0, 2.5f);
            moveSpeed = 4f;
        }
        else if(partsRemaining == 0)
        {
            //Debug.Log("You win!");
            GameController.instance.boss3Alive = false;
        }
    }

    void move(){
        

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
