using UnityEngine;
using System;
using UnityEngine.UI;
using AudioName;

/// <summary>
/// UIを選択時にSpriteを変化させるクラス
/// </summary>
public class SelectSpriteUI : MonoBehaviour
{
    /// <summary>
    /// 必要な情報を入れるクラス
    /// </summary>
    [Serializable]
    public class SpriteSet
    {
        [Tooltip("対象のUIボタン")]
        public Button button;

        [Tooltip("非選択時のSprite")]
        public Sprite sprite1;

        [Tooltip("選択時のSprite")]
        public Sprite sprite2;

        [Tooltip("非選択時の表示サイズ（width, height）")]
        public Vector2 sizeSprite1 = new Vector2(100f, 50f);

        [Tooltip("選択時の表示サイズ（width, height）")]
        public Vector2 sizeSprite2 = new Vector2(100f, 50f);

        public Image image;
    }

    [SerializeField]
    private SpriteSet[] spriteSets;

    [SerializeField]
    private InputUI inputUI;

    // 未選択状態かどうか
    private bool isUnSelected = true;

    private int currentIndex;

    private void Awake()
    {
        if (inputUI == null) 
            inputUI = GetComponent<InputUI>();

        if (inputUI == null)
        {
            Debug.LogError("[ButtonSelectorUI] InputHandler が見つかりません。");
            return;
        }

        inputUI.OnMoveUp   += HandleMoveUp;
        inputUI.OnMoveDown += HandleMoveDown;
    }

    private void Start()
    {
        InitImage();
        ApplyAllUnSelected();
    }

    private void OnDestroy()
    {
        if (inputUI == null)
            return;

        inputUI.OnMoveUp   -= HandleMoveUp;
        inputUI.OnMoveDown -= HandleMoveDown;
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void InitImage()
    {
        for (int i = 0; i < spriteSets.Length; i++)
        {
            SpriteSet set = spriteSets[i];

            // button が未設定
            if (set.button == null)
            {
                Debug.LogWarning($"[SelectSpriteUI] spriteSets[{i}].button が未設定です。");
                continue;
            }

            // targetImage が Inspector で未設定なら Button と同 GameObject から自動取得
            if (set.image == null)
            {
                set.image = set.button.GetComponent<Image>();

                if (set.image == null)
                    Debug.LogWarning($"[SelectSpriteUI] spriteSets[{i}] に Image が見つかりません。" +
                                     "targetImage を Inspector で直接設定してください。");
            }
        }
    }

    private void HandleMoveUp()   => MoveSelection(-1);
    private void HandleMoveDown() => MoveSelection(+1);

    /// <summary>
    /// W/Sキーの入力を有効・無効化する関数
    /// </summary>
    public void SetInputEnabled(bool enabled)
    {
        if (inputUI == null)
            return;

        if (enabled)
        {
            inputUI.OnMoveUp   += HandleMoveUp;
            inputUI.OnMoveDown += HandleMoveDown;
        }
        else
        {
            inputUI.OnMoveUp   -= HandleMoveUp;
            inputUI.OnMoveDown -= HandleMoveDown;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    private void MoveSelection(int direction)
    {
        if (spriteSets.Length == 0)
            return;

        int beforeIndex = currentIndex;

        if (isUnSelected)
        {
            currentIndex = direction > 0
                ? spriteSets.Length - 1
                : 0;

            isUnSelected = false;
        }
        else
        {
            currentIndex =
                (currentIndex + direction + spriteSets.Length)
                % spriteSets.Length;
        }

        // 選択が変わった時だけSE
        if (beforeIndex != currentIndex || isUnSelected == false)
        {
            AudioManager.Instance.PlaySE(SEName.PUNCH_SE_NAME);
        }

        ApplySelection(currentIndex);
    }
    private void ApplyAllUnSelected()
    {
        for (int i = 0; i < spriteSets.Length; i++)
        {
            ApplySpriteAndSize(i, selected: false);
        }
    }

    /// <summary>
    /// ボタンが選択されているかどうかで、スプライトを切り替える関数
    /// </summary>
    /// <param name="index"></param>
    private void ApplySelection(int index)
    {
        for (int i = 0; i < spriteSets.Length;i++)
        {
            ApplySpriteAndSize(i, selected: i == index);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ApplySpriteAndSize(int i, bool selected)
    {
        SpriteSet set = spriteSets[i];
        if (set.image == null)
            return;

        set.image.sprite = selected ? set.sprite2 : set.sprite1;

        RectTransform rt = set.image.rectTransform;
        rt.sizeDelta = selected ? set.sizeSprite2 : set.sizeSprite1;
    }

    /// <summary>
    /// indexを設定できる関数
    /// </summary>
    /// <param name="index"></param>
    public void SetSelection(int index)
    {
        if (!IsValidIndex(index))
            return;

        currentIndex = index;
        isUnSelected = false;
        ApplySelection(currentIndex);
    }

    /// <summary>
    /// 現在のindexを返す関数
    /// </summary>
    /// <returns></returns>
    public int GetCurrentIndex() => currentIndex;

    /// <summary>
    /// 一度もW/Sが押されておらず未選択状態かどうかを返す
    /// </summary>
    public bool IsUnSelected => isUnSelected;

    /// <summary>
    /// spriteSetsの配列ないに収まっているかを確認する関数
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool IsValidIndex(int index) 
        => spriteSets != null && index >= 0 && index < spriteSets.Length;
}
