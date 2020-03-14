using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionShotUpgrade : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(-1, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameController.instance.companionShotOn = true;//activates the spread shot upgrade on the the game controller
            
            Destroy(gameObject);//destroys this gameObject on collision with the player

        }

        else if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }
    }
}
