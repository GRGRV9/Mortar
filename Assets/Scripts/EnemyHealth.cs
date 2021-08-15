using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageTaker
{
    public void TakeDamage(float value);
    public void TakePoisonDamage(float value);
    public void SlowingMovement(float value);
}

public class EnemyHealth : MonoBehaviour, IDamageTaker
{
    public float Health { get; private set; }
    Rigidbody rb;
    
    [SerializeField]
    Animator animator;

    [SerializeField]
    float maxHealth = 100;

    [SerializeField]
    GameObject dyingParticles;

    EnemySpawner enemySpawner;

    private void Start()
    {
        Health = maxHealth;
        rb = GetComponentInParent<Rigidbody>();
        enemySpawner = GetComponentInParent<EnemySpawner>();
    }

    public void TakeDamage(float value)
    {
        if (value > 0)
        {
            Health -= value;
            animator.SetTrigger("isDamaged");
            rb.AddForce(0, 300, 0, ForceMode.Impulse);
        }
        else
        {
            throw new Exception ("Value can not be less than 0");
        }


        if (Health <= 0)
        {
            Instantiate(dyingParticles, transform.position, transform.rotation);
            rb.mass = 10;
            enemySpawner.DeathCallback();
            rb.AddForce(0, 600, UnityEngine.Random.Range(-200,200), ForceMode.Impulse);
            StartCoroutine("DeathDelay");
        }        
    }


    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(transform.parent.gameObject);
    }

    public void TakePoisonDamage(float poisonDamage)
    {
        StartCoroutine(PoisonDamaging(poisonDamage));
    }

    public void TakeFrostSlowing(float frostValue)
    {
        SlowingMovement(frostValue);
    }

    IEnumerator PoisonDamaging(float poisonDamage)
    {
        yield return new WaitForSeconds(1.5f);
        TakeDamage(poisonDamage);
        yield return new WaitForSeconds(1.5f);
        TakeDamage(poisonDamage);
        yield return new WaitForSeconds(1.5f);
        TakeDamage(poisonDamage);
    }    

    public void SlowingMovement(float value)
    {
        GetComponentInParent<EnemyMovement>().FreezeSpeed(value);
    }
}
