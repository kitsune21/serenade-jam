using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlasteroidsController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject laser;
    public GameObject player;
    public Vector2 target;
    public float speed;
    private int leftRightBound = 12;
    private int topBottomBound = 8;
    private float spawnTimer;
    private float spawnTimerMax = 0.34f;
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
    public GameObject mainCanvas;
    private GraphicRaycaster raycaster;
    private PointerEventData clickData;
    private List<RaycastResult> clickResults;
    private List<GameObject> clickedElements;
    private bool isDragging = false;
    private Vector3 mousePosition;
    private Vector3 prevMousePosition;
    private GameObject draggingElement;

    public MusicController musicController;
    public SoundController soundController;
    private bool vampTransition = false;

    public GameObject loadingPanel;
    public Slider loadingBar;
    public bool readyToPlay = false;

    
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
        loadingPanel.SetActive(false);
        raycaster = mainCanvas.GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
        clickedElements = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseDragUi();
        if (gameStart) {
            laserCooldown();
            rotatePlayer();
            spawnAsteroids();
            shootLaser();
            moveAsteroids();
        }
        if(vampTransition)
        {
            loadingBar.value += Time.deltaTime;
            if(musicController.getClipStatus())
            {
                waitTillVamp();
                vampTransition = false;
                loadingPanel.SetActive(false);
            }
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
                tempAsteroid.GetComponent<AsteroidController>().speed = Random.Range(0.6f, 2.8f);
                tempAsteroid.GetComponent<AsteroidController>().rotate(player.transform.rotation);
                asteroidList.Add(tempAsteroid);
            }
        }
    }

    private void moveAsteroids() {
        if(asteroidList.Count > 0) {
            foreach (GameObject ast in asteroidList)
            {
                if(ast != null) {
                    Vector3 flatAsteroidPosition = new Vector3(ast.transform.position.x, ast.transform.position.y, player.transform.position.z);
                    Vector3 flatTargetPostion = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                    ast.transform.position = Vector3.MoveTowards(flatAsteroidPosition, flatTargetPostion, ast.GetComponent<AsteroidController>().speed * Time.deltaTime);
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
                soundController.playEffect("laser");
            }
        }
    }

    public void addScore() {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
        soundController.playEffect("explosion");
    }

    public void playerDied() {
        gameStart = false;
        endGame();
    }

    private void endGame() {
        //display final score panel
        soundController.playEffect("explosion");
        player.SetActive(false);
        deleteAsteroids();
        scorePanel.SetActive(false);
        finalScorePanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score.ToString();
    }

    public void quitGameButton() {
        deleteAsteroids();
        scorePanel.SetActive(true);
        gameStart = false;
        scoreText.text = "Score: 0";
        player.SetActive(false);
        finalScorePanel.SetActive(false);
        score = 0;
        myCamera.gameObject.SetActive(false);
        myCanvas.gameObject.SetActive(false);
        if(musicController.isClipPlaying("blasteroids-main") || musicController.isClipPlaying("blasteroids-vamp"))
        {
            musicController.stopClip();
        }
        loadingBar.value = 0;
        loadingPanel.SetActive(false);
        readyToPlay = false;
    }

    public void resetGame() {
        scorePanel.SetActive(true);
        gameStart = true;
        scoreText.text = "Score: 0";
        player.SetActive(true);
        finalScorePanel.SetActive(false);
        score = 0;
        soundController.playEffect("click");
    }

    private void deleteAsteroids() {
        foreach(GameObject ast in asteroidList) {
            Destroy(ast);
        }
        asteroidList.Clear();
    }

    public void startButton() {
        vampTransition = true;
        musicController.endLoop();
        loadingPanel.SetActive(true);
        soundController.playEffect("click");
    }

    private void waitTillVamp()
    {
        scorePanel.SetActive(true);
        gameStart = true;
        scoreText.text = "Score: 0";
        player.SetActive(true);
        finalScorePanel.SetActive(false);
        score = 0;
        startPanel.SetActive(false);
        canShoot = true;
        musicController.loopClip("blasteroids-main");
    }

    public void openGame() {
        if(!readyToPlay)
        {
            myCamera.gameObject.SetActive(true);
            myCanvas.gameObject.SetActive(true);
            phoneImage.SetActive(true);
            startPanel.SetActive(true);
            musicController.loopClip("blasteroids-vamp");
            readyToPlay = true;
        }
    }

    void mouseDragUi()
    {
        /** Houses the main mouse dragging logic. **/

        mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            detectUi();
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            dragElement();
        }
        else
        {
            isDragging = false;
        }

        prevMousePosition = mousePosition;
    }

    void detectUi()
    {
        /** Detect if the mouse has been clicked on a UI element, and begin dragging **/

        getUiElementsClicked();

        if (clickedElements.Count > 0)
        {
            if(clickedElements[0].gameObject.tag == "Phone")
            {
                isDragging = true;
                draggingElement = phoneImage;
            }
        }
    }

    void getUiElementsClicked()
    {
        /** Get all the UI elements clicked, using the current mouse position and raycasting. **/

        clickData.position = mousePosition;
        clickResults.Clear();
        raycaster.Raycast(clickData, clickResults);
        clickedElements.Clear();
        foreach (RaycastResult result in clickResults)
        {
            clickedElements.Add(result.gameObject);
        }
    }

    void dragElement()
    {
        /** Drag a UI element across the screen based on the mouse movement. **/

        RectTransform elementRect = draggingElement.GetComponent<RectTransform>();

        Vector2 drag_movement = mousePosition - prevMousePosition;
        float newY = elementRect.anchoredPosition.y + drag_movement.y;
        if(newY > -60)
        {
            newY = -60;
            openGame();
        } else if(newY < -420)
        {
            newY = -420;
        }
        if(newY < -60)
        {
            quitGameButton();
        }
        elementRect.anchoredPosition = new Vector2(elementRect.anchoredPosition.x, newY);
    }
}
