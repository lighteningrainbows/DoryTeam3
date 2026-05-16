using UnityEngine;
using System.Collections;

public class TimerButton : MonoBehaviour
{
    public GimmickReceiver[] targets;
    public float activeTime = 3f;

    private Coroutine timerRoutine;

    public void Press()
    {
        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        timerRoutine = StartCoroutine(TimerRoutine());
    }

    IEnumerator TimerRoutine()
    {
        SetTargets(true);

        yield return new WaitForSeconds(activeTime);

        SetTargets(false);
        timerRoutine = null;
    }

    void SetTargets(bool active)
    {
        foreach (GimmickReceiver target in targets)
        {
            if (target == null) continue;

            if (active)
                target.Activate();
            else
                target.Deactivate();
        }
    }
}