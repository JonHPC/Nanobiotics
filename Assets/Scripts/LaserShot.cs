using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public int damage = 2;

    private float shotSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(1, 0, 0);
        transform.position += move * shotSpeed * Time.deltaTime;


    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }

    }
}
