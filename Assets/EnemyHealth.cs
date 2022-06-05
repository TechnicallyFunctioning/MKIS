using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private UnityEvent deathEvent;
    private float enemyHealth;
    private bool alive;

    private void Awake()
    {
        enemyHealth = maxHealth;
        alive = true;
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    private void Update()
    {
        if(enemyHealth <= 0 && alive) { deathEvent.Invoke(); alive = false; }
    }
}
