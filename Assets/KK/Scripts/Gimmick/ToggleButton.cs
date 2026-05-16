using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GimmickReceiver[] firstTargets;
    public GimmickReceiver[] secondTargets;

    private bool secondMode;

    public void Press()
    {
        secondMode = !secondMode;

        foreach (GimmickReceiver target in firstTargets)
        {
            if (target == null) continue;

            if (secondMode)
                target.Deactivate();
            else
                target.Activate();
        }

        foreach (GimmickReceiver target in secondTargets)
        {
            if (target == null) continue;

            if (secondMode)
                target.Activate();
            else
                target.Deactivate();
        }
    }
}