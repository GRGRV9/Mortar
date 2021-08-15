using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    float damage;
    float poisonDamage = 0;
    float freezingPower = 0;

    [SerializeField]
    float bulletLifetime = 1.5f;

    [SerializeField]
    GameObject particlesPrefab;

    [SerializeField]
    GameObject deathParticlesPrefab;

    public Vector3 delta;

    private void Start()
    {
        StartCoroutine("LifetimeCountdown");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(particlesPrefab, other.transform.position + delta, transform.rotation);
            DamageEnemy(other.GetComponent<IDamageTaker>());
            Destroy(gameObject);
        }        
    }


    public void SetDamage(float value)
    {
        damage = value;
    }

    public void SetPoisonDamage(float value)
    {
        poisonDamage = value;
    }

    public void SetFreezingPower(float value)
    {
        freezingPower = value;
    }




    private void DamageEnemy(IDamageTaker enemyHealth)
    {
        enemyHealth.TakeDamage(damage);

        if (poisonDamage>0)
        {
            enemyHealth.TakePoisonDamage(poisonDamage);
        }

        if (freezingPower>0)
        {
            enemyHealth.SlowingMovement(freezingPower);
        }
    }

    private IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Instantiate(deathParticlesPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
