using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour
{
    public int damage = 1;

    private float shotSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(1 , 0 , 0);
        transform.position += move * shotSpeed * Time.deltaTime;

        
    }

    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Boundary"){
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemy"){
            //Destroy this basic shot when it collides with an enemy
            Destroy(gameObject);
        }
    }
}
