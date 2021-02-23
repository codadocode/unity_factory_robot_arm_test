using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private IndustrialRobotArm manualRobot;
    [SerializeField] private IndustrialRobotArm autoRobot;
    [SerializeField] private GameObject manualUI;
    [SerializeField] private GameObject autoUI;
    public void StartManualMode()
    {
        this.autoRobot.gameObject.SetActive(false);
        this.manualRobot.gameObject.SetActive(true);
        this.manualUI.SetActive(true);
        this.autoUI.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void StartAutoMode()
    {
        this.autoRobot.gameObject.SetActive(true);
        this.manualRobot.gameObject.SetActive(false);
        this.manualUI.SetActive(false);
        this.autoUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        this.gameObject.SetActive(true);
        this.autoRobot.gameObject.SetActive(false);
        this.manualRobot.gameObject.SetActive(false);
        this.manualUI.SetActive(false);
        this.autoUI.SetActive(false);
    }
}
