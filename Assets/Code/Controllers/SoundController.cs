using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip laserShot;
    public AudioClip explosion;
    public AudioClip select;

    private List<AudioSource> audioPlayers;

    public float volume;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayers = new List<AudioSource>();
        volume = 1f;
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
}
