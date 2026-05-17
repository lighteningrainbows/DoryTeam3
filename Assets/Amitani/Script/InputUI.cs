using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// タイトル画面での選択キー入力を管理するクラス
/// </summary>
public class InputUI : MonoBehaviour
{
    /// <summary>
    /// 上方向へのキー
    /// </summary>
    private Key upKey       = Key.W;

    /// <summary>
    /// 下方向へのキー
    /// </summary>
    private Key downKey     = Key.S;

    /// <summary>
    /// 決定キー
    /// </summary>
    private Key decisionKey = Key.F;

    /// <summary>
    /// キー入力を通知するイベント
    /// </summary>
    public event Action OnMoveUp;
    public event Action OnMoveDown;
    public event Action OnDecision;

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null )
            return;

        if (keyboard[upKey].wasPressedThisFrame) 
            Debug.Log("W 検知");
        if (keyboard[downKey].wasPressedThisFrame) 
            Debug.Log("S 検知");

        if (keyboard[upKey].wasPressedThisFrame)
            OnMoveUp?.Invoke();
        if (keyboard[downKey].wasPressedThisFrame) 
            OnMoveDown?.Invoke();
        if (keyboard[decisionKey].wasPressedThisFrame) 
            OnDecision?.Invoke();
    }
}
