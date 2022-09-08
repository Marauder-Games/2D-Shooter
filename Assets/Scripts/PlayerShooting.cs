using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    private Transform aimTransform;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabOpposite;

    public float bulletForce = 20f;
    
    void Awake() 
    {
        aimTransform = transform.Find("Aim");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }
    void Shoot()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;

        // This method of using an opposite bullet sprite is super jenk
        // There is a better way of doing it but I'm lazy and I want to move on
        // to more fun stuff. Will fix later.
        if (angle > 90 || angle < -90) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        else {
            GameObject bulletOpp = Instantiate(bulletPrefabOpposite, firePoint.position, firePoint.rotation);
            Rigidbody2D rbOpp = bulletOpp.GetComponent<Rigidbody2D>();
            rbOpp.AddForce(firePoint.up * bulletForce * (-1), ForceMode2D.Impulse);
        }
    }
}
