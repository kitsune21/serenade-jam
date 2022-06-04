using UnityEngine;
using System.Collections;

public class InteractableStartMinigame : Interactable
{

    private int currentText = 0;

    void Awake()
    {
    }

    public override void OnInteract(Character character)
    {
        character.Movement.SetFrozen(true);
        // we have read all of the messages
        if (currentText >= text.Length)
        {
            character.Movement.SetFrozen(false);
            currentText = 0;
        }
        // otherwise we still have messages left
        else
        {
            Debug.Log(text[currentText]);
            currentText++;
        }
    }
}
