using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Color_Changer : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Material playerMainMaterial;
    [SerializeField] private float colorChangeSpeed;

    // Script References
    private PlayerProperties playerProperties;

    // Variables
    private float heatColor;

    void OnEnable()
    {
        PlayerController.OnTakeDamage += StartBeUntouchable;
    }

    void OnDisable()
    {
        PlayerController.OnTakeDamage -= StartBeUntouchable;
    }

    void Start()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        Heating();
    }

    void Heating()
    {
        float heat = playerProperties.Heat / playerProperties.MaxHeat;  // 0-1 araliðina çektik heati

        float targetG = Mathf.Lerp(1f, 140f / 255f, heat);  // 1'den 140/255 ( 0.55 )'e heat kadar git.
        float targetB = Mathf.Lerp(1f, 0f, heat);           // 1'den 0'a heat kadar git.

        Color targetColor = playerMainMaterial.color;

        targetColor.g = Mathf.MoveTowards(targetColor.g, targetG, colorChangeSpeed * Time.deltaTime);
        targetColor.b = Mathf.MoveTowards(targetColor.b, targetB, colorChangeSpeed * Time.deltaTime);

        playerMainMaterial.color = targetColor;
    }

    /*------------------------------------*/

    void StartBeUntouchable()
    {
        StartCoroutine(BeUntouchable());
    }

    IEnumerator BeUntouchable()
    {
        playerMainMaterial.color = new Color(playerMainMaterial.color.r, playerMainMaterial.color.g, playerMainMaterial.color.b, 0.2f);

        yield return new WaitForSeconds(1.2f);

        playerMainMaterial.color = new Color(playerMainMaterial.color.r, playerMainMaterial.color.g, playerMainMaterial.color.b, 1f);
    }

    /*------------------------------------*/
}
