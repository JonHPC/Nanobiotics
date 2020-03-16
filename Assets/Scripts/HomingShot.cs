using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingShot : MonoBehaviour
{
    public Transform target;
    [SerializeField] float shotSpeed = 300.0f;
    [SerializeField] float rotateSpeed = 2000f;

    public int damage = 8;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        target = GameObject.FindGameObjectWithTag("MiddleSpawn").transform;

    }

    void Update(){

        rb.velocity = transform.up * shotSpeed * Time.deltaTime;

        Vector3 targetVector = target.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.deltaTime;
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
