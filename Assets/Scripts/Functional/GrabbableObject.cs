using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Collider objectCollider;
    private List<Collider> collisionsTriggers;
    void Awake()
    {
        this.collisionsTriggers = new List<Collider>();
        this.rb = GetComponent<Rigidbody>();
        this.objectCollider = GetComponent<Collider>();
    }
    public Rigidbody GetRigidbody()
    {
        return this.rb;
    }
    public Collider GetCollider()
    {
        return this.objectCollider;
    }
    public List<Collider> GetAllTriggersOnCollision()
    {
        return this.collisionsTriggers;
    }
    public void SetKinematic(bool kinematicStatus)
    {
        this.rb.isKinematic = kinematicStatus;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou Trigger!");
        this.collisionsTriggers.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Saiu Trigger!");
        this.collisionsTriggers.Remove(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject tmpGameObject = collision.gameObject;
        if (tmpGameObject.CompareTag("Ground"))
        {
            LevelController.grabbableObjectSpawner.SpawnGrabbableObject();
            Destroy(this.gameObject);
        }
    }
}
