using UnityEngine;

public class StageGoal : MonoBehaviour
{
    public StageManager stageManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        stageManager.LoadNextStage();
    }
}