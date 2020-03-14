using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void startGame(){
        //Start game here
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);//starts the game and loads the scene "Level1"
    }

    public void quitGame(){
        //Quit game here
        Debug.Log("Quit Game");
        Application.Quit();//quits the game
    }
}
