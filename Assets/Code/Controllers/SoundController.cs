using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip laserShot;
    public AudioClip explosion;
    public AudioClip select;
    public AudioClip click;
    public AudioClip footSteps;

    private List<AudioSource> audioPlayers;
    public List<AudioClip> randomBG;

    public float volume;

    private float randomSoundTimer;
    private float randomSoundTimerMax = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayers = new List<AudioSource>();
        volume = 1f;
        randomSoundTimer = randomSoundTimerMax;
    }

    void Update()
    {
        if(randomSoundTimer > 0)
        {
            randomSoundTimer -= Time.deltaTime;
        } else
        {
            randomSoundTimer = randomSoundTimerMax;
            playRandomSound();
        }
    }

    void FixedUpdate()
    {
        if(audioPlayers.Count > 0)
        {
            List<AudioSource> tempList = new List<AudioSource>();
            foreach (AudioSource player in audioPlayers)
            {
                if (!player.isPlaying)
                {
                    DestroyImmediate(player);
                } else
                {
                    tempList.Add(player);
                }
            }
            audioPlayers = tempList;
        }
    }

    private AudioClip stringToClip(string effect)
    {
        if(effect == "laser")
        {
            return laserShot;
        }
        if(effect == "explosion")
        {
            return explosion;
        }
        if(effect == "select")
        {
            return select;
        }
        if(effect == "click")
        {
            return click;
        }

        return null;
    }

    private AudioSource availableSource()
    {
        foreach(AudioSource player in audioPlayers)
        {
            if(!player.isPlaying)
            {
                return player;
            }
        }

        AudioSource temp =  gameObject.AddComponent<AudioSource>();
        audioPlayers.Add(temp);

        return temp;
    }

    public void playEffect(string effect)
    {
        AudioSource availablePlayer = availableSource();
        availablePlayer.clip = stringToClip(effect);
        availablePlayer.volume = volume;
        availablePlayer.Play();
    }

    public void playFootSteps()
    {
        AudioSource availablePlayer = availableSource();
        availablePlayer.clip = footSteps;
        availablePlayer.volume = volume;
        availablePlayer.Play();
        availablePlayer.loop = true;
    }

    private void playRandomSound()
    {
        if(Random.Range(0, 4) == 1)
        {
            int randomIndex = Random.Range(0, randomBG.Count);
            AudioSource availablePlayer = availableSource();
            availablePlayer.clip = randomBG[randomIndex];
            availablePlayer.volume = volume;
            availablePlayer.Play();
        }
    }
}
