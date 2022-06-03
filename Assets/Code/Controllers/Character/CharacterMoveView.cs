using UnityEngine;
using System.Collections;

public class CharacterMoveView : MonoBehaviour
{
    public Animator Animator;

    private CharacterMoveModel m_MovementModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMoveModel>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    void UpdateDirection()
    {
        Vector3 facingDirection = m_MovementModel.GetFacingDirection();
        Animator.SetFloat("DirectionX", facingDirection.x);
        Animator.SetFloat("DirectionY", facingDirection.y);
        Animator.SetBool("IsMoving", m_MovementModel.IsMoving());
    }
}
