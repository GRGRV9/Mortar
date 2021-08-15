using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    GameObject explosionParticlesPrefab;

    private bool _isInversed;
    private bool _isActive;

    void Update()
    {
        Rotating();
        AutoShooting();
    }

    private void Rotating()
    {
        float xAxis = joystick.Horizontal * 0.2f;
        float yAxis = joystick.Vertical;

        if (yAxis < 0)
        {
            yAxis *= -1;
            _isInversed = true;
        }
        else
        {
            _isInversed = false;
        }

        float zAxis = Mathf.Atan2(xAxis, yAxis) * Mathf.Rad2Deg;        

        if (zAxis > 0 || zAxis < 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Clamp(zAxis, -17, 17), 0);
            _isActive = true;
        }
        else
        {
            _isActive = false;
            GetComponent<PlayerShooting>().RestartDelay();
        }
    }

    

    private void AutoShooting()
    {
        if (_isActive)
        {
            GetComponent<PlayerShooting>().Shoot();
        }
    }

    public bool isInversed()
    {
        return _isInversed;
    }

    public void PlayerDying()
    {
        Instantiate(explosionParticlesPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
