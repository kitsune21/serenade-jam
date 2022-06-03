using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    // Start is called before the first frame update
    void Start()
    {
        selectedUpDown = 0;
        selectLeftRight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        menuSelection();
    }

    public void exit() {
        Application.Quit();
    }

    public void updateMusicVolume() {
        musicVolume = musicSlider.value;
        int musicVolumeInt = (int)(musicVolume * 10);
        musicSliderText.text = musicVolumeInt.ToString();
    }

    public void updateSoundVolume() {
        soundVolume = soundSlider.value;
        int soundVolumeInt = (int)(soundVolume * 10);
        soundSliderText.text = soundVolumeInt.ToString();
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
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(selectLeftRight == 0 && selectedUpDown == 0) {
                Debug.Log("start the game");
            }
            if(selectLeftRight == 0 && selectedUpDown == 1) {
                exit();
            }
        }
    }
}
