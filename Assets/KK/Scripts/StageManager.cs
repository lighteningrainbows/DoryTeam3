using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefabs;

    public Transform stageRoot;

    public Transform player;
    public Transform robot;

    int currentStageIndex;
    GameObject currentStageObject;
    StageInfo currentStageInfo;

    void Start()
    {
        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadStage();
        }
    }

    public void LoadStage(int index)
    {
        if (currentStageObject != null)
        {
            Destroy(currentStageObject);
        }

        currentStageIndex = index;

        currentStageObject =
            Instantiate(stagePrefabs[currentStageIndex], stageRoot);

        currentStageInfo =
            currentStageObject.GetComponent<StageInfo>();

        GridManager.Instance.SetStage(currentStageInfo);

        ResetActors();
    }

    public void ReloadStage()
    {
        LoadStage(currentStageIndex);
    }

    public void LoadNextStage()
    {
        int nextIndex = currentStageIndex + 1;

        if (nextIndex >= stagePrefabs.Length)
        {
            Debug.Log("全ステージクリア");
            return;
        }

        LoadStage(nextIndex);
    }

    void ResetActors()
    {
        player.position = currentStageInfo.playerStart.position;
        robot.position = currentStageInfo.robotStart.position;

        PlayerGridMovement playerMove =
            player.GetComponent<PlayerGridMovement>();

        if (playerMove != null)
        {
            playerMove.ResetState();
        }

        CompanionRobot companion =
            robot.GetComponent<CompanionRobot>();

        if (companion != null)
        {
            companion.ResetState();
        }
    }
}