using Assets.Scripts.Industrial_Robot_Arm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustrialRobotArm : MonoBehaviour
{
    [SerializeField] private Transform grabbedObjectTransform;
    [SerializeField] private Transform controllerBaseTransform;
    [SerializeField] private Transform controllerArmTransform;
    [SerializeField] private Transform controllerHeadTransform;
    [SerializeField] private Transform controllerEnd2Transform;
    [SerializeField] private Transform controllerEnd1Transform;
    [SerializeField] private float controllerBaseRotationSpeed = 40;
    [SerializeField] private float controllerArmRotationSpeed = 40;
    [SerializeField] private float controllerHeadRotationSpeed = 40;
    [SerializeField] private float controllerEnd2RotationSpeed = 40;
    [SerializeField] private float controllerEnd1RotationSpeed = 40;
    [SerializeField] private BoxCollider pincerRightTrigger;
    [SerializeField] private BoxCollider pincerLeftTrigger;
    [SerializeField] private bool on = true;
    private int controllerBaseRotationDirection = 0;
    private int controllerArmRotationDirection = 0;
    private int controllerHeadRotationDirection = 0;
    private int controllerEnd2RotationDirection = 0;
    private int controllerEnd1RotationDirection = 0;
    private int clawState = (int)CLAW_STATE.OPEN;
    private bool autoMode = false;
    private GrabbableObject grabbableObject;
    private Animator animator;
    private void Awake()
    {
        LevelController.robotArm = this;
        this.animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (IsOn())
        {
            ProcessControllers();
        }
    }
    private void ProcessControllers()
    {
        switch(this.controllerBaseRotationDirection)
        {
            case -1:
                this.controllerBaseTransform.rotation = this.controllerBaseTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, this.controllerBaseRotationSpeed) * Time.deltaTime));
                break;
            case 1:
                this.controllerBaseTransform.rotation = this.controllerBaseTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, -this.controllerBaseRotationSpeed) * Time.deltaTime));
                break;
        }
        switch (this.controllerArmRotationDirection)
        {
            case -1:
                this.controllerArmTransform.rotation = this.controllerArmTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, this.controllerArmRotationSpeed) * Time.deltaTime));
                break;
            case 1:
                this.controllerArmTransform.rotation = this.controllerArmTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, -this.controllerArmRotationSpeed) * Time.deltaTime));
                break;
        }
        switch (this.controllerHeadRotationDirection)
        {
            case -1:
                this.controllerHeadTransform.rotation = this.controllerHeadTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, this.controllerHeadRotationSpeed) * Time.deltaTime));
                break;
            case 1:
                this.controllerHeadTransform.rotation = this.controllerHeadTransform.rotation * (Quaternion.Euler(new Vector3(0, 0, -this.controllerHeadRotationSpeed) * Time.deltaTime));
                break;
        }
        switch (this.controllerEnd2RotationDirection)
        {
            case -1:
                this.controllerEnd2Transform.rotation = this.controllerEnd2Transform.rotation * (Quaternion.Euler(new Vector3(this.controllerEnd2RotationSpeed, 0, 0) * Time.deltaTime));
                break;
            case 1:
                this.controllerEnd2Transform.rotation = this.controllerEnd2Transform.rotation * (Quaternion.Euler(new Vector3(-this.controllerEnd2RotationSpeed, 0, 0) * Time.deltaTime));
                break;
        }
        switch (this.controllerEnd1RotationDirection)
        {
            case -1:
                this.controllerEnd1Transform.rotation = this.controllerEnd1Transform.rotation * (Quaternion.Euler(new Vector3(0, this.controllerEnd1RotationSpeed, 0) * Time.deltaTime));
                break;
            case 1:
                this.controllerEnd1Transform.rotation = this.controllerEnd1Transform.rotation * (Quaternion.Euler(new Vector3(0, -this.controllerEnd1RotationSpeed, 0) * Time.deltaTime));
                break;
        }
    }
    public void Grab()
    {
        if (IsOn())
        {
            switch(this.clawState)
            {
                case (int)CLAW_STATE.OPEN:
                    this.clawState = (int)CLAW_STATE.CLOSED;
                    this.animator.SetInteger("clawState", Mathf.Clamp(this.clawState, 0, 1));
                    
                    break;
                case (int)CLAW_STATE.CLOSED:
                    DropGrabbableObject();
                    this.animator.speed = 1;
                    this.clawState = (int)CLAW_STATE.OPEN;
                    this.animator.SetInteger("clawState", Mathf.Clamp(this.clawState, 0, 1));
                    break;
            }
        }
    }
    private void DropGrabbableObject()
    {
        if (this.grabbableObject != null)
        {
            this.grabbableObject.transform.SetParent(this.grabbableObject.transform.root);
            this.grabbableObject.GetRigidbody().isKinematic = false;
            this.grabbableObject = null;
        }
    }
    public void IsGrabbingSomething()
    {
        if (IsOn())
        {
            if (this.grabbableObject == null)
            {
                Grab();
            }
        }
    }
    public void SetControllerBaseRotation(int direction)
    {
        this.controllerBaseRotationDirection = direction;
    }
    public void SetControllerArmRotation(int direction)
    {
        this.controllerArmRotationDirection = direction;
    }
    public void SetControllerHeadRotation(int direction)
    {
        this.controllerHeadRotationDirection = direction;
    }
    public void SetControllerEnd2Rotation(int direction)
    {
        this.controllerEnd2RotationDirection = direction;
    }
    public void SetControllerEnd1Rotation(int direction)
    {
        this.controllerEnd1RotationDirection = direction;
    }
    public bool IsOn()
    {
        return this.on;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject tmpObject = other.gameObject;
        if (tmpObject.CompareTag("Grabbable"))
        {
            Debug.Log("Pass Compare!");
            GrabbableObject grabbableObject = tmpObject.GetComponent<GrabbableObject>();
            List<Collider> grabbableObjectCollisions = grabbableObject.GetAllTriggersOnCollision();
            if (grabbableObjectCollisions.Contains(this.pincerLeftTrigger) && grabbableObjectCollisions.Contains(this.pincerRightTrigger) && this.clawState == (int)CLAW_STATE.CLOSED)
            {
                Debug.Log("Contains Pincer Colliders!");
                this.grabbableObject = grabbableObject;
                this.grabbableObject.SetKinematic(true);
                this.grabbableObject.transform.SetParent(this.grabbedObjectTransform);
                if (!this.autoMode)
                {
                    this.animator.speed = 0;
                }
            }
        }
    }
    public void RunAutoMode()
    {
        if (!this.autoMode)
        {
            this.autoMode = true;
            this.animator.SetBool("autoMode", this.autoMode);
        }
    }
    public void ExitAutoMode()
    {
        if (!this.autoMode)
        {
            this.autoMode = false;
            this.animator.SetBool("autoMode", this.autoMode);
        }
    }
}
