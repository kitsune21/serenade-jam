using UnityEngine;
using System.Collections;

public class ActorMoveController : ActorMoveBaseControl {

    private float timeToAct;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
    }

    IEnumerator Walk()
    {
        Vector2 newDirection = Vector2.zero;
        // inclusive min but exclusive max, so use max of 2
        newDirection.y = Random.Range(-1, 2);
        newDirection.x = Random.Range(-1, 2);
        timeToAct = Random.Range(.2f, .3f);
        SetDirection(newDirection);
        yield return new WaitForSeconds(timeToAct);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        SetDirection(Vector2.zero);
        timeToAct = Random.Range(.5f, .75f);
        yield return new WaitForSeconds(timeToAct);
        StartCoroutine(Walk());
    }
}
