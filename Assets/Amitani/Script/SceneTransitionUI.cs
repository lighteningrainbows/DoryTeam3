using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class SceneTransitionUI : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private enum State
    {
        Idle,                   // 通常時
        WaitingSecondConfirm,   // 一回目の決定キーを入力した時
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ButtonSceneEntry
    {
        [Tooltip("移行するシーン名")]
        public string sceneName;

        public bool quitGame = false;
    }

    [SerializeField]
    private InputUI inputUI;

    [SerializeField]
    private SelectSpriteUI selectSpriteUI;

    /// <summary>
    /// 一回目の決定キー入力の際に表示するimage
    /// </summary>
    [SerializeField]
    private GameObject decisionImageObject;

    [SerializeField]
    private ButtonSceneEntry[] sceneEntries;

    private State state = State.Idle;

    private void Awake()
    {
        if (inputUI == null) 
            inputUI = GetComponent<InputUI>();

        if (selectSpriteUI == null)
            selectSpriteUI = GetComponent<SelectSpriteUI>();

        if (inputUI == null)
        {
            Debug.LogError("[SceneTransitionUI] InputUIが見つかりません。");
            return;
        }

        inputUI.OnDecision += HandleDecision;
    }

    private void Start()
    {
        SetDecisionImageActive(false);
    }

    private void OnDestroy()
    {
        if (inputUI != null)
            inputUI.OnDecision -= HandleDecision;
    }

    private void HandleDecision()
    {
        switch (state)
        {
            case State.Idle:
                OnFirstDecision(); 
                break;

            case State.WaitingSecondConfirm:
                OnSecondDecision();
                break;
        }
    }

    /// <summary>
    /// 一回目の決定キー入力の際に実行される関数
    /// </summary>
    private void OnFirstDecision()
    {
        int index = selectSpriteUI != null ? selectSpriteUI.GetCurrentIndex() : 0;

        if (sceneEntries == null || index >= sceneEntries.Length)
        {
            Debug.LogError($"[SceneTransitionUI] sceneEntries[{index}] が未設定です。");
            return;
        }

        ButtonSceneEntry entry = sceneEntries[index];

        if (entry.quitGame)
        {
            Debug.Log("[SceneTransitionUI] ゲームを終了します。");
            QuitGame();
            return;
        }

        selectSpriteUI?.SetInputEnabled(false);
        SetDecisionImageActive(true);
        state = State.WaitingSecondConfirm;

        Debug.Log("[SceneTransitionUI] Imageを表示しました。");
    }

    /// <summary>
    /// 二回目の決定キー入力の際に実行される関数
    /// </summary>
    private void OnSecondDecision()
    {
        int index = selectSpriteUI != null ? selectSpriteUI.GetCurrentIndex() : 0;

        if (sceneEntries == null || index >= sceneEntries.Length)
        {
            Debug.LogError($"[SceneTransitionUI] sceneEntries[{index}] が未設定です。");
            ResetState();
            return;
        }

        string targetScene = sceneEntries[index].sceneName;

        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogError($"[SceneTransitionUI] sceneEntries[{index}].sceneName が空です。");
            ResetState();
            return;
        }

        Debug.Log($"[SceneTransitionUI] シーン '{targetScene}' へ遷移します。");
        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// ゲームを終了する関数
    /// </summary>
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="active"></param>
    private void SetDecisionImageActive(bool active)
    {
        if (decisionImageObject != null) 
            decisionImageObject.SetActive(active);
    }

    /// <summary>
    /// 表示をリセットする関数
    /// </summary>
    private void ResetState()
    {
        selectSpriteUI?.SetInputEnabled(true);
        SetDecisionImageActive(false);
        state = State.Idle;
    }

    /// <summary>
    /// 
    /// </summary>
    public void CancelTransition()
    {
        ResetState();
        Debug.Log("[SceneTransitionUI] 遷移をキャンセルしました。");
    }
}