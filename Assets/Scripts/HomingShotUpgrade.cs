using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotUpgrade : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int score = 100;
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
            GameController.instance.homingShotOn = true;//activates the spread shot upgrade on the the game controller
            GameController.instance.basicShotOn = false;//turns off the basic shot
            GameController.instance.score += score;
            GameController.instance.untilNextDose -= score;
            GameController.instance.UpgradePickupSFX();
            Destroy(gameObject);//destroys this gameObject on collision with the player

        }

        if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }

    }
}
