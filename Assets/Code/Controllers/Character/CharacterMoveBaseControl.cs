using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMoveBaseControl : MonoBehaviour
{

    protected CharacterMoveModel m_MovementModel;
    // private CharacterInteractionModel m_InteractionModel;
 
    protected Animator m_animator;
    // prepare references before starting
    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMoveModel>();
        // m_InteractionModel = GetComponent<CharacterInteractionModel>();
        m_MovementModel.SetDirection(Vector2.down);
    }

    protected void SetDirection(Vector2 direction)
    {
        if (m_MovementModel == null)
        {
            return;
        }
        m_MovementModel.SetDirection(direction);
    }

    protected void SetFacingDirection(Vector2 direction)
    {
        if (m_MovementModel == null)
        {
            return;
        }
        m_MovementModel.SetFacingDirection(direction);
    }

    /* protected void OnActionPressed()
    {
        if (m_InteractionModel == null)
        {
            return;
        }
        m_InteractionModel.OnInteract();
        // if close enough and facing the target
    } */

    // Update is called once per frame
    void Update()
    {

    }
}
