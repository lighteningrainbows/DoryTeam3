using UnityEngine;
using System.Collections;
using AudioName;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefabs;

    public Transform stageRoot;

    public Transform player;
    public Transform robot;

    public StageRankData[] rankDatas;

    public StageClearUI clearUI;

    public GameTimerUI gameTimerUI;

    int currentStageIndex;
    GameObject currentStageObject;
    StageInfo currentStageInfo;

    float stageTimer;
    bool isStagePlaying;
    bool isClearScreen;
    bool isGameClearing;

    public string titleSceneName = "TitleScene";

    void Start()
    {
        clearUI.Hide();

        LoadStage(0);

        AudioManager.Instance.PlayLoopBGM(BGMName.GAME_BGM_NAME);
    }

    void Update()
    {
        if (isGameClearing) return;

        if (isStagePlaying)
        {
            stageTimer += Time.deltaTime;

            if (gameTimerUI != null)
            {
                gameTimerUI.SetTime(stageTimer);
            }
        }

        if (isClearScreen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GoNextStageFromClear();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadStage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadNextStage();
        }
    }

    public void LoadStage(int index, bool resetTimer = true)
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

        if (!AudioManager.Instance.IsPlaying(BGMName.GAME_BGM_NAME))
        {
            AudioManager.Instance.PlayLoopBGM(BGMName.GAME_BGM_NAME);
        }

        clearUI.Hide();

        isClearScreen = false;
        isStagePlaying = true;

        if (resetTimer)
        {
            stageTimer = 0f;

            if (gameTimerUI != null)
            {
                gameTimerUI.SetTime(0f);
            }
        }

        ResetActors();
    }
    public void ReloadStage()
    {
        if (isGameClearing) return;

        LoadStage(currentStageIndex, false);
    }

    public void StageClear()
    {
        if (isClearScreen) return;

        isStagePlaying = false;
        isClearScreen = true;

        StopPlayerControl();

        AudioManager.Instance.StopBGM(BGMName.GAME_BGM_NAME);

        StageRankData rankData =
            rankDatas[currentStageIndex];

        clearUI.Show(stageTimer, rankData);

        AudioManager.Instance.PlaySE(SEName.CLEAR_SE_NAME);
    }

    void GoNextStageFromClear()
    {
        int nextIndex = currentStageIndex + 1;

        if (nextIndex >= stagePrefabs.Length)
        {
            StartCoroutine(GameClearRoutine());
            return;
        }

        LoadStage(nextIndex);
    }

    public void LoadNextStage()
    {
        StageClear();
    }

    IEnumerator GameClearRoutine()
    {
        isGameClearing = true;

        AudioManager.Instance.StopBGM(BGMName.GAME_BGM_NAME);
        AudioManager.Instance.PlaySE(SEName.CLEAR_SE_NAME);

        float waitTime =
            AudioManager.Instance.GetSELength(SEName.CLEAR_SE_NAME);

        yield return new WaitForSeconds(waitTime);

        Time.timeScale = 1f;

        SceneManager.LoadScene(titleSceneName);
    }

    void StopPlayerControl()
    {
        PlayerGridMovement playerMove =
            player.GetComponent<PlayerGridMovement>();

        if (playerMove != null)
        {
            playerMove.CanControl = false;
            playerMove.ResetState();
        }

        PlayerKick playerKick =
            player.GetComponent<PlayerKick>();

        if (playerKick != null)
        {
            playerKick.CanControl = false;
        }

        PlayerAction playerAction =
            player.GetComponent<PlayerAction>();

        if (playerAction != null)
        {
            playerAction.CanControl = false;
        }

        CompanionRobot companion =
            robot.GetComponent<CompanionRobot>();

        if (companion != null)
        {
            companion.ResetState();
        }
    }

    void ResetActors()
    {
        player.position = currentStageInfo.playerStart.position;
        robot.position = currentStageInfo.robotStart.position;

        PlayerGridMovement playerMove =
            player.GetComponent<PlayerGridMovement>();

        if (playerMove != null)
        {
            playerMove.CanControl = true;
            playerMove.ResetState();
        }

        PlayerKick playerKick =
            player.GetComponent<PlayerKick>();

        if (playerKick != null)
        {
            playerKick.CanControl = true;
        }

        PlayerAction playerAction =
            player.GetComponent<PlayerAction>();

        if (playerAction != null)
        {
            playerAction.CanControl = true;
        }

        CompanionRobot companion =
            robot.GetComponent<CompanionRobot>();

        if (companion != null)
        {
            companion.ResetState();
        }
    }
}