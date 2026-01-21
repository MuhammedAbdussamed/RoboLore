using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    public void Enter(PlayerController controller) { controller.stateControl = PlayerController.PlayerState.Walk; }

    public void Exit(PlayerController controller) {}

    public void Update(PlayerController controller) 
    {
        if (controller.playerProperties.onGround && Mathf.Abs(controller.moveDirection) <= 0.01f)   // Mathf.Abs mutlak deðer alýr.
        {
            controller.ChangeState(controller.idleState);
        }
        else if (!controller.playerProperties.onGround)         // Yerde deðilse...
        {
            controller.ChangeState(controller.jumpState);
        }
    }
}
