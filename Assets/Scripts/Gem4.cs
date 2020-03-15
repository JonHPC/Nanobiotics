using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem4 : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public int score = 100;


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
            GameController.instance.score += score;//adds this objects score to the total score
            GameController.instance.lifeBonus += score;
            Destroy(gameObject);//destroys this gameObject on collision with the player

        }

        else if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }
    }
}
