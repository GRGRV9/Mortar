using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private float poisonBulletDamage;

    [SerializeField]
    private float freezePower;

    [SerializeField]
    float bulletForce;

    [SerializeField]
    float shotDelay = 3f;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject shotParticlesPrefab;  

    [SerializeField]
    Transform bulletSpawner;    

    bool canShoot = true;
    bool delayRestarting = false;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Shoot()
    {
        if (canShoot)
        {
            if (playerController.isInversed())
            {
                SecondaryWeaponShot();                
            }
            else
            {
                PrimaryWeaponShot();
            }
        }
    }

    IEnumerator Delaying()
    {        
        canShoot = false;
        delayRestarting = true;
        yield return new WaitForSeconds(shotDelay);        
        canShoot = true;
        delayRestarting = false;
    }

    public void RestartDelay()
    {
        if (!delayRestarting)
        {
            StartCoroutine("Delaying");
        }        
    }

    public void ImproveDamage(float value)
    {
        if (value > 0)
        {
            bulletDamage += value;
            Debug.Log($"Damage Upgraded by {value}");
        }
        else
        {
            throw new Exception("Value can not be less than 0");
        }
        
    }

    public void AddPoisonDamage(float value)
    {
        poisonBulletDamage += value;
    }

    public void AddFreezePower(float value)
    {
        freezePower += value;
    }

    public void ImproveAttackSpeed(float value)
    {
        if (value > 0)
        {
            shotDelay = shotDelay - shotDelay*value/100;
            Debug.Log($"Attack Speed Upgraded to {shotDelay}");
        }
        else
        {
            throw new Exception("Value can not be less than 0");
        }

    }

    private void PrimaryWeaponShot()
    {
        GetComponent<AudioSource>().Play();
        Instantiate(shotParticlesPrefab, bulletSpawner.position, bulletSpawner.rotation);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawner.forward * bulletForce * 1000f);
        bullet.GetComponent<BulletBehaviour>().SetDamage(bulletDamage);
        if (poisonBulletDamage>0)
        {
            bullet.GetComponent<BulletBehaviour>().SetPoisonDamage(poisonBulletDamage);
        }

        if (freezePower>0)
        {
            bullet.GetComponent<BulletBehaviour>().SetFreezingPower(freezePower);
        }
        GetComponentInChildren<Animator>().SetTrigger("Shot");
        StartCoroutine("Delaying");
    }

    private void SecondaryWeaponShot()
    {
        
    }


}
