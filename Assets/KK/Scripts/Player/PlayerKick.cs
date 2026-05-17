using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    public LayerMask kickLayer;
    public float kickRadius = 0.35f;

    PlayerGridMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerGridMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Kick();
        }
    }

    void Kick()
    {
        Vector2Int face = movement.LastMoveDir;

        Vector3Int playerCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int targetCell =
            playerCell + new Vector3Int(face.x, face.y, 0);

        Vector3 worldPos =
            GridManager.Instance.GetWorld(targetCell);

        Collider2D hit =
            Physics2D.OverlapCircle(worldPos, kickRadius, kickLayer);

        if (hit == null)
            return;

        JumpPad jumpPad = hit.GetComponent<JumpPad>();
        if (jumpPad != null)
        {
            jumpPad.KickByPlayer(transform);
            return;
        }

        TimerButton timerButton = hit.GetComponent<TimerButton>();
        if (timerButton != null)
        {
            timerButton.Press();
            return;
        }

        ToggleButton toggleButton = hit.GetComponent<ToggleButton>();
        if (toggleButton != null)
        {
            toggleButton.Press();
            return;
        }

        CompanionRobot robot = hit.GetComponent<CompanionRobot>();
        if (robot != null)
        {
            Vector3Int robotCell =
                GridManager.Instance.GetCell(robot.transform.position);

            Vector3Int kickDir = robotCell - playerCell;

            kickDir.x = Mathf.Clamp(kickDir.x, -1, 1);
            kickDir.y = Mathf.Clamp(kickDir.y, -1, 1);
            kickDir.z = 0;

            robot.Kick(kickDir);
            return;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (GridManager.Instance == null) return;
        if (movement == null) return;

        Vector2Int face = movement.LastMoveDir;

        Vector3Int playerCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int targetCell =
            playerCell + new Vector3Int(face.x, face.y, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            GridManager.Instance.GetWorld(targetCell),
            kickRadius
        );
    }
}