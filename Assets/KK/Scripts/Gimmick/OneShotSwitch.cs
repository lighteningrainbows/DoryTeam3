using UnityEngine;

public class OneShotSwitch : MonoBehaviour
{
    public GimmickReceiver[] targets;

    bool isPressed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPressed) return;

        GridActor actor =
            other.GetComponent<GridActor>();

        if (actor == null) return;
        if (!actor.canPressSwitch) return;

        isPressed = true;

        foreach (GimmickReceiver target in targets)
        {
            if (target == null) continue;

            target.Activate();
        }

        AudioManager.Instance.PlaySE("SwitchSE");
    }
}