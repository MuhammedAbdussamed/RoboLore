using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order = 1)]
public class Gun_Base : ScriptableObject
{
    public string GunName;
    public float AttackSpeed;
    public float BulletSpeed;
    public float AttackDamage;
    public float HeatRate;
}
