using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Tooltip("Layers that Raycast will hit")]
    [SerializeField] private LayerMask raycastLayers;

    // Script References
    internal PlayerController controller;

    void Start()
    {
        controller = PlayerController.Instance;
    }

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, raycastLayers))             // Ray doðrultusunda, 2f uzunluðunda bir ýþýn çikart. Çarptiðin objenin özelliklerini hit'e yaz.
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.green, 0.5f);        //  Eðer ýþýn çarparsa yeþil renklendir ve
            controller.playerProperties.onGround = true;                               //  "Karakter yerde mi?" true çevir.
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.5f);          // Eðer ýþýn çarpmazsa kirmizi renklendir ve
            controller.playerProperties.onGround = false;                              // "Karakter yerde mi?" false çevir.
        }
    }
}
