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

    // Actions
    internal InputAction moveAction;
    internal InputAction jumpAction;

    // Events
    public static Action<bool> OnJumpInput;

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

        jumpAction.performed += Jump;
        jumpAction.canceled += Jump;
    }

    void OnDisable()
    {
        moveAction.performed -= Move;               // Obje deaktif olduðunda abonelikten çikiyoruz.
        moveAction.canceled -= StopMove;            //

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

    void Jump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;                          // ReadValue<>() yapmadik çünkü button inputlarinda bu daha kullaniþli.
        OnJumpInput?.Invoke(isJumping);
    }

    /*--------------------*/

    void AssignInputs()
    {
        movementMap = inputs.FindActionMap("Movement");
        moveAction = movementMap.FindAction("Move");
        jumpAction = movementMap.FindAction("Jump");

        inputs.Enable();
    }

    #endregion
}
