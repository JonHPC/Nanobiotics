using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBC : MonoBehaviour
{

    public float moveSpeed;
    public float pulse;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(-1, 0, 0);
        //moveSpeed = 2f;
        pulse = 1.6f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new Vector3(dir.x *  moveSpeed * Mathf.Lerp(1,4, Mathf.PingPong(Time.time * pulse, 1)) * Time.deltaTime, 0, 0);
    }

    public void SetSpeed(float speed){
        moveSpeed = speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
