using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject bombModel;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float explosionTime = .5f;
    [SerializeField] private float explosionRadius = 1;
    [SerializeField] private float explosionDamage = 1;
    [SerializeField] private LayerMask explosionMask;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        explosion.SetActive(true);
        bombModel.SetActive(false);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);
        foreach(Collider collider in hitColliders)
        {
            EnemyHealth enemyHealth = collider.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null) { enemyHealth.TakeDamage(explosionDamage); }
        }

        Destroy(gameObject, explosionTime);
    }
}
