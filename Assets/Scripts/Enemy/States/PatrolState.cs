using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : Enemy_IState
{
    private float waitTime;
    private int patrolIndex;

    public void Enter(Enemy_Bot_Base botController)
    {
        botController.botAnimationState = Enemy_Bot_Base.BotAnimationState.Walk;
        botController.patrolPointBools[patrolIndex] = true;
        botController.botAI.SetDestination(botController.patrolPoints[patrolIndex].position);
    }

    public void Exit(Enemy_Bot_Base botController) { ResetPatrolPoints(botController); }

    public void Update(Enemy_Bot_Base botController)
    {
        SetWaitTime(botController);
        SetTargetPoint(botController);

        Debug.Log(patrolIndex);

        if (botController.fovScript.playerDetected)
        {
            botController.ChangeState(botController.followState);
        }
    }

    #region Functions

    void Patrol(Enemy_Bot_Base botController)
    {
        for (int i = 0; i < botController.patrolPointBools.Length; i++)
        {
            if (botController.patrolPointBools[i])
            {
                botController.botAI.SetDestination(botController.patrolPoints[i].position);
            }
        }
    }

    /*---------------------------------------------------------------*/

    void SetTargetPoint(Enemy_Bot_Base botController)
    {
        if (botController.botAI.pathPending) { return; }                    // Yol hesaplaniyorsa döngüyü kir

        if (botController.botAI.remainingDistance > 0.2f) { return; }       // Hedefe olan uzaklik 0.2f den fazlaysa döngüyü kýr

        botController.botAI.isStopped = true;                               // Hedefe varilmiþsa botu durdur

        botController.botAnimationState = Enemy_Bot_Base.BotAnimationState.Idle;

        if (waitTime > 0.2f) { return; }                                    // waitTime kadar bekle

        for (int i = 0; i < botController.patrolPointBools.Length; i++)
        {
            if (botController.patrolPointBools[i])
            {
                botController.patrolPointBools[i] = false;                                              // Hedefe varildiði için mevcut boolu false çevir.

                patrolIndex = (patrolIndex + 1) % botController.patrolPointBools.Length;

                botController.patrolPointBools[patrolIndex] = true;

                Patrol(botController);                                                                  // Bool dizisini dön. True olana istikamet ayarla

                botController.botAnimationState = Enemy_Bot_Base.BotAnimationState.Walk;
                break;
            }
        }

        botController.botAI.isStopped = false;
    }

    /*------------------------------------------*/

    void SetWaitTime(Enemy_Bot_Base botController)
    {
        if (botController.botAI.isStopped)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            waitTime = botController.CornerWaitTime;
        }
    }

    /*-------------------------------------------*/

    void ResetPatrolPoints(Enemy_Bot_Base botController)
    {
        for(int i = 0; i < botController.patrolPointBools.Length; i++)
        {
            botController.patrolPointBools[i] = false;
        }
    }

    #endregion
}



