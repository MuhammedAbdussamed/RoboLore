using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : Enemy_IState
{
    void Enter(Enemy_Bot_Base botController) { botController.botState = Enemy_Bot_Base.BotState.Patrol; }

    void Exit(Enemy_Bot_Base botController) { }

    void Update(Enemy_Bot_Base botController)
    {
        Patrol(botController);
    }

    #region Functions

    void Patrol(Enemy_Bot_Base botController)
    {
        switch (botController.currentPoint)
        {
            case Enemy_Bot_Base.PatrolPointState.None:
                SetPatrolPoint(botController, 0);
                break;

            case Enemy_Bot_Base.PatrolPointState.point1:
                SetPatrolPoint(botController, 1);
                break;

            case Enemy_Bot_Base.PatrolPointState.point2:
                SetPatrolPoint(botController, 2);
                break;

            case Enemy_Bot_Base.PatrolPointState.point3:
                SetPatrolPoint(botController, 3);
                break;
        }
    }


    void SetPatrolPoint(Enemy_Bot_Base botController, int patrolPointIndex)
    {
        botController.botAI.ResetPath();

        botController.botAI.SetDestination(botController.patrolPoints[patrolPointIndex].position);

        botController.transform.LookAt(new Vector3(
            botController.botAI.destination.x,
            botController.transform.position.y,
            botController.botAI.destination.z
        ));
    }

/*-------------------------------------------------------*/

    IEnumerator CheckPatrolPoints(Enemy_Bot_Base botController)
    {
        float distance = botController.botAI.remainingDistance;

        if (distance < 0.25f)
        {
            botController.botAI.isStopped = true;

            yield return new WaitForSeconds(botController.CornerWaitTime);

            botController.currentPoint = (Enemy_Bot_Base.PatrolPointState)((int)botController.currentPoint + 1);    // Mevcut enum elemaninin indexini al ve 1 ekle. Artik bu bir intager. En baþtaki kod satiri ile tekrar enuma çeviriyoruz.

            botController.botAI.isStopped = false;
        }
    }

    #endregion
}
    


