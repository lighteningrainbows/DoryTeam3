using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageClearUI : MonoBehaviour
{
    public GameObject panel;

    public TMP_Text timeText;
    public Image rankImage;

    public bool IsShowing { get; private set; }

    void Start()
    {
        Hide();
    }

    public void Show(float clearTime, StageRankData rankData)
    {
        IsShowing = true;

        panel.SetActive(true);

        timeText.text = FormatTime(clearTime);

        rankImage.sprite =
            rankData.GetRankSprite(clearTime);
    }

    public void Hide()
    {
        IsShowing = false;

        panel.SetActive(false);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int centiSeconds = Mathf.FloorToInt((time * 100f) % 100f);

        return $"{minutes:00}:{seconds:00}";
    }
}