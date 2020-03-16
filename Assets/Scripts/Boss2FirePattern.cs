using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2FirePattern : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 0f, endAngle = 360f;

    private Vector2 bulletMoveDirection;

    private bool sprayOn = false;
    private bool fireOn = false;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Fire", 0f, 2f);
        StartCoroutine(changePattern());
    }

    void Update(){

        if(fireOn == false){
            startAngle = 150f;
            endAngle = 340f;
            InvokeRepeating("Fire", 0f, 2f);
            fireOn = true;
        }
        else if(sprayOn == true){
            //StartCoroutine(pause());
        }
    }

    private void Fire()
    {
        if(fireOn == true){
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;

            for (int i = 0; i < bulletsAmount + 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<EnemyProjectile>().SetMoveDirection(bulDir);

                angle += angleStep;
            }
        }

    }

    private void Spray()
    {


        if(sprayOn == true){
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;



            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<EnemyProjectile>().SetMoveDirection(bulDir);
            angle += angleStep;

            StartCoroutine(pause());
        }


    }

    IEnumerator changePattern(){

        yield return new WaitForSeconds(8f);

        CancelInvoke("Fire");
        startAngle = 330f;
        endAngle = 240f;
        gameObject.GetComponent<Boss2>().moveSpeed = 0f;
        //fireOn = false;
        sprayOn = true;
        Spray();
        //InvokeRepeating("pause", 0f, 8f);

        StartCoroutine(changePattern2());
    }

    IEnumerator pause()
    {

        yield return new WaitForSeconds(0.1f);
        startAngle -= 20;
        endAngle -= 20;
        Spray();
        //yield return angle;


    }

    IEnumerator changePattern2(){
        yield return new WaitForSeconds(5f);
        fireOn = true;
        startAngle = 150f;
        endAngle = 340f;
        InvokeRepeating("Fire", 0f, 2f);
        sprayOn = false;
        gameObject.GetComponent<Boss2>().moveSpeed = 2f;

        StartCoroutine(changePattern3());
    }

    IEnumerator changePattern3()
    {

        yield return new WaitForSeconds(10f);

        CancelInvoke("Fire");
        startAngle = 330f;
        endAngle = 240f;
        gameObject.GetComponent<Boss2>().moveSpeed = 0f;
        //fireOn = false;
        sprayOn = true;
        Spray();
        //InvokeRepeating("pause", 0f, 8f);

        StartCoroutine(changePattern2());
    }
}
