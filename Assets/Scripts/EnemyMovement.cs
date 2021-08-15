using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{    
    Transform _target;

    [SerializeField]
    float speed;

    float defaultSpeed;

    private void Start()
    {
        defaultSpeed = speed;
    }

    void FixedUpdate()
    {
        transform.Translate(_target.position.normalized * speed * 0.01f);
    }

    public void SetTargetTransform(Transform target)
    {
        _target = target;
    }

    public void FreezeSpeed(float freezeValue)
    {
        speed -= freezeValue;
        Debug.Log($"Speed decreased from: {defaultSpeed} to {speed}");
        StartCoroutine("SpeedReanimation");
    }

    IEnumerator SpeedReanimation()
    {
        yield return new WaitForSeconds(5f);
        speed = defaultSpeed;
        Debug.Log($"Speed reanimated to {speed}");
    }
}
