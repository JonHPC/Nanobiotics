using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCController : MonoBehaviour
{
    public GameObject[] rbc;

    public Transform[] spawns;

    public float spawnRate;

    public float moveSpeed;

    private float timer;
    private Vector3 cellSize;


    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 0.7f; 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate){



            int randomAmount = Random.Range(4, 10);




            for (int i = 0; i < randomAmount; i++ )
            {
                int spawnPoint = Random.Range(0, 9);
                int randomRBC = Random.Range(0, 4);
                float randomSize = Random.Range(1, 4);

                cellSize = new Vector3(randomSize, randomSize, 0);

                GameObject rbc1 = Instantiate(rbc[randomRBC], spawns[spawnPoint].position, Quaternion.identity) as GameObject;
                rbc1.transform.localScale = cellSize;
                moveSpeed = randomSize * 2;
                rbc1.GetComponent<RBC>().moveSpeed = moveSpeed;

            }


            timer = 0;
        }







    }
}
