using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation_Controller : MonoBehaviour
{
    // Animation Variables
    internal Animator animator;
    internal Enemy_Bot_Base.BotAnimationState animationState;
    internal Enemy_Bot_Base botController;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        botController = GetComponent<Enemy_Bot_Base>();
    }

    void Update()
    {
        SetAnimations();
    }

    #region Functions

    void SetAnimations()
    {
        if (animationState == botController.botAnimationState) { return; }

        animationState = botController.botAnimationState;
        animator.SetInteger("State", (int)animationState);
    }

    #endregion
}
