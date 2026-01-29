using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] private GameObject[] bullets;
    internal Bullet[] bulletScripts;

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
        for (int i = 0; i < bulletScripts.Length; i++)
        {
            if (!bulletScripts[i].isFired)
            {
                bullets[i].SetActive(true);
                bulletScripts[i].isFired = true;
                return;
            }
        }
    }

    #endregion
}
