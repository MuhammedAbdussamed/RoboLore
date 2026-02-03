using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : Enemy_IState
{
    public void Enter(Enemy_Bot_Base botController)
    {
        botController.botAnimationState = Enemy_Bot_Base.BotAnimationState.Follow;
        botController.StartCoroutine(Wait(botController));
    }

    public void Exit(Enemy_Bot_Base botController) { botController.StartCoroutine(Wait(botController)); }

    public void Update(Enemy_Bot_Base botController)
    {
        FollowTarget(botController);

        if (!botController.fovScript.playerDetected)
        {
            botController.ChangeState(botController.patrolState);
        }
    }

    void FollowTarget(Enemy_Bot_Base botController)
    {
        if (botController.botAI.isStopped) { return; }

        Collider target = botController.fovScript.targetCollider;

        botController.botAI.ResetPath();

        botController.botAI.SetDestination(target.gameObject.transform.position);
    }

    /*---------------------------------------------------------------*/

    IEnumerator Wait(Enemy_Bot_Base botController)     // Bot karakteri gördüðünde animasyona girmesi için bir müddet bekletilir
    {
        botController.botAI.isStopped = true;
        yield return new WaitForSeconds(1.5f);
        botController.botAI.isStopped = false;
    }

}
