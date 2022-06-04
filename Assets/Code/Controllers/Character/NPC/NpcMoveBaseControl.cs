using UnityEngine;
using System.Collections;

public class NpcMoveBaseControl : ActorMoveBaseControl
{

    // prepare references before starting
    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMoveModel>();
        m_MovementModel.SetDirection(Vector2.down);
    }
}
