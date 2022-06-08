using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private float launchForce;
    [SerializeField] private float fireDelay;
    private float cooldownTime = 0;

    public void LaunchBomb()
    {
        if (cooldownTime <= Time.time)
        {
            cooldownTime = Time.time + fireDelay;
            GameObject launchedBomb = Instantiate(bomb, transform.position, Quaternion.identity);
            launchedBomb.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }
    }
}
