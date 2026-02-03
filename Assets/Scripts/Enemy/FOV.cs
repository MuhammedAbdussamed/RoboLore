using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [Header("Raycast Layers")]
    [SerializeField] private LayerMask raycastLayers;

    // Bot Transform
    private Transform botTransform;

    // Movement Bools
    private bool isPlayerIn;
    [SerializeField] internal bool playerDetected;

    // Colliders
    internal Collider targetCollider;

    void Start()
    {
        botTransform = GetComponentInParent<Enemy_Bot_Base>().transform;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerIn = true;
            targetCollider = col;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerIn = false;
            playerDetected = false;
        }
    }

    void Update()
    {
        if (isPlayerIn)
        {
            SentRaycast();
        }
        
    }

    #region Functions 

    void SentRaycast()
    {
        Vector3 dir = (targetCollider.transform.position - botTransform.position).normalized;  // Direction = positiondan > collidera doðru.
        RaycastHit hit;

        if (Physics.Raycast(botTransform.position, dir, out hit, 15f, raycastLayers))
        {
            Debug.DrawRay(botTransform.position, dir * 15f, Color.green);

            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
            }
        }
        else
        {
            Debug.DrawRay(botTransform.position, dir * 15f, Color.red, 100f);
            Debug.Log("Iþýn Çarpmadý!");
        }
    }

    #endregion
}
