using UnityEngine;

public class HpHeatingColor : MonoBehaviour
{
    [Header("Material Properties")]
    [SerializeField] private Material botMaterial;
    [SerializeField] private float colorChangeSpeed;

    // Script References
    private Enemy_Bot_Base botController;

    // Can deðerleri deðiþkenleri
    private float Health;
    private float MaxHealth;
    private float HealthRatio;

    void Start()
    {
        botController = GetComponent<Enemy_Bot_Base>();

        MaxHealth = botController.BotMaxHealth;
        Health = botController.BotHealth;
    }

    void Update()
    {
        HpHeating();
    }

    public void HpHeating()
    {
        Health = botController.BotHealth;
        HealthRatio = Health / MaxHealth;

        // Can AZALDIKÇA kýrmýzýlaþsýn diye tersliyoruz
        float targetG = Mathf.Lerp(140f / 255f, 1f, HealthRatio);
        float targetB = Mathf.Lerp(0f, 1f, HealthRatio);

        Color targetColor = botMaterial.color;

        targetColor.g = Mathf.MoveTowards(
            targetColor.g,
            targetG,
            colorChangeSpeed * Time.deltaTime
        );

        targetColor.b = Mathf.MoveTowards(
            targetColor.b,
            targetB,
            colorChangeSpeed * Time.deltaTime
        );

        botMaterial.color = targetColor;
    }
}