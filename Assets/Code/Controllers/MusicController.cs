using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioPlayer;
    private AudioSource bgAudioPlayer;
    private float volume;
    private float currentVolume;
    private float bgVolume;
    private float bgCurrentVolume;
    private bool bgFadeIn = false;
    
    //songs
    public AudioClip blasteroidsMain;
    public AudioClip blasteroidsVamp;
    public AudioClip day5Main;
    public AudioClip mainMenu;
    public AudioClip waterCooler;

    private string clipString;
    private string mainClip = "day-5";

    public bool isFading = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        bgAudioPlayer = gameObject.AddComponent<AudioSource>();
        fadeInClip("menu");
    }

    void Update()
    {
        if(isFading)
        {
            if(currentVolume < volume)
            {
                currentVolume += 0.001f;
                audioPlayer.volume = currentVolume;
            } else
            {
                isFading = false;
                currentVolume = volume;
            }
        }
        if (bgFadeIn)
        {
            if (bgCurrentVolume < bgVolume)
            {
                bgCurrentVolume += 0.001f;
                bgAudioPlayer.volume = bgCurrentVolume;
            }
            else
            {
                bgFadeIn = false;
                bgCurrentVolume = bgVolume;
            }
        }
    }

    void FixedUpdate()
    {
        if(!audioPlayer.isPlaying)
        {
            fadeInClip(mainClip);
        }
        if(!bgAudioPlayer.isPlaying && clipString == "day-5")
        {
            bgAudioPlayer.clip = stringToClip("water-cooler");
            bgAudioPlayer.volume = 0;
            bgAudioPlayer.Play();
            bgAudioPlayer.loop = true;
        }
    }

    private AudioClip stringToClip(string clip)
    {
        if (clip == "blasteroids-main")
        {
            return blasteroidsMain;
        }
        if (clip == "blasteroids-vamp")
        {
            return blasteroidsVamp;
        }
        if(clip == "day-5")
        {
            return day5Main;
        }
        if(clip == "menu")
        {
            return mainMenu;
        }
        if(clip == "water-cooler")
        {
            return waterCooler;
        }

        return null;
    }

    public void loopClip(string clip)
    {
        clipString = clip;
        audioPlayer.clip = stringToClip(clip);
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    public void stopClip()
    {
        audioPlayer.Stop();
    }

    public void endLoop()
    {
        audioPlayer.loop = false;
    }

    public bool getClipStatus()
    {
        if(audioPlayer.isPlaying)
        {
            return false;
        } else
        {
            return true;
        }
    }

    public void updateVolume(float newVolume)
    {
        volume = newVolume;
        currentVolume = newVolume;
        bgVolume = newVolume;
        audioPlayer.volume = volume;
    }

    public bool isClipPlaying(string clip)
    {
        if(clip == clipString)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void fadeInClip(string clip)
    {
        loopClip(clip);
        isFading = true;
        currentVolume = 0f;
    }

    public void fadeInBg()
    {
        bgFadeIn = true;
    }
}
