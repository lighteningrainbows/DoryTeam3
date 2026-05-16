using UnityEngine;

public class MultiSwitchController : MonoBehaviour
{
    public SwitchState[] switches;
    public GimmickReceiver[] targets;

    void Start()
    {
        foreach (SwitchState sw in switches)
        {
            sw.onChanged += CheckSwitches;
        }

        CheckSwitches();
    }

    void CheckSwitches()
    {
        foreach (SwitchState sw in switches)
        {
            if (!sw.IsPressed)
            {
                SetTargets(false);
                return;
            }
        }

        SetTargets(true);
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