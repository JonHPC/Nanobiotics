﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance = null; //declaring and initializing a public static GameController class to null. 
    public int lives;
    public int score;
    public int lifeBonus;
    public int untilNextDose;
    public int bombs;
    public bool isDead;

    public bool basicShotOn = true;
    public bool spreadShotOn = false;
    public bool laserShotOn = false;
    public bool homingShotOn = false;
    public bool companionShotOn = false;
    public bool backShotOn = false;

    public bool boss1Alive = true;
    public bool boss1Spawned = false;
    public bool boss2Alive = true;
    public bool boss2Spawned = false;
    public bool boss3Alive = true;
    public bool boss3Spawned = false;

    public bool startWave = true;

    public bool ableToRespawn = true;

    public GameObject[] currentEnemies;

    public GameObject homingExplosion;

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
    public TextMeshProUGUI bombsText;

    public GameObject winScreen;
    public GameObject loseScreen;

    public GameObject[] upgradeText;
    public GameObject[] upgrades;
    public GameObject[] drops;
    public GameObject itemExpander;

    public int level;
    public int numberOfEnemies = 10;
    public int wave = 1;

    public Transform[] spawnPoints;
    public Transform playerSpawnPoint;

    public GameObject[] enemy;
    public GameObject enemy1;
    public float spawnRate = 0.5f;


    public GameObject[] boss;
    public GameObject boss1;
    public Transform boss1SpawnPoint;

    public GameObject player;

    public GameObject enemyDeath;
    public GameObject enemyHit;
    public GameObject playerDeath;
    public GameObject playerDeathRewind;

    public AudioSource lifeObtained;
    public AudioSource gemPickup;
    public AudioSource bombPickup;
    public AudioSource upgradePickup;

    public ShakeBehavior shake;

    private float timer;

    void Start(){
        lives = PlayerPrefs.GetInt("lives", 10); //initializes the amount of playerLives
        score = PlayerPrefs.GetInt("score", 0); //initializes the score
        level = SceneManager.GetActiveScene().buildIndex; //gets and stores the buildIndex as a int
        isDead = false;
        spawnRate = 0.7f;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        Time.timeScale = 1f;
        ableToRespawn = true;
        untilNextDose = 10000;
                    
    }

    void Update(){

        playerLoses();
        updateUI(); //updates the UI text every frame
        spawnEnemy(); //spawns random enemies
        playerDead();//checks to see if the player is dead
        lifeBonusScore();//checks to see if player gets a bonus life
        UpdateCurrentEnemiesArray();

    }

    void FixedUpdate(){
        checkUpgrades();
    }

    void updateUI(){

        playerLivesText.text = "x " + lives.ToString();
        scoreText.text = "Until next dose: " + untilNextDose.ToString();
        levelText.text = "Wave: " + wave.ToString();
        bombsText.text = "  x " + bombs.ToString();
    }

    void spawnEnemy(){

        timer += Time.deltaTime;

        if(wave == 1 && startWave == true){
            if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);
                GameObject enemySpawn = Instantiate(enemy[1], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                timer = 0;
                numberOfEnemies -= 1;
            }
            else if(numberOfEnemies <= 0){

                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 20;
                spawnRate = 0.5f;
            }
            
        }
        else if(wave == 2 && startWave == true){
            if(timer > spawnRate && numberOfEnemies >= 1){
                int spawn = Random.Range(0, 9);
                GameObject enemySpawn = Instantiate(enemy[0], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                timer = 0;
                numberOfEnemies -= 1;
            }
            else if(numberOfEnemies <= 0){
               
                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 0;

            }
        }
        else if(wave == 3 && startWave == true){


            if(boss1Spawned == false) {
                GameObject enemyBossSpawn = Instantiate(boss[0], boss1SpawnPoint.position, Quaternion.identity) as GameObject;
                boss1Spawned = true;
            }
            else if(wave == 3 && boss1Alive == false){
                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 50;
                spawnRate = 0.3f;
            }
        }
        else if(wave == 4 && startWave == true){
            int spawn = Random.Range(0, 9);
            if (timer > spawnRate && numberOfEnemies >= 1){
                GameObject enemySpawn = Instantiate(enemy[0], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                numberOfEnemies -= 1;
            }
            else if(numberOfEnemies <= 0){
                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 50;
                spawnRate = 0.7f;
            }
            

        }

        else if(wave == 5 && startWave == true){
            if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);
                GameObject enemySpawn = Instantiate(enemy[1], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                timer = 0;
                numberOfEnemies -= 1;
            }
            else if (numberOfEnemies <= 0)
            {

                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 25;
            }
        }

        else if (wave == 6 && startWave == true){
            if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);
                int randomEnemy = Random.Range(0, 10);

                if (randomEnemy <= 4)
                {
                    GameObject enemySpawn = Instantiate(enemy[1], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
                else if (randomEnemy > 4 && randomEnemy <= 7)
                {
                    GameObject enemySpawn = Instantiate(enemy[0], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemy[2], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
            }

            else if (numberOfEnemies <= 0)
            {

                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 0;
            }
        }

        else if (wave == 7 && startWave == true)
        {


            if (boss2Spawned == false)
            {
                GameObject enemyBossSpawn = Instantiate(boss[1], boss1SpawnPoint.position, Quaternion.identity) as GameObject;
                boss2Spawned = true;
            }
            else if (wave == 7 && boss2Alive == false)
            {
                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 50;
                spawnRate = 0.7f;
            }
        }

        else if (wave == 8 && startWave == true)
        {
            if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);
                int randomEnemy = Random.Range(0, 10);

                if (randomEnemy <= 4)
                {
                    GameObject enemySpawn = Instantiate(enemy[1], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
                else if (randomEnemy > 4 && randomEnemy <= 7)
                {
                    GameObject enemySpawn = Instantiate(enemy[0], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemy[2], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;
                }
            }

            else if (numberOfEnemies <= 0)
            {

                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 20;
                spawnRate = 0.5f;
            }
        }

        else if (wave == 9 && startWave == true)
        {
            if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);



                    GameObject enemySpawn = Instantiate(enemy[2], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                    timer = 0;
                    numberOfEnemies -= 1;

            }

            else if (numberOfEnemies <= 0)
            {

                StartCoroutine(timeBetweenWaves(5f));
                numberOfEnemies = 100;
                spawnRate = 8f;
            }
        }

        else if (wave == 10 && startWave == true)
        {


            if (boss3Spawned == false)
            {
                GameObject enemyBossSpawn = Instantiate(boss[2], boss1SpawnPoint.position, Quaternion.identity) as GameObject;
                boss3Spawned = true;
            }
            else if (timer > spawnRate && numberOfEnemies >= 1)
            {
                int spawn = Random.Range(0, 9);

                GameObject enemySpawn = Instantiate(enemy[2], spawnPoints[spawn].position, Quaternion.identity) as GameObject;
                timer = 0;
                numberOfEnemies -= 1;

            }
            else if (wave == 10 && boss3Alive == false)
            {
                //Debug.Log("Win screen");
                winScreen.SetActive(true);
                //StartCoroutine(PauseLose());
            }
        }




    }

    IEnumerator timeBetweenWaves(float time){
        wave += 1;
        startWave = false;
        yield return new WaitForSeconds(time);
        startWave = true;
    }

    void playerDead(){
        if(isDead == true && ableToRespawn == true){
            StartCoroutine(playerRespawn());
            basicShotOn = true;
            spreadShotOn = false;
            laserShotOn = false;
            homingShotOn = false;
            companionShotOn = false;
            backShotOn = false;
        }
        else if (ableToRespawn == false){

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

            /*upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);*/

        }
        else if (homingShotOn == true)
        {
            basicShotOn = false;
            spreadShotOn = false;
            laserShotOn = false;
            //homingShotOn = true;

            backShotOn = false;

            /*upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(true);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);*/
        }
        else if(spreadShotOn == true){
            basicShotOn = false;
            //spreadShotOn = true;
            laserShotOn = false;
            homingShotOn = false;

            backShotOn = false;

            /*upgradeText[0].SetActive(true);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);*/
        }
        else if(laserShotOn == true){
            basicShotOn = false;
            spreadShotOn = false;
            //laserShotOn = true;
            homingShotOn = false;

            backShotOn = false;

            /*upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(true);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(false);*/
        }

       
        else if(backShotOn == true){
            basicShotOn = false;
            spreadShotOn = false;
            laserShotOn = false;
            homingShotOn = false;

            //backShotOn = true;

            /*upgradeText[0].SetActive(false);
            upgradeText[1].SetActive(false);
            upgradeText[2].SetActive(false);
            //upgradeText[3].SetActive(false);
            upgradeText[4].SetActive(true)*/
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
            //upgradeText[3].SetActive(true);
            //upgradeText[4].SetActive(false);
        }
        else{
            //upgradeText[3].SetActive(false);
        }

    }

    public void spawnUpgrade(Transform enemyTransform){
        int chanceOfDrop = Random.Range(0, 5);

        if(chanceOfDrop == 1){
            int chanceOfUpgrade = Random.Range(0, 7);
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
        else if(chanceOfDrop == 2){
            int chanceOfGem = Random.Range(0, 12);
            if(chanceOfGem == 0){
                GameObject gemItem = Instantiate(drops[0], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfGem == 1)
            {
                GameObject gemItem = Instantiate(drops[1], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfGem >= 5 && chanceOfGem < 7)
            {
                GameObject gemItem = Instantiate(drops[2], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if (chanceOfGem >= 7 && chanceOfGem < 10)
            {
                GameObject gemItem = Instantiate(drops[3], enemyTransform.position, Quaternion.identity) as GameObject;
            }
            else if(chanceOfGem >= 11)
            {
                GameObject gemItem = Instantiate(drops[4], enemyTransform.position, Quaternion.identity) as GameObject;
            }
        }
    }

    public void spawnBossDrops(Transform enemyTransform){

        //guarantees an upgrade drop
        int chanceOfUpgrade = Random.Range(0, 5);
        if(chanceOfUpgrade == 0)
        {
            GameObject upgradeItem = Instantiate(upgrades[0], enemyTransform.position, Quaternion.identity) as GameObject;
        }
        else if (chanceOfUpgrade == 1)
        {
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

        int numberOfGems = Random.Range(3, 7);

        //int randomSpawn = Random.Range(1, 4);

        for (int i = 0; i < numberOfGems; i++){
            int chanceOfGems = Random.Range(0, 5);
            //GameObject dropGem = Instantiate(drops[i], enemyTransform.position- new Vector3(randomSpawn, randomSpawn,0), Quaternion.identity) as GameObject;
            GameObject dropGem = Instantiate(drops[chanceOfGems],enemyTransform.position, Quaternion.identity) as GameObject;

        }

        GameObject itemExpand = Instantiate(itemExpander, enemyTransform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyParticles(itemExpand));

    }

    public void bossDeathParticles(Transform transform){
        GameObject bossDeathParticles1 = Instantiate(enemyDeath, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyPlayerParticles(bossDeathParticles1));
    }

    public void enemyDeathParticles(Transform transform){
        GameObject enemyDeathParticles1 = Instantiate(enemyHit, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyParticles(enemyDeathParticles1));
    }

    public void enemyHitParticles(Transform transform){
        GameObject enemyHitParticles1 = Instantiate(enemyHit, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyEnemyHitParticles(enemyHitParticles1));
    }

    IEnumerator destroyParticles(GameObject particles){


        yield return new WaitForSeconds(2.0f);
        Destroy(particles);
       
    }

    IEnumerator destroyEnemyHitParticles(GameObject particles){
        yield return new WaitForSeconds(0.1f);
        Destroy(particles);
    }

    IEnumerator destroyPlayerParticles(GameObject particles)
    {


        yield return new WaitForSeconds(3.0f);
        Destroy(particles);

    }

    public void playerDeathParticles(Transform transform){
        GameObject playerDeathParticles1 = Instantiate(playerDeath, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyPlayerParticles(playerDeathParticles1));
        GameObject playerDeathParticlesRewind1 = Instantiate(playerDeathRewind, playerSpawnPoint.position, Quaternion.identity) as GameObject;

    }

    public void lifeBonusScore(){

        if(untilNextDose <= 0){
            lifeBonus = 0;
            untilNextDose = 10000;
            lives += 1;//player gets a life everytime the player gets 10,000 pts
            lifeObtained.Play();
        }
    }

    public void UpdateCurrentEnemiesArray()
    {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");


    }

    public void PlayGemPickup()
    {
        gemPickup.Play();
    }

    public void BombPickupSFX()
    {
        bombPickup.Play();
    }

    public void UpgradePickupSFX()
    {
        upgradePickup.Play();
    }

    public void HomingExplosion(Transform transform)
    {
        GameObject homing = Instantiate(homingExplosion, transform.position, Quaternion.identity) as GameObject;
    }

    public void playerLoses()
    {
        if(lives <= 0){

            loseScreen.SetActive(true);
            ableToRespawn = false;
            //StartCoroutine(PauseLose());
        }
    }

    IEnumerator PauseLose()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public void shakeNow()
    {
        shake.TriggerShake();
    }
}
