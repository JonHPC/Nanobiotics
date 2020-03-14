using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null; //declaring and initializing a public static GameController class to null. 
    public int lives;
    public int score;
    public bool isDead;

    public bool basicShotOn = true;
    public bool spreadShotOn = false;
    public bool laserShotOn = false;
    public bool homingShotOn = false;
    public bool companionShotOn = false;
    public bool backShotOn = false;

    void Awake(){
        //Determine if our instance is null
        if (instance == null){
            instance = this; //assign instance to this instance of the class
        }
        //Determine if our instance is already assigned to something else   
        else if (instance != this){
            Destroy(gameObject); //since we already have a GameController assigned somewhere else, we don't need a duplicate
        } 
            
    }


    public TextMeshProUGUI playerLivesText;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI levelText;

    public GameObject[] upgradeText;
    public GameObject[] upgrades;

    public int level;

    public Transform[] spawnPoints;
    public Transform playerSpawnPoint;

    public GameObject enemy1;
    public float spawnRate = 0.5f;

    public GameObject player;


    private float timer;

    void Start(){
        lives = PlayerPrefs.GetInt("lives", 3); //initializes the amount of playerLives
        score = PlayerPrefs.GetInt("score", 0); //initializes the score
        level = SceneManager.GetActiveScene().buildIndex; //gets and stores the buildIndex as a int
        isDead = false;
    }

    void Update(){

        updateUI(); //updates the UI text every frame
        spawnEnemy(); //spawns random enemies
        playerDead();//checks to see if the player is dead

    }

    void FixedUpdate(){
        checkUpgrades();
    }

    void updateUI(){

        playerLivesText.text = "x " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        levelText.text = "Level: " + level.ToString();
    }

    void spawnEnemy(){

        timer += Time.deltaTime;

        if(timer > spawnRate){
            int spawn = Random.Range(0, 9);
            GameObject enemy = Instantiate(enemy1, spawnPoints[spawn].position, Quaternion.identity) as GameObject;
            timer = 0;
        }

    }

    void playerDead(){
        if(isDead == true){
            StartCoroutine(playerRespawn());
            basicShotOn = true;
            spreadShotOn = false;
            laserShotOn = false;
            homingShotOn = false;
            companionShotOn = false;
            backShotOn = false;
        }
    }

    IEnumerator playerRespawn(){

        isDead = false;
        //Debug.Log("About to respawn");
        yield return new WaitForSeconds(2f);
        //Debug.Log("Player is respawned");
        GameObject spawnedPlayer = Instantiate(player, playerSpawnPoint.position, Quaternion.identity) as GameObject;
    }

    void checkUpgrades()
    {
        if(basicShotOn == true){
            //basicShotOn = true;
            spreadShotOn = false;
            laserShotOn = false;
            homingShotOn = false;

            backShotOn = false;

            upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);

        }
        else if(spreadShotOn == true){
            basicShotOn = false;
            //spreadShotOn = true;
            laserShotOn = false;
            homingShotOn = false;

            backShotOn = false;

            upgradeText[0].SetActive(true);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);
        }
        else if(laserShotOn == true){
            basicShotOn = false;
            spreadShotOn = false;
            //laserShotOn = true;
            homingShotOn = false;

            backShotOn = false;

            upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(true);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);
        }
        else if(homingShotOn ==true){
            basicShotOn = false;
            spreadShotOn = false;
            laserShotOn = false;
            //homingShotOn = true;

            backShotOn = false;

            upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(true);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);
        }
       
        else if(backShotOn == true){
            basicShotOn = false;
            spreadShotOn = false;
            laserShotOn = false;
            homingShotOn = false;

            //backShotOn = true;

            upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(true);
        }

        if (companionShotOn == true)
        {
            //basicShotOn = false;
            //spreadShotOn = false;
            //laserShotOn = false;
            //homingShotOn = false;
            //companionOn = true;
            //backShotOn = false;

            //upgradeText[0].SetActive(false);
            //upgradeText[1].SetActive(false);
            //upgradeText[2].SetActive(false);
            upgradeText[3].SetActive(true);
            //upgradeText[4].SetActive(false);
        }
        else{
            upgradeText[3].SetActive(false);
        }

    }

    public void spawnUpgrade(Transform enemyTransform){
        int chanceOfDrop = Random.Range(0, 5);

        if(chanceOfDrop == 1){
            int chanceOfUpgrade = Random.Range(0, 5);
            if(chanceOfUpgrade == 0){
                GameObject upgradeItem = Instantiate(upgrades[0], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if(chanceOfUpgrade == 1){
                GameObject upgradeItem = Instantiate(upgrades[1], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfUpgrade == 2)
            {
                GameObject upgradeItem = Instantiate(upgrades[2], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfUpgrade == 3)
            {
                GameObject upgradeItem = Instantiate(upgrades[3], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfUpgrade == 4)
            {
                GameObject upgradeItem = Instantiate(upgrades[4], enemyTransform.position, Quaternion.identity) as GameObject;
            }
        }
    }
}
