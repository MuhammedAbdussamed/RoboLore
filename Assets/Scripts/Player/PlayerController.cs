using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Script Reference
    internal PlayerProperties playerProperties;

    // Character States
    internal PlayerState stateControl;      // Animasyon deðiþtirmek için kontrol ettiðimiz deðiþken.
    internal IState currentState;           // FSM sisteminde ki güncel durum
    internal IState walkState;              //
    internal IState idleState;              //  Karakterin içerisinde bulunabileceði farkli durumlar.
    internal IState jumpState;              //

    // Components
    internal Rigidbody rb;

    // Movement Variables
    internal float moveDirection;

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

        stateControl = PlayerState.Idle;

        // States
        walkState = new WalkState();
        idleState = new IdleState();
        jumpState = new JumpState();

        currentState = idleState;

        // Assign
        playerProperties = GetComponent<PlayerProperties>();
    }

    void OnEnable()
    {
        InputManager.OnJumpInput += JumpPlayer;
        InputManager.OnDashInput += Dash;
    }

    void OnDisable()
    {
        InputManager.OnJumpInput -= JumpPlayer;
        InputManager.OnDashInput -= Dash;
    }

    void Update()
    {
        currentState.Update(this);
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
            rb.velocity.y,                               // Sadece x eksenine kuvvet uyguluyoruz.
            rb.velocity.z                                // 
        );

        Turn();
    }

    /*-----------------------------------------*/

    void Turn()
    {
        if (moveDirection == 0) { return; }                 // Yürüme girdisi yoksa döngüyü kýr.

        else if (moveDirection < 0)
        {
            playerProperties.faceRight = false;
        }
        else
        {
            playerProperties.faceRight = true;
        }

        float targetY = playerProperties.faceRight ? 90f : -90f;        // faceRight true ise robotun bakmasi gereken yer Y 90 derece. false ise -90 derece.

        Vector3 currentEuler = transform.eulerAngles;                   // Güncel açýlarý aldýk.

        float newY = Mathf.MoveTowardsAngle(            // Yeni Y derecesi her frame güncellenir.
            currentEuler.y,                             // Mevcut y konumundan , hedef y konumuna doðru akar.
            targetY,                                    
            3000f * Time.deltaTime                       // Her saniye 3000 derece döner.
        );

        transform.rotation = Quaternion.Euler(          // Y derecesi güncellendiði her frame, player'ýn mevcut rotasyonuna uygulanir.
            currentEuler.x,
            newY,                                       //
            currentEuler.z                              // Bir nevi önceden deðiþtirdiðimiz datayý burada karaktere uyguluyoruz.
        );                                              // 
    }

    /*-----------------------------------------*/

    void JumpPlayer(bool isJumping)
    {
        if (InputManager.isJumping && playerProperties.onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, playerProperties.JumpPower, rb.velocity.z);   // Mevcut x ve z deðerini koru ki karakter havada sað sol yapabilsin.
            // rb.AddForce(Vector3.up * playerProperties.JumpPower, ForceMode.Impulse);            // Burada mevcut deðerler korunmuyor. Ýleride bug çýkarabilir.
        }
    }

    /*-----------------------------------------*/

    public void ChangeState(IState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    /*-----------------------------------------*/

    void Dash()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * playerProperties.DashSpeed, ForceMode.Impulse);
    }

    #endregion

    #region Enums

    public enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        Dash
    }

    #endregion
}
