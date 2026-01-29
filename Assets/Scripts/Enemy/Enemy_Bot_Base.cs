using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Bot_Base : MonoBehaviour
{
    [Header("Bot Properties")]
    [SerializeField] internal float BotSpeed;
    [SerializeField] internal float BotHealth;
    [SerializeField] internal float BotAttackDamage;
    [SerializeField] internal float CornerWaitTime;

    [Header("Patrol Points")]
    [SerializeField] public Transform[] patrolPoints;

    [Header("Current Bot State")]
    [SerializeField] internal BotState botState;
    internal PatrolPointState currentPoint;
    internal Enemy_IState currentState;
    internal Enemy_IState patrolState;
    internal Enemy_IState followState;

    // Components
    internal NavMeshAgent botAI;

    // State Bools
    internal bool patroling;
    internal bool attacking;
    internal bool following;

    void Start()
    {
        patrolState = new PatrolState();
        followState = new FollowState();
        botAI = new NavMeshAgent();

        currentState = patrolState;
    }

    void Update()
    {

    }

    #region Functions

    public void ChangeState(Enemy_IState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    #endregion

    public enum BotState
    {
        Patrol,
        Follow,
        Attack
    }

    /*-----------------------------------*/

    public enum PatrolPointState
    {
        None, point1, point2, point3, point4
    }
}
