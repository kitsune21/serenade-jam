using UnityEngine;
using System.Collections;

public class MoveBaseControl : MonoBehaviour
{
    // prepare references before starting
    void Awake()
    {
    }

    virtual protected void SetDirection(Vector2 direction)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FreezeCharacter(GameObject objectToFreeze)
    {
        CharacterMoveModel m_moveModel = objectToFreeze.GetComponent<CharacterMoveModel>();
        m_moveModel.SetFrozen(true);
        yield return new WaitForSeconds(.25f);
        m_moveModel.SetFrozen(false);
    }
}
