using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{

    public AudioSource startSound;
    public AudioSource uiSound;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }



    public void Restart()
    {

        startSound.Play();
        Time.timeScale = 1f;
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);//starts the game and loads the scene "Level1"
    }

    public void Title()
    {
        SceneManager.LoadScene(0);
    }

    public void buttonHighlighted()
    {
        uiSound.Play();
    }

    public void buttonLeave()
    {
        uiSound.Play();
    }
}
