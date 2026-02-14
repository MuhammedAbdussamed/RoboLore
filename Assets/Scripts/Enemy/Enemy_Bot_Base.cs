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
    [SerializeField] internal float BotMaxHealth;
    [SerializeField] internal BotType botType;
    
    [Header("Patrol Points")]
    [SerializeField] public Transform[] patrolPoints;
    internal bool[] patrolPointBools;
    internal int patrolPointIndex;

    // Bot States
    internal BotAnimationState botAnimationState;
    internal Enemy_IState currentState;
    internal Enemy_IState patrolState;
    internal Enemy_IState followState;

    // Script References
    internal FOV fovScript;

    // Components
    internal NavMeshAgent botAI;
    internal Rigidbody rb;

    void Awake()
    {
        // States
        patrolState = new PatrolState();
        followState = new FollowState();
        botAI = new NavMeshAgent();

        currentState = patrolState;

        // Components
        botAI = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        fovScript = GetComponentInChildren<FOV>();
    }

    void Start()
    {
        patrolPointBools = new bool[patrolPoints.Length];
        currentState.Enter(this);
    }

    void Update()
    {
        currentState.Update(this);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("ThrowableObjects"))
        {

        }
    }

    #region Functions

    public void ChangeState(Enemy_IState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    #endregion

    #region Enums

    public enum BotAnimationState
    {
        Idle,
        Walk,
        Follow,
        Attack
    }

    public enum BotType
    {
        SpiderBot
    }

    #endregion

}
