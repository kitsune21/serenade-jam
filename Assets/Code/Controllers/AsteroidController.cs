using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public BlasteroidsController bc;
    public float speed;

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
