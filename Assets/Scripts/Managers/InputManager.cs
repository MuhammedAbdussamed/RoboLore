using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] internal InputActionAsset inputs;

    // Action Maps
    internal InputActionMap movementMap;
    internal InputActionMap gameMap;
    internal InputActionMap attackMap;
    internal InputActionMap socialMap;

    // Actions
    internal InputAction moveAction;
    internal InputAction jumpAction;
    internal InputAction pauseAction;
    internal InputAction fireAction;
    internal InputAction danceAction;
    internal InputAction dashAction;

    // Events
    public static Action<bool> OnJumpInput;
    public static Action OnPauseInput;
    public static Action OnAttackInput;
    public static Action OnDashInput;

    // Variables
    public static float moveDir;
    public static bool isJumping;

    void Awake()
    {
        AssignInputs();
    }

    void OnEnable()
    {
        moveAction.performed += Move;               // moveAction girdisi alýndýðýnda "Move" fonksiyonu çaliþacak çünkü abone olduk
        moveAction.canceled += StopMove;

        dashAction.performed += Dash;

        fireAction.performed += Fire;

        jumpAction.performed += Jump;
        jumpAction.canceled += Jump;

        pauseAction.performed += Pause;
    }

    void OnDisable()
    {
        moveAction.performed -= Move;               // Obje deaktif olduðunda abonelikten çikiyoruz.
        moveAction.canceled -= StopMove;            //

        pauseAction.performed -= Pause;

    }

    #region Functions

    void Move(InputAction.CallbackContext context)              //
    {
        moveDir = context.ReadValue<float>();                   //  Tuþa basýldýðýnda deðer ata
    }
                                                                //
    void StopMove(InputAction.CallbackContext context)          //
    {
        moveDir = 0;                                            //  Tuþu býraktýðýnda deðeri sýfýrla
    }

    /*--------------------*/

    void Fire(InputAction.CallbackContext context)
    {
        OnAttackInput?.Invoke();

    }

    /*-------------------*/

    void Jump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;                          // ReadValue<>() yapmadik çünkü button inputlarinda bu daha kullaniþli.
        OnJumpInput?.Invoke(isJumping);
    }

    /*--------------------*/
    void Pause(InputAction.CallbackContext context)
    {
        OnPauseInput?.Invoke();
    }

    /*-------------------*/

    void Dash(InputAction.CallbackContext context)
    {
        OnDashInput?.Invoke();
    }

    /*-------------------*/

    void AssignInputs()
    {
        movementMap = inputs.FindActionMap("Movement");
        gameMap = inputs.FindActionMap("Game");
        attackMap = inputs.FindActionMap("Attack");

        moveAction = movementMap.FindAction("Move");
        dashAction = movementMap.FindAction("Dash");
        jumpAction = movementMap.FindAction("Jump");
        pauseAction = gameMap.FindAction("Pause");
        fireAction = attackMap.FindAction("Fire");

        inputs.Enable();
    }

    #endregion
}
