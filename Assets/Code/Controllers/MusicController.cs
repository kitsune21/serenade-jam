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

    public List<ClipScript> songs = new List<ClipScript>();
    private ClipScript currentClip;

    private string mainClip = "day-5";

    public bool isFading = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        bgAudioPlayer = gameObject.AddComponent<AudioSource>();
        fadeInClip("main-menu");
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
        if(!audioPlayer.isPlaying && (currentClip.clipName != "blasteroids-vamp" || currentClip.clipName != "blasteroids-main"))
        {
            fadeInClip(mainClip);
        }
        if(!bgAudioPlayer.isPlaying && currentClip.clipName == "day-5")
        {
            bgAudioPlayer.clip = stringToClip("water-cooler").clip;
            bgAudioPlayer.volume = 0;
            bgAudioPlayer.Play();
            bgAudioPlayer.loop = true;
        } else if(currentClip.clipName != "day-5")
        {
            bgAudioPlayer.Stop();
        }
    }

    private ClipScript stringToClip(string clip)
    {
        foreach(ClipScript song in songs)
        {
            if(song.clipName == clip)
            {
                return song;
            }
        }

        return null;
    }

    public void loopClip(string clip)
    {
        currentClip = stringToClip(clip);
        audioPlayer.clip = currentClip.clip;
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
        if(clip == currentClip.clipName)
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

    public void turnOffBg()
    {
        bgFadeIn = false;
        bgCurrentVolume = 0;
        bgAudioPlayer.volume = bgCurrentVolume;
    }
}
