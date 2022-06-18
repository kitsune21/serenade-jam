using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<ClipScript> fxs = new List<ClipScript>();
    public List<ClipScript> randomBG = new List<ClipScript>();

    private List<AudioSource> audioPlayers;

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

    private ClipScript stringToClip(string effect)
    {
        foreach (ClipScript fx in fxs)
        {
            if (fx.clipName == effect)
            {
                return fx;
            }
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
        availablePlayer.clip = stringToClip(effect).clip;
        availablePlayer.volume = volume;
        availablePlayer.Play();
    }

    public void playFootSteps()
    {
        AudioSource availablePlayer = availableSource();
        availablePlayer.clip = stringToClip("footsteps").clip;
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
            availablePlayer.clip = randomBG[randomIndex].clip;
            availablePlayer.volume = volume;
            availablePlayer.Play();
        }
    }
}
