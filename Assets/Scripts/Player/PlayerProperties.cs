using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] internal float Speed;
    [SerializeField] internal float JumpPower;
    [SerializeField] internal float Health;
    [SerializeField] internal float Heat;
    [SerializeField] internal float CoolingRate;
    [SerializeField] internal float BulletSpeed;
    [SerializeField] internal float DashSpeed;
    internal float MaxHeat;
    internal float MaxHealth;

    // Flags
    [SerializeField] internal bool onGround;
    [SerializeField] internal bool faceRight;

    void Start()
    {
        AssignStartValue();
    }

    void Update()
    {
        Clamps();
        HeatControl();
    }

    #region Functions
    void Clamps()
    {
        Heat = Mathf.Clamp(Heat, 0, MaxHeat);
        Health = Mathf.Clamp(Health, 0, MaxHealth);
    }

    /*-----------------------------------------*/

    void AssignStartValue()
    {
        faceRight = true;

        Health = 100f;
        MaxHealth = 100f;

        Heat = 0f;
        MaxHeat = 100f;
    }

    /*----------------------------------------*/

    void HeatControl()
    {
        Heat -= Time.deltaTime * CoolingRate;
    }

    #endregion 
}
