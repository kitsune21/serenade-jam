using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private float musicVolume;
    private float soundVolume;

    public Slider musicSlider;
    public Text musicSliderText;
    public Slider soundSlider;
    public Text soundSliderText;
    public Button startButton;
    public Button exitButton;
    private int selectedUpDown;
    private int selectLeftRight;

    public MusicController mc;
    public SoundController sc;

    public GameObject menuPanels;
    public GameObject clockPanel;
    public GameObject phone;
    public GameObject gameOverPanel;
    public Text finalScore;
    public GameObject energyPanel;


    private bool waitForSound = false;
    private float soundVolumeTimer;
    private float soundVolumeTimerMax = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        selectedUpDown = 0;
        selectLeftRight = 0;
        musicVolume = 0.5f;
        soundVolume = 0.5f;
        soundVolumeTimer = soundVolumeTimerMax;
        updateMusicVolume();
        updateSoundVolume();
    }

    // Update is called once per frame
    void Update()
    {
        menuSelection();
        if(Input.GetKeyDown(KeyCode.Escape)) {
            exit();
        }
        if(waitForSound)
        {
            if(soundVolumeTimer > 0)
            {
                soundVolumeTimer -= Time.deltaTime;
            } else
            {
                waitForSound = false;
                soundVolumeTimer = soundVolumeTimerMax;
            }
        }
    }

    public void exit() {
        Application.Quit();
    }

    public void updateMusicVolume() {
        musicVolume = musicSlider.value;
        int musicVolumeInt = (int)(musicVolume * 10);
        musicSliderText.text = musicVolumeInt.ToString();
        mc.updateVolume(musicVolume);
    }

    public void updateSoundVolume() {
        soundVolume = soundSlider.value;
        int soundVolumeInt = (int)(soundVolume * 10);
        soundSliderText.text = soundVolumeInt.ToString();
        sc.volume = soundVolume;
        if(!waitForSound)
        {
            sc.playEffect("click");
            waitForSound = true;
        }
    }

    private void menuSelection() {
        if(Input.GetKeyDown(KeyCode.W)){
            selectedUpDown = selectedUpDown == 1 ? 0 : 1;
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            selectedUpDown = selectedUpDown == 0 ? 1 : 0;
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            selectLeftRight = selectLeftRight == 0 ? 1 : 0;
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            selectLeftRight = selectLeftRight == 1 ? 0 : 1;
        }
        if(selectLeftRight == 0) {
            if(selectedUpDown == 0) {
                startButton.Select();
            } else {
                exitButton.Select();
            }
        } else {
            if(selectedUpDown == 0) {
                musicSlider.Select();
            } else {
                soundSlider.Select();
            }
        }
    }

    public void startGame()
    {
        menuPanels.SetActive(false);
        clockPanel.SetActive(true);
        phone.SetActive(true);
        mc.fadeInClip("day-5");
        energyPanel.SetActive(true);
    }

    public void resetWholeGame()
    {
        SceneManager.LoadScene(0);
    }

    public void gameOver(int score)
    {
        gameOverPanel.SetActive(true);
        finalScore.text = "Your Score: " + score.ToString();
    }
}
