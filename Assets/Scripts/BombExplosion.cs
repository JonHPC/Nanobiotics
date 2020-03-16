using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public Vector3 scaleUp;

    void Start(){
        scaleUp = new Vector3(0.5f, 0.5f, 0f);
        StartCoroutine(explode());
        StartCoroutine(destroyBomb());
    }

    IEnumerator explode(){
        gameObject.transform.localScale += scaleUp;
        yield return new WaitForSeconds(0.025f);
        StartCoroutine(explode2());
    }

    IEnumerator explode2(){
        gameObject.transform.localScale += scaleUp;
        yield return new WaitForSeconds(0.025f);
        StartCoroutine(explode());
    }

    IEnumerator destroyBomb()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
