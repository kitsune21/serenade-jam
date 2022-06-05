using UnityEngine;
using System.Collections;

public class CharacterMoveModel : MonoBehaviour
{
    public float Speed;

    private Vector3 m_MovementDirection;
    private Vector3 m_FacingDirection;
    private Rigidbody2D m_Body;
    private GameObject thisGameObject;
    public bool m_IsFrozen = false;
    // Use this for initialization
    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (m_IsFrozen)
        {
            m_Body.velocity = Vector3.zero;
            return;
        }
        if (m_MovementDirection != Vector3.zero)
        {
            m_MovementDirection.Normalize();
        }
        else
        {
            SetFrozen(false);
            m_FacingDirection.Normalize();
        }
        m_Body.velocity = m_MovementDirection * Speed;
    }

    public void SetFrozen(bool isFrozen)
    {
        m_IsFrozen = isFrozen;
    }

    public void SetDirection(Vector3 direction)
    {
        if (m_IsFrozen)
        {
            return;
        }

        m_MovementDirection = new Vector3(direction.x, direction.y, 0);
        m_FacingDirection = m_MovementDirection;
    }

    public void SetFacingDirection(Vector2 direction)
    {
        if (m_IsFrozen)
        {
            return;
        }
        m_FacingDirection = new Vector3(direction.x, direction.y, 0);
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public Vector3 GetCurrentPosition()
    {
        return new Vector3(m_Body.position.x, m_Body.position.y);
    }

    public void FaceInteractor(Vector3 direction)
    {
        float oldSpeed = Speed;
        SetSpeed(0);
        SetDirection(direction);
        StartCoroutine(RestoreActorMovement(direction, oldSpeed));
    }

    IEnumerator RestoreActorMovement(Vector3 direction, float oldSpeed)
    {
        SetFrozen(true);
        yield return null;
        SetFrozen(false);
        SetSpeed(oldSpeed);
        SetDirection(Vector3.zero);
    }

    IEnumerator ResetDirection(Vector3 direction)
    {
        SetDirection(direction);
        yield return new WaitForSeconds(0.001f);
    }

    public bool IsMoving()
    {
        if (m_IsFrozen)
        {
            return false;
        }
        return m_MovementDirection != Vector3.zero;
    }

    public bool IsFrozen()
    {
        return m_IsFrozen;
    }

    public void ToggleCursorControl()
    {
        Cursor.visible = !Cursor.visible;
    }

    IEnumerator TimedUnFreeze(float timeFrozen)
    {
        yield return new WaitForSeconds(timeFrozen);
        SetFrozen(false);
    }
}
