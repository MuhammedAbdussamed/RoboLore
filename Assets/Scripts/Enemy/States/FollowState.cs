using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : Enemy_IState
{
    void Enter(Enemy_Bot_Base botController) { botController.botState = Enemy_Bot_Base.BotState.Follow; }

    void Exit(Enemy_Bot_Base botController) { }

    void Update(Enemy_Bot_Base botController) 
    { 
    
    }

}
