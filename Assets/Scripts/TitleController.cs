using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public AudioSource startSound;
    public AudioSource uiSound;

    public void startGame(){
        //Start game here
        //Debug.Log("Start Game");
        startSound.Play();
        StartCoroutine(start());

    }

    public void quitGame(){
        //Quit game here
        //Debug.Log("Quit Game");
        Application.Quit();//quits the game
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);//starts the game and loads the scene "Level1"
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
