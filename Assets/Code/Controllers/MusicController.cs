using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioPlayer;
    private float volume;
    
    //songs
    public AudioClip blasteroidsMain;
    public AudioClip blasteroidsVamp;
    public AudioClip day5Main;

    private List<string> queue;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        queue = new List<string>();
        loopClip("day-5");
    }

    // Update is called once per frame
    void Update()
    {
        
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

        return null;
    }

    public void loopClip(string clip)
    {
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

    public void addToQueue(string clip)
    {
        queue.Add(clip);
    }

    public void updateVolume(float newVolume)
    {
        volume = newVolume;
        audioPlayer.volume = volume;
    }
}
