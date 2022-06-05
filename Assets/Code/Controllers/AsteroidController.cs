using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public BlasteroidsController bc;
    public float speed;

    public void selectSprite(Sprite asteroid1, Sprite asteroid2)
    {
        if(Random.Range(0,2) == 1)
        {
            GetComponent<SpriteRenderer>().sprite = asteroid1;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = asteroid2;
        }
    }

    public void rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    
    void Update() {
        badCollisionChecker();
    }

    private void badCollisionChecker() {
        if(transform.localPosition.x <= 1 && transform.localPosition.y <= 1) {
            if(transform.localPosition.x <= 1 && transform.localPosition.y >= -1) {
                if(transform.localPosition.x >= -1 && transform.localPosition.y >= -1) {
                    if(transform.localPosition.x >= -1 && transform.localPosition.y <= 1) {
                        bc.playerDied();
                    }
                }
            }
        }
    }
}
