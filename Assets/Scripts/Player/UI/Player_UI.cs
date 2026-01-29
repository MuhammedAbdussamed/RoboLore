using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image heatBar;

    // Script References
    private PlayerController playerController;

    void Start()
    {
        playerController = PlayerController.Instance;
    }

    void Update()
    {
        SetBarsValue();
    }

    #region Functions

    void SetBarsValue()
    {
        hpBar.fillAmount = playerController.playerProperties.Health / playerController.playerProperties.MaxHealth;  // Caný maksimum cana bölüyoruz. Çünkü can barinin alabileceði en büyük deðer 1.
        heatBar.fillAmount = playerController.playerProperties.Heat / playerController.playerProperties.MaxHeat;    // Ayni iþlemi heat deðeri için yapiyoruz
    }

    #endregion
}
