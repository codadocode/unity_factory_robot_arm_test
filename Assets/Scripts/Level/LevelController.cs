using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelController
{   
    public static Canvas canvas = GameObject.FindObjectOfType<Canvas>();
    public static IndustrialRobotArm robotArm;
    public static GrabbableObjectReceiver grabbableObjectReceiver;
    public static GrabbableObjectSpawner grabbableObjectSpawner;
    public static int globalPoints = 0;
    public static void ResetPoint()
    {
        globalPoints = 0;
    }
}
