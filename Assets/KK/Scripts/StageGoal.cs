using UnityEngine;

public class StageGoal : MonoBehaviour
{
    StageManager stageManager;

    void Start()
    {
        stageManager = FindAnyObjectByType<StageManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        stageManager.StageClear();
    }
}