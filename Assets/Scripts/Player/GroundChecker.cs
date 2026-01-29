using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float raycastLength;

    // Script References
    internal PlayerController controller;

    void Start()
    {
        controller = PlayerController.Instance;
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))             // Ray doðrultusunda, raycastLenght uzunluðunda bir ýþýn çikart. Çarptiðin objenin özelliklerini hit'e yaz.
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.green, raycastLength);        //  Eðer ýþýn çarparsa yeþil renklendir ve
            controller.playerProperties.onGround = true;                                        //  "Karakter yerde mi?" true çevir.
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red, raycastLength);          // Eðer ýþýn çarpmazsa kirmizi renklendir ve
            controller.playerProperties.onGround = false;                                       // "Karakter yerde mi?" false çevir.
        }
    }
}
