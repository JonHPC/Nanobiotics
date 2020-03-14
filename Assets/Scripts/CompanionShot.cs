using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionShot : MonoBehaviour
{
    public GameObject basicShot;
    public Transform shotSpawn;
    public float fireRate = 0.1f;

    private float timer;
    private Transform shotSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        shotSpawnTransform = this.gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > fireRate)
        {
            if (Input.GetKey("space"))
            {
                GameObject shot = Instantiate(basicShot, shotSpawnTransform.position, Quaternion.identity) as GameObject;

            }

            timer = 0;
        }
    }
}
