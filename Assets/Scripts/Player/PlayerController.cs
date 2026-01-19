using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Scripts
    [SerializeField] internal PlayerProperties playerProperties;

    // Components
    internal Rigidbody rb;

    // Variables
    internal float moveDirection;

    // State
    internal PlayerState currentState;
    internal IState state;
    internal IState walkState;
    internal IState idleState;

    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        rb = GetComponent<Rigidbody>();

        currentState = PlayerState.Idle;

        walkState = new WalkState();
        idleState = new IdleState();

        state = idleState;
    }

    void OnEnable()
    {
        InputManager.OnJumpInput += JumpPlayer;
    }

    void OnDisable()
    {
        InputManager.OnJumpInput -= JumpPlayer;
    }

    void Update()
    {
        UpdateState();
        state.Update(this);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    #region Functions

    void MovePlayer()
    {
        moveDirection = InputManager.moveDir;            // Ýnput managerda ki yürüme girdisini al.

        rb.velocity = new Vector3(
            moveDirection * playerProperties.Speed,      // 
            rb.velocity.y,                               // Sadece z eksenine kuvvet uyguluyoruz.
            rb.velocity.z                                // 
            );
    }

    /*-----------------------------------------*/

    void JumpPlayer(bool isJumping)
    {
        if (InputManager.isJumping)
        {
            Debug.Log("Zipliyor");
            rb.velocity = new Vector3(rb.velocity.x, playerProperties.JumpPower, rb.velocity.z);   // Mevcut x ve z deðerini koru ki karakter havada sað sol yapabilsin.
            // rb.AddForce(Vector3.up * playerProperties.JumpPower, ForceMode.Impulse);            // Burada mevcut deðerler korunmuyor. Ýleride bug çýkarabilir.
        }
    }

    /*-----------------------------------------*/

    public void UpdateState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                ChangeState(idleState);
                break;

            case PlayerState.Walk:
                ChangeState(walkState);
                break;
        }
    }

    void ChangeState(IState newState)
    {
        state.Exit(this);
        state = newState;
        state.Enter(this);
    }

    /*-----------------------------------------*/

    #endregion

    #region Enums

    public enum PlayerState
    {
        Idle,
        Walk,
        Jump
    }

    #endregion
}
