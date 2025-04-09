using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
        MoveAction.Enable();
    }

    void FixedUpdate ()
    {
        // reads the play's input
        var pos = MoveAction.ReadValue<Vector2>();
        
        float horizontal = pos.x;
        float vertical = pos.y;
        // sets movement vector
        m_Movement.Set(horizontal, 0f, vertical);
        // then normalizes
        m_Movement.Normalize ();

        // checks if the player is pressing any movement keys
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }
        // rotes the player toward their movement direction
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        // turns directional vector into a rotational ratio
        Quaternion endRotation = Quaternion.LookRotation(desiredForward);

        // Quaternion.Lerp(a, b, t) -> a: start unit value, b: end unit value, t: interpolation ratio
        m_Rotation = Quaternion.Lerp(transform.rotation, endRotation, turnSpeed * Time.deltaTime);

        // m_Rotation = Quaternion.LookRotation (desiredForward);

    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}