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
    private const float MIN_ANG_VEL_DEG = 10.0f;
    private const float MAX_ANG_VEL_DEG = 1000.0f;

    private float _alpha = 0.5f; // normalized to [0.0, 1.0] scale

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
        // Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        // calculate the linear interpolation
        float interpAngVelDeg = 
            (1.0f - _alpha) * MIN_ANG_VEL_DEG +
            _alpha * MAX_ANG_VEL_DEG;

        // get the point where you want to turn to 
        Quaternion targetRotation = Quaternion.LookRotation(m_Movement);

        // apply the linear interplotaion in getting to that point!
        m_Rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, interpAngVelDeg * Time.deltaTime);

    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}