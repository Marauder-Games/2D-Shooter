using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
   private Transform aimTransform;
   public Transform firePoint;
   private Animator aimAnimator;
   
   void Awake() 
   {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
   }

    // Update is called once per frame
    void Update()
    {
        Aiming();
        Shooting();
    }

    void Aiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            localScale.y = -1f;
            //firePoint.Rotate(0, +1f, 0);
        } else {
            localScale.y = +1f;
            //firePoint.Rotate(0, -1f, 0);
        }
        aimTransform.localScale = localScale;
    }
    
    void Shooting() 
    {
        if (Input.GetMouseButtonDown(0)) {
            aimAnimator.SetTrigger("Shoot");
        }
    }
}
