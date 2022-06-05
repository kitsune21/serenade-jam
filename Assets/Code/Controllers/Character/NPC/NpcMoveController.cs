using UnityEngine;
using System.Collections;

public class NpcMoveController : ActorMoveBaseControl
{
    private float timeToAct;
    private float timeToWait = .5f;
    private int currentNode = 0;
    private bool canTalk = false;
    private bool isAtDesk = false;
    private bool isTalking = false;
    public GameObject[] nodes;
    private bool goingToDestination = true;
    public int distance = 5;
    public bool isTheBoss = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateDirection()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (nodes[currentNode] == col.gameObject)
        {
            SetDirection(Vector2.zero);
            if (goingToDestination)
            {
                currentNode += 1;
                if (currentNode >= nodes.Length)
                {
                    currentNode = nodes.Length - 2;
                    goingToDestination = false;
                    canTalk = true;
                    timeToWait = 2f;
                }
            } else
            {
                currentNode -= 1;
                if (currentNode < 0)
                {
                    currentNode = 1;
                    goingToDestination = true;
                    canTalk = true;
                    isAtDesk = true;
                    timeToWait = 2f;
                }
            }
        }
    }

    public bool GetCanTalk()
    {
        return canTalk;
    }

    public bool IsAtDesk()
    {
        return isAtDesk;
    }

    public void SetIsTalking(bool newIsTalking)
    {
        isTalking = newIsTalking;
        if (!isTalking)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Walk()
    {
        if (timeToWait > .5f)
        {
            canTalk = false;
            isAtDesk = false;
            timeToWait = .5f;
        }
        Vector2 newDirection = Vector2.zero;
        Vector2 npc = transform.position;
        Vector2 nextNode = nodes[currentNode].transform.position;
        newDirection = (nextNode - npc).normalized;
        timeToAct = distance * .1f;
        SetDirection(newDirection);
        yield return new WaitForSeconds(timeToAct);
        if (currentNode >= nodes.Length)
        {
            StartCoroutine(Wait());
        }
        else
        {
            StartCoroutine(Wait());
        }
    }

    public void SetCurrentNode(int newNode)
    {
        currentNode = newNode;
    }

    public int GetCurrentNode()
    {
        return currentNode;
    }

    public IEnumerator Wait()
    {
        SetDirection(Vector2.zero);
        float waitMax = timeToWait * 1.5f;
        timeToAct = Random.Range(timeToWait, waitMax);
        yield return new WaitForSeconds(timeToAct);
        if (!isTalking)
        {
            StartCoroutine(Walk());
        }
    }
}
