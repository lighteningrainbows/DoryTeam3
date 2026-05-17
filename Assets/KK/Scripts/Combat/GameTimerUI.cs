using UnityEngine;
using TMPro;

public class GameTimerUI : MonoBehaviour
{
    public TMP_Text timerText;

    public void SetTime(float time)
    {
        int minutes =
            Mathf.FloorToInt(time / 60f);

        int seconds =
            Mathf.FloorToInt(time % 60f);

        int centiSeconds =
            Mathf.FloorToInt((time * 100f) % 100f);

        timerText.text =
            $"{minutes:00}:{seconds:00}";
    }
}