using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlasteroidsController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject laser;
    public GameObject player;
    public Vector2 target;
    public float speed;
    public float asteroidSpeed;
    private int leftRightBound = 11;
    private int topBottomBound = 7;
    private float spawnTimer;
    private float spawnTimerMax = 0.5f;
    private float laserTimer;
    private float laserTimerMax = 0.5f;
    private bool canShoot;
    private List<GameObject> asteroidList = new List<GameObject>();
    public Text scoreText;
    public Text finalScoreText;
    public GameObject scorePanel;
    public GameObject finalScorePanel;
    public GameObject startPanel;
    private int score;
    private bool gameStart;
    private Quaternion rotation;

    public Camera myCamera;
    public Canvas myCanvas;
    public GameObject phoneImage;

    
    // Start is called before the first frame update
    void Start()
    {
        myCamera.gameObject.SetActive(false);
        myCanvas.gameObject.SetActive(false);
        spawnTimer = spawnTimerMax;
        laserTimer = laserTimerMax;
        gameStart = false;
        player.SetActive(false);
        startPanel.SetActive(true);
        scorePanel.SetActive(false);
        finalScorePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart) {
            laserCooldown();
            rotatePlayer();
            spawnAsteroids();
            shootLaser();
            moveAsteroids();
        }
    }

    private void rotatePlayer() {
        if(Input.GetMouseButtonDown(0)) {
            target = myCamera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        }
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        angle -= 90;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, speed * Time.deltaTime);
    }

    private void spawnAsteroids() {
        if(spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
        } else {
            spawnTimer = spawnTimerMax;
            int spawn = Random.Range(0,2);
            if(spawn == 1) {
                float side = Random.Range(0, 4);
                float flip = Random.Range(0, 2);
                Vector3 spawnPoint;
                if(side == 0) {
                    //top
                    float x = Random.Range(0, leftRightBound);
                    x = flip == 1 ? -x : x;
                    spawnPoint = new Vector3(x, topBottomBound + 40, 10);
                } else if(side == 1) {
                    //bottom
                    float x = Random.Range(0, leftRightBound);
                    x = flip == 1 ? -x : x;
                    spawnPoint = new Vector3(x, -topBottomBound + 40, 10);
                } else if(side == 2) {
                    //left
                    float y = Random.Range(0, topBottomBound);
                    y = flip == 1 ? -y : y;
                    spawnPoint = new Vector3(-leftRightBound, y + 40, 10);
                } else if(side == 3) {
                    //right
                    float y = Random.Range(0, topBottomBound);
                    y = flip == 1 ? -y : y;
                    spawnPoint = new Vector3(leftRightBound, y + 40, 10);
                } else {
                    spawnPoint = new Vector3(0,0,10);
                }
                GameObject tempAsteroid = Instantiate(asteroid, spawnPoint, transform.rotation, transform);
                tempAsteroid.GetComponent<AsteroidController>().bc = this;
                asteroidList.Add(tempAsteroid);
            }
        }
    }

    private void moveAsteroids() {
        if(asteroidList.Count > 0) {
            float step = asteroidSpeed * Time.deltaTime;
            foreach (GameObject ast in asteroidList)
            {
                if(ast != null) {
                    Vector3 flatAsteroidPosition = new Vector3(ast.transform.position.x, ast.transform.position.y, player.transform.position.z);
                    Vector3 flatTargetPostion = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                    ast.transform.position = Vector3.MoveTowards(flatAsteroidPosition, flatTargetPostion, step);
                }
            }
        }
    }

    private void laserCooldown() {
        if(laserTimer > 0) {
            laserTimer -= Time.deltaTime;
        } else {
            laserTimer = laserTimerMax;
            canShoot = true;
        }
    }

    private void shootLaser() {
        if(Input.GetMouseButtonDown(0)) {
            if(canShoot) {
                GameObject tempLaser = Instantiate(laser, player.transform.position, rotation, transform);
                tempLaser.GetComponent<LaserController>().bc = gameObject;
                canShoot = false;
            }
        }
    }

    public void addScore() {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }

    private void resetScore() {
        score = 0;
    }

    public void playerDied() {
        gameStart = false;
        endGame();
    }

    private void endGame() {
        //display final score panel
        player.SetActive(false);
        deleteAsteroids();
        scorePanel.SetActive(false);
        finalScorePanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score.ToString();
    }

    public void quitGameButton() {
        scorePanel.SetActive(true);
        gameStart = false;
        scoreText.text = "Score: 0";
        player.SetActive(false);
        finalScorePanel.SetActive(false);
        score = 0;
        myCamera.gameObject.SetActive(false);
        myCanvas.gameObject.SetActive(false);
        phoneImage.SetActive(false);
    }

    public void resetGame() {
        scorePanel.SetActive(true);
        gameStart = true;
        scoreText.text = "Score: 0";
        player.SetActive(true);
        finalScorePanel.SetActive(false);
        score = 0;
    }

    private void deleteAsteroids() {
        foreach(GameObject ast in asteroidList) {
            Destroy(ast);
        }
        asteroidList.Clear();
    }

    public void startButton() {
        scorePanel.SetActive(true);
        gameStart = true;
        scoreText.text = "Score: 0";
        player.SetActive(true);
        finalScorePanel.SetActive(false);
        score = 0;
        startPanel.SetActive(false);
        canShoot = true;
    }

    public void openGame() {
        myCamera.gameObject.SetActive(true);
        myCanvas.gameObject.SetActive(true);
        phoneImage.SetActive(true);
        slidePhoneIntoPlace();
        startButton();
    }

    private void slidePhoneIntoPlace() {
        phoneImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -60);
    }
}
