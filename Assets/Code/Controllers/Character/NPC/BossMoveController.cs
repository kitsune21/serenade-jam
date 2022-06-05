using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveController : NpcMoveController
{
    Rigidbody2D bossRb2D;
    GameObject player;
    Character character;
    GameObject playerDesk;
    SpriteRenderer alertSprite;
    Vector2 playerDir;
    Vector2 deskDir;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        player = GameObject.FindWithTag("Player");
        character = player.GetComponent<Character>();
        playerDesk = GameObject.FindWithTag("Player Desk");
        bossRb2D = GetComponent<Rigidbody2D>();

        foreach (Transform child in transform)
        {
            if (child.name == "alert")
            {
                alertSprite = child.GetComponent<SpriteRenderer>();
            }
        }

        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // cast a ray towards the player and the desk
        playerDir = (player.transform.position - transform.position).normalized;
        deskDir = (playerDesk.transform.position - transform.position).normalized;
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, playerDir);
        RaycastHit2D deskHit = Physics2D.Raycast(transform.position, deskDir);
        Debug.DrawRay(transform.position, playerDir * 100f);
        Debug.DrawRay(transform.position, deskDir * 100f);
        bool usingDesk = character.Stats.GetUsingDesk();
        float deskDist = 5000f;
        float playerDist = 5000f;
        bool hasPlayer = false;
        bool hasDesk = false;
        if (!usingDesk)
        {
            if ((playerHit.collider != null
                    && playerHit.collider.tag == "Player"))
            {
                playerDist = playerHit.distance;
                hasPlayer = true;
            }
            if ((deskHit.collider != null
                    && deskHit.collider.tag == "Player Desk"))
            {
                deskDist = deskHit.distance;
                hasDesk = true;
            }
            // did we see the player?
            if (hasPlayer || hasDesk)
            {
                if (deskDist > playerDist)
                {
                    SetDirection(playerDir);
                }
                else
                {
                    SetDirection(playerDir);
                }
                alertSprite.enabled = true;
            }
            else
            {
                if (alertSprite.enabled)
                {
                    alertSprite.enabled = false;
                    SetCurrentNode(0);
                }
            }
        }
        else
        {
            if (alertSprite.enabled)
            {
                alertSprite.enabled = false;
                SetCurrentNode(0);
            }
        }
    }
}
