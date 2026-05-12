using System;
using System.Collections;
using UnityEngine;

public class Fade : Singleton<Fade>
{
    /// <summary>
    /// シーンに置かずに生成させる
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnGameLoad()
    {
        var gameObject = new GameObject("Fade");
        DontDestroyOnLoad(gameObject);

        gameObject.AddComponent<Fade>();
    }

    /// <summary>
    /// 双方向フェード可能
    /// </summary>
    /// <param name="from">ここから</param>
    /// <param name="to">ここまで</param>
    /// <param name="duration">時間</param>
    /// <param name="onUpdate">フェード進行中に呼ばれる</param>
    /// <param name="onComplete">フェード完了時に呼ばれる</param>
    public void StartFade(
        float from,
        float to,
        float duration,
        Action<float> onUpdate = null,
        Action onComplete = null
    )
    {
        StartCoroutine(FadeCoroutine(from, to, duration, onUpdate, onComplete));
    }

    private IEnumerator FadeCoroutine(
        float from,
        float to,
        float duration,
        Action<float> onUpdate,
        Action onComplete
    )
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);
            float value = Mathf.Lerp(from, to, t);

            // フェード進行通知
            onUpdate?.Invoke(value);

            yield return null;
        }

        // 最終値を保証
        onUpdate?.Invoke(to);

        // 完了
        onComplete?.Invoke();
    }
}
