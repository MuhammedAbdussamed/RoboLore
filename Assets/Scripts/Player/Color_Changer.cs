using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Color_Changer : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Material material;
    [SerializeField] private float colorChangeSpeed;

    // Script References
    private PlayerProperties playerProperties;

    // Variables
    private float heatColor;


    void Start()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        float heat = playerProperties.Heat / playerProperties.MaxHeat;  // 0-1 araliðina çektik heati

        float targetG = Mathf.Lerp(1f, 140f / 255f, heat);  // 1'den 140/255 ( 0.55 )'e heat kadar git.
        float targetB = Mathf.Lerp(1f, 0f, heat);           // 1'den 0'a heat kadar git.

        Color targetColor = material.color;

        targetColor.g = Mathf.MoveTowards(targetColor.g, targetG, colorChangeSpeed * Time.deltaTime);
        targetColor.b = Mathf.MoveTowards(targetColor.b, targetB, colorChangeSpeed * Time.deltaTime);

        material.color = targetColor;
    }
}
