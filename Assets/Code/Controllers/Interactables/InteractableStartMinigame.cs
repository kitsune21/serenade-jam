using UnityEngine;
using System.Collections;

public class InteractableStartMinigame : Interactable
{

    private int currentText = 0;
    NpcMoveController m_MoveController;
    private bool canTalk = true;
    private bool isAtDesk = false;
    public Typer typingGame;

    void Awake()
    {
    }

    public override void OnInteract(Character character)
    {
        // are we using the player's desk?
        if (this.gameObject.CompareTag("Player Desk"))
        {
            if(typingGame.gamePaused)
            {
                character.Stats.SetUsingDesk(true);
                character.Movement.SetFrozen(true);
                character.Stats.SetHavingFun(isFun);
                canTalk = false;
                typingGame.startTypingGame();
                isAtDesk = true;
            } else
            {
                character.Stats.SetUsingDesk(false);
                character.Movement.SetFrozen(false);
                character.Stats.SetHavingFun(false);
                canTalk = true;
                typingGame.stopTypingGame();
                isAtDesk = false;
            }
        }
        // are we chatting up an NPC?
        if (this.gameObject.CompareTag("NPC"))
        {
            CharacterMoveModel m_MoveModel;
            m_MoveController = GetComponent<NpcMoveController>();
            canTalk = m_MoveController.GetCanTalk();

            if (canTalk)
            {
                m_MoveController.SetIsTalking(true);
                m_MoveModel = GetComponent<CharacterMoveModel>();
                Vector3 dir = character.Movement.GetFacingDirection();
                dir = -dir;
                m_MoveModel.FaceInteractor(dir);
            }
        }

        if (canTalk)
        {
            character.Movement.SetFrozen(true);
            character.Stats.SetHavingFun(isFun);
            // we have read all of the messages
            if (currentText >= text.Length)
            {
                character.Movement.SetFrozen(false);
                character.Stats.SetHavingFun(false);
                character.Stats.SetUsingDesk(false);
                currentText = 0;

                // release the NPC if we're talking to them
                if (m_MoveController)
                {
                    m_MoveController.SetIsTalking(false);
                }
            }
            // otherwise we still have messages left
            else
            {
                Debug.Log(text[currentText]);
                currentText++;
            }
        }
    }
}
