using UnityEngine;
using System.Collections.Generic;

public class SwitchState : MonoBehaviour
{
    public bool IsPressed => actorsOnSwitch.Count > 0;

    private HashSet<GridActor> actorsOnSwitch = new HashSet<GridActor>();

    public System.Action onChanged;

    void OnTriggerEnter2D(Collider2D other)
    {
        GridActor actor = other.GetComponent<GridActor>();

        if (actor == null) return;
        if (!actor.canPressSwitch) return;

        actorsOnSwitch.Add(actor);
        onChanged?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GridActor actor = other.GetComponent<GridActor>();

        if (actor == null) return;

        actorsOnSwitch.Remove(actor);
        onChanged?.Invoke();
    }
}