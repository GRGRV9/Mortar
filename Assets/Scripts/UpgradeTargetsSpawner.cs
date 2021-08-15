using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTargetsSpawner : MonoBehaviour
{
    [SerializeField]
    Transform SpawnerTransform;
    [SerializeField]
    GameObject UpgradeTargetPrefab;

    public void InstantiateUpgradeTargets()
    {
        GameObject target1 = Instantiate(UpgradeTargetPrefab, SpawnerTransform.transform.position, SpawnerTransform.transform.rotation);
        target1.transform.parent = gameObject.transform;
        GameObject target2 = Instantiate(UpgradeTargetPrefab, SpawnerTransform.transform.position + new Vector3(0, 0, -2.7f), SpawnerTransform.transform.rotation);
        target2.transform.parent = gameObject.transform;
        GameObject target3 = Instantiate(UpgradeTargetPrefab, SpawnerTransform.transform.position + new Vector3(0, 0, 2.7f), SpawnerTransform.transform.rotation);
        target3.transform.parent = gameObject.transform;
    }

    public void ClearTargets()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        GetComponent<AudioSource>().Play();
    }
}
