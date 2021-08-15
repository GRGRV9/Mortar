using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField]
    float delay = 3;
    
    void Start()
    {
        StartCoroutine("SelfDestroy");
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
