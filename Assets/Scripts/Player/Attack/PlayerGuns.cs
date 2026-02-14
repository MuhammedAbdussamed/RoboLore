using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField] private Gun_Base pistolGun;
    [SerializeField] private Gun_Base machineGun;
    [SerializeField] private Gun_Base currentGun;
    private int maxBullet;

    // Events
    public static Action OnAttack;

    // Bools
    [SerializeField] private bool canShoot;
    
    // Bullets
    [SerializeField] private Transform[] bullets;

    // Scripts
    internal PlayerProperties playerProperties;
    internal PlayerController playerController;
    [SerializeField] internal Bullet[] bulletScripts;

    void OnEnable()
    {
        InputManager.OnAttackInput += StartChooseBulletFunction;
        PlayerController.OnSwitchGun += SwitchGun;

        playerController = PlayerController.Instance;
        playerProperties = playerController.playerProperties;
    }

    void OnDisable()
    {
        InputManager.OnAttackInput -= StartChooseBulletFunction;
        PlayerController.OnSwitchGun -= SwitchGun;
    }

    void Start()
    {
        currentGun = pistolGun;

        canShoot = true;

        maxBullet = 25;

        AssignBullets();
    }

    #region Functions

    void AssignBullets()
    {
        bulletScripts = GetComponentsInChildren<Bullet>(true);  // True yazarsak inactive olanlarida alir. Tam olarak istediðimiz bu.

        bullets = new Transform[bulletScripts.Length];

        for (int i = 0; i < maxBullet; i++)
        {
            bullets[i] = bulletScripts[i].transform;
        }
    }

    /*---------------------------------------------------*/

    void StartChooseBulletFunction() 
    {
        Debug.Log("Saldiriyor");
        StartCoroutine(ChooseBullet()); 
    }

    IEnumerator ChooseBullet()
    {
        float gap = playerProperties.MaxHeat - playerProperties.Heat;   // Daha ne kadar isinabilir ?

        if(gap < currentGun.HeatRate || !canShoot) { yield break; }

        canShoot = false;

        for (int i = 0; i < bulletScripts.Length; i++)
        {
            if (!bulletScripts[i].isFired)
            {
                bullets[i].gameObject.SetActive(true);

                bulletScripts[i].isFired = true;

                playerProperties.Heat += currentGun.HeatRate;

                yield return new WaitForSeconds(4f / currentGun.AttackSpeed);

                canShoot = true;

                yield break;
            }
        }
    }

    /*-------------------------------------------------------------------*/

    void SwitchGun()
    {
        switch (playerController.currentWeapon)
        {
            case PlayerController.Gun.Pistol:
                currentGun = pistolGun;
                break;

            case PlayerController.Gun.MachineGun:
                currentGun = machineGun;
                break;
        }
    }

    #endregion
}
