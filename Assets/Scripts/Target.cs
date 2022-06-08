using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject targetModel;
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private float explosionTime = 0.5f;

    private void Awake()
    {
        explosionObject.SetActive(false);
        targetModel.SetActive(true);
    }

    public void Explode()
    {
        explosionObject.SetActive(true);
        targetModel.SetActive(false);
        Destroy(gameObject, explosionTime);
    }
}
