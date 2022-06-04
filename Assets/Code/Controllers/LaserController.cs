using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private float laserSpeed = 0.002f;
    private Rigidbody2D rb;
    private float laserTime = 1f;
    public GameObject bc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * laserSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(laserTime > 0) {
            laserTime -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "BL-Asteroid") {
            bc.GetComponent<BlasteroidsController>().addScore();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
