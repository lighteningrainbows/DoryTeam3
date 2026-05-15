using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public CompanionRobot robot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            robot.ToggleFix();
        }
    }
}