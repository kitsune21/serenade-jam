using UnityEngine;
using System.Collections;

public class NpcMoveController : ActorMoveBaseControl
{
    private float timeToAct;
    private int dir = 1;
    public enum Direction
    {
        Horizontal,
        Vertical
    };
    public Direction direction;
    public int distance = 5;

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
        if (dir == 1)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        if (direction.ToString() == "Horizontal")
        {
            newDirection.y = 0;
            newDirection.x = dir;
        }
        else
        {
            newDirection.y = dir;
            newDirection.x = 0;
        }
        timeToAct = distance * .1f;
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
