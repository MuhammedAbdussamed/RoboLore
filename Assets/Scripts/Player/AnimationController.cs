using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Script Reference
    internal PlayerController controller;

    // Animators
    internal Animator animator;

    // State
    [SerializeField] internal PlayerController.PlayerState lastState;          // Animasyonlari deðiþtirmek için kullanacaðimiz karakter durum bilgisi

    void Start()
    {
        controller = PlayerController.Instance;
        animator = GetComponent<Animator>();

        lastState = controller.stateControl;                // Oyuncunun durum bilgisini aldik.
    }

    void Update()
    {
        SetAnimations();
    }

    void SetAnimations()
    {
        if (controller.stateControl != lastState)           // Deðiþkende ki durum, güncel durum ile bir deðilse
        {
            lastState = controller.stateControl;            // Durumu güncelle
            animator.SetInteger("State", (int)lastState);   // Güncel durumun enum indexini al ve animatorde ki integer'i ona ayarla.
        }
    }

}
