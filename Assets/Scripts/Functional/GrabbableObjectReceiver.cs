using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabbableObjectReceiver : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Text pointsUIManual;
    [SerializeField] private Text pointsUIAuto;
    private void Awake()
    {
        LevelController.grabbableObjectReceiver = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject tmpGameObject = other.gameObject;
        if (tmpGameObject.CompareTag("Grabbable"))
        {
            GrabbableObject tmpGrabbableObject = tmpGameObject.GetComponent<GrabbableObject>();
            if (!tmpGrabbableObject.GetRigidbody().isKinematic)
            {
                this.particles.Play();
                LevelController.globalPoints++;
                this.pointsUIAuto.text = "Score: " + LevelController.globalPoints.ToString();
                this.pointsUIManual.text = "Score: " + LevelController.globalPoints.ToString();
                Destroy(tmpGrabbableObject.gameObject);
                LevelController.grabbableObjectSpawner.SpawnGrabbableObject();
            }
        }
    }
}
