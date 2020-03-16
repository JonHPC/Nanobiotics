using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingExplosion : MonoBehaviour
{
    public int damage = 4;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(decay());
    }

    IEnumerator decay()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Boundary")
        {
            //Destroys this basic shot when it collides with a boundary
            Destroy(gameObject);
        }
        /*else if (other.gameObject.tag == "Enemy")
        {
            //Destroy this basic shot when it collides with an enemy
            Destroy(gameObject);
        }*/
    }
}
