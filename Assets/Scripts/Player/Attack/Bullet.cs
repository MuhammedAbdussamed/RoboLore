using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Script References
    private PlayerController controller;

    // Components
    private Rigidbody bulletRb;

    // Bools
    internal bool isFired;
    private bool isFiring;

    void Start()
    {
        controller = PlayerController.Instance;
        bulletRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Fire();
    }

    void OnCollisionEnter(Collision col)
    {
        DestroyBullet();
    }

    #region Functions

    void Fire()
    {

        if (!isFired || isFiring) { return; }               // Mermi ateþlenmemiþ ise ve þuanda ateþlenmiyor ise...

        isFiring = true;                                    // Ateþleniyor 

        bulletRb.WakeUp();                                  // Merminin fizik hesaplamalarini aç

        if (controller.playerProperties.faceRight)                                                              // Karakter saða bakiyor ise...
        {
            bulletRb.AddForce(Vector3.right * controller.playerProperties.BulletSpeed, ForceMode.Impulse);          // Global x ekseninde pozitif yönde kuvvet uygula
        }

        else                                                                                                    // Karakter sola bakiyor ise
        {
            bulletRb.AddForce(-Vector3.right * controller.playerProperties.BulletSpeed, ForceMode.Impulse);         // Global x ekseninde negatif yönde kuvvet uygula
        }

        StartCoroutine(ResetFire());                                                                            // Mermiyi sýfýrla
    }

    /*-------------------------------------------------------*/

    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(2f);        // 2 saniye bekle      
        DestroyBullet();                            // Mermiyi yok et
        isFired = false;                            // Ateþlendi deðiþkenini sifirla.
        isFiring = false;                           // Döngüyü bitir. Ateþleniyor deðiþkenini sifirla
    }

    /*-------------------------------------------------------*/

    // Object Pooling için mermi yok edilmiyor , yeniden kullanilmak üzere resetleniyor.
    void DestroyBullet()
    {
        gameObject.SetActive(false);                            
        transform.localPosition = new Vector3(0f, 0f, 0f);      // Pozisyonunu sifirla
        transform.rotation = Quaternion.identity;               // Açisini sifirla

        bulletRb.velocity = Vector3.zero;                       // Mermiye uygulanan fiziksel kuvvetleri sifirla.
        bulletRb.angularVelocity = Vector3.zero;                // Mermiye uygulanan açýsal fiziksel kuvvetleri sifirla.
        bulletRb.Sleep();                                       // Fizik hesaplamalarini kapat.

        isFired = false;                            // Ateþlendi deðiþkenini sifirla.
        isFiring = false;                           // Döngüyü bitir. Ateþleniyor deðiþkenini sifirla
    }

    #endregion
}
