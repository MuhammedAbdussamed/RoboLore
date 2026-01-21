using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    public void Enter(PlayerController controller){ controller.stateControl = PlayerController.PlayerState.Jump; }

    public void Exit(PlayerController controller) {}
    
    public void Update(PlayerController controller)
    {
        if (controller.playerProperties.onGround && Mathf.Abs(controller.moveDirection) >= 0.01f)        // Yerde ve yürüyorsa...
        {
            controller.ChangeState(controller.walkState);
        }
        else if (controller.playerProperties.onGround && Mathf.Abs(controller.moveDirection) <= 0.01f)   // Yerde ve yürümüyorsa...
        {
            controller.ChangeState(controller.idleState);
        }
    }
}
