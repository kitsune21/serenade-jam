using UnityEngine;
using System.Collections;

public class ActorMoveBaseControl : MoveBaseControl {


    protected CharacterMoveModel m_MovementModel;

    // prepare references before starting
    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMoveModel>();
        m_MovementModel.SetDirection(Vector2.down);
    }

    protected override void SetDirection(Vector2 direction)
    {
        if (m_MovementModel == null)
        {
            return;
        }
        m_MovementModel.SetDirection(direction);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
