using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{
    [SerializeField] private float gunDamage;
    [SerializeField] private float fireDelay;
    [SerializeField] private float fireRange;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask splashMask;
    [SerializeField] private GameObject splashObject;
    [SerializeField] private GameObject tracerObject;
    [SerializeField] private float splashDuration;
    private float cooldownTime = 0;

    public void Fire()
    {
        // If Not Cooling Down, Fire
        if(cooldownTime <= Time.time)
        {
            // Reset Cooldown
            cooldownTime = Time.time + fireDelay;
            // Raycast
            Ray gunRay = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            RaycastHit hit;
            Vector3 hitPoint;
            // If Hit an Enemy, Pass Gun Damage to Enemy Health
            if(Physics.Raycast(gunRay, out hit, fireRange, hitMask))
            {
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                if(enemyHealth != null) { enemyHealth.TakeDamage(gunDamage); }
            }
            // If Hit No Enemy, show Splash Effect
            else if(Physics.Raycast(gunRay, out hit, fireRange, splashMask))
            {
                hitPoint = hit.point;
                GameObject newSplash = Instantiate(splashObject, hitPoint, Quaternion.identity);
                Destroy(newSplash, splashDuration);
            }
            // If Hit Nothing, show Tracer Effect
            else
            {
                hitPoint = gunRay.origin + (gunRay.direction * fireRange);
                GameObject newSplash = Instantiate(tracerObject, hitPoint, Quaternion.identity);
                Destroy(newSplash, splashDuration);
            }
        }
    }
}
