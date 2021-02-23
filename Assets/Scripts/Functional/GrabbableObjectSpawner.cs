using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private GameObject grabbableObjectPrefab;
    private void Awake()
    {
        LevelController.grabbableObjectSpawner = this;
    }
    public void SpawnGrabbableObject()
    {
        Instantiate(this.grabbableObjectPrefab, this.spawnTransform.position, this.spawnTransform.rotation);
    }
}
