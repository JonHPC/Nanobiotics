using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpgrade : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public int score = 100;

    void Update()
    {
        Vector3 move = new Vector3(-1, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player")
        {
            GameController.instance.bombs += 1;
            GameController.instance.bombs = Mathf.Clamp(GameController.instance.bombs, 0, 99);
            GameController.instance.score += score;
            GameController.instance.untilNextDose -= score;
            GameController.instance.BombPickupSFX();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }
    }


}
