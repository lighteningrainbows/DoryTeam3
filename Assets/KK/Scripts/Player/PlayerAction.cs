using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public CompanionRobot robot;

    public bool CanControl { get; set; } = true;

    void Update()
    {
        if (!CanControl) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            robot.ToggleFix();
        }
    }
}