using UnityEngine;
using System.Collections.Generic;

public class FloorSwitch : MonoBehaviour
{
    public GimmickReceiver[] targets;

    private HashSet<GridActor> actorsOnSwitch = new HashSet<GridActor>();

    void OnTriggerEnter2D(Collider2D other)
    {
        print("PUSH");
        GridActor actor = other.GetComponent<GridActor>();

        if (actor == null) return;
        if (!actor.canPressSwitch) return;

        actorsOnSwitch.Add(actor);
        UpdateSwitch();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GridActor actor = other.GetComponent<GridActor>();

        if (actor == null) return;

        actorsOnSwitch.Remove(actor);
        UpdateSwitch();
    }

    void UpdateSwitch()
    {
        bool isPressed = actorsOnSwitch.Count > 0;

        foreach (GimmickReceiver target in targets)
        {
            if (target == null) continue;

            if (isPressed)
                target.Activate();
            else
                target.Deactivate();
        }
    }
}