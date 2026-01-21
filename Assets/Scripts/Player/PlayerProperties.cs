using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] internal float Speed;
    [SerializeField] internal float JumpPower;

    // Flags
    [SerializeField] internal bool onGround;
    [SerializeField] internal bool faceRight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
