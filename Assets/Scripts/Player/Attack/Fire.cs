using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] private GameObject[] bullets;
    internal Bullet[] bulletScripts;

    // Scripts
    internal PlayerProperties playerProperties;

    void OnEnable()
    {
        InputManager.OnAttackInput += ChooseBullet;

    }

    void OnDisable()
    {
        InputManager.OnAttackInput -= ChooseBullet;
    }

    void Start()
    {
        bulletScripts = new Bullet[bullets.Length];
        playerProperties = GetComponent<PlayerProperties>();

        AssignBulletScripts();
    }

    #region Functions

    void AssignBulletScripts()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bulletScripts[i] = bullets[i].GetComponent<Bullet>();
        }
    }

    /*---------------------------------------------------*/

    void ChooseBullet()
    {
        float gap = playerProperties.MaxHeat - playerProperties.Heat;

        if (gap < playerProperties.HeatPerBullet) { return; }

        for (int i = 0; i < bulletScripts.Length; i++)
        {
            if (!bulletScripts[i].isFired)
            {
                bullets[i].SetActive(true);
                bulletScripts[i].isFired = true;
                playerProperties.Heat += playerProperties.HeatPerBullet;
                return;
            }
        }
    }

    #endregion
}
