using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Character))]
public class CharacterInteractionModel : MonoBehaviour
{

    private Character m_Character;
    private CharacterMoveModel m_MoveModel;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_MoveModel = GetComponent<CharacterMoveModel>();
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract()
    {
        Interactable usableInteractable = FindUsableInteractable();
        if (usableInteractable == null)
        {
            return;
        }

        usableInteractable.OnInteract(m_Character);
        if (usableInteractable.CompareTag("NPC"))
        {
            usableInteractable.FaceCharacter(m_Character);
        }
    }

    Interactable FindUsableInteractable()
    {
        Interactable closestInteractable = null;
        float angleToClosestInteractable = Mathf.Infinity;

        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, 0.35f);
        for (int i = 0; i < closeColliders.Length; ++i)
        {
            Interactable colliderInteractable = closeColliders[i].GetComponent<Interactable>();
            if (colliderInteractable == null)
            {
                continue;
            }

            Vector3 directionToInteractable = closeColliders[i].transform.position - transform.position;
            float angleToInteractable = Vector3.Angle(m_MoveModel.GetFacingDirection(), directionToInteractable);
            if (angleToInteractable < 90)
            {
                if (angleToInteractable < angleToClosestInteractable)
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractable = angleToInteractable;
                }

            }
        }
        return closestInteractable;
    }
}
