using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTarget : MonoBehaviour, IDamageTaker
{
    private float Health = 10;

    [SerializeField]
    GameObject dyingParticles;
    
    PlayerShooting playerShooting;

    public int upgradeType;
    
    public int damageUpgradeValue = 5;
    
    public int attackSpeedUpgradeValue = 20;

    public int poisonUpgradeValue = 4;

    public int frostUpgradeValue = 2;


    private void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();

        upgradeType = Random.Range(1, 5);

        switch (upgradeType)
        {
            case 1:
                GetComponentInChildren<Text>().text = "<anim>EXTRA DAMAGE</anim>";
                break;
            case 2:
                GetComponentInChildren<Text>().text = "<anim>EXTRA ATTACK SPEED</anim>";
                break;
            case 3:
                GetComponentInChildren<Text>().text = "<anim>POISON ATTACK</anim>";
                break;
            case 4:
                GetComponentInChildren<Text>().text = "<anim>FREEZING ATTACK</anim>";
                break;
        }
        
    }


    public void TakeDamage(float value)
    {
        if (value > 0)
        {
            Health -= value;
        }

        if (Health <= 0)
        {
            Instantiate(dyingParticles, transform.position, transform.rotation);
            switch (upgradeType)
            {
                case 1:
                    UpgradePlayerDamage();                    
                    break;
                case 2:
                    UpgradePlayerAttackSpeed();
                    break;
                case 3:
                    AddPoisonDamage();
                    break;
                case 4:
                    AddFrostPower();
                    break;

            }            
            StartCoroutine("DeathDelay");
            GetComponentInParent<UpgradeTargetsSpawner>().ClearTargets();
        }
    }

    void UpgradePlayerDamage()
    {
        playerShooting.ImproveDamage(damageUpgradeValue);
    }

    void UpgradePlayerAttackSpeed()
    {
        playerShooting.ImproveAttackSpeed(attackSpeedUpgradeValue);
    }
    void AddPoisonDamage()
    {
        playerShooting.AddPoisonDamage(poisonUpgradeValue);
    }
    void AddFrostPower()
    {
        playerShooting.AddFreezePower(frostUpgradeValue);
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(transform.parent.gameObject);
    }

    public void TakePoisonDamage(float value)
    {
        TakeDamage(value);
    }

    public void SlowingMovement(float value)
    {
        TakeDamage(value);
    }
}
