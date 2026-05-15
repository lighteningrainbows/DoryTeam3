using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    PlayerGridMovement movement;

    void Start()
    {
        movement =
            GetComponent<PlayerGridMovement>();
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
        Vector3Int playerCell =
            GridManager.Instance.GetCell(transform.position);

        Vector2Int face =
            movement.LastMoveDir;

        Vector3Int targetCell =
            playerCell - new Vector3Int(face.x, face.y, 0);

        Vector3 worldPos =
            GridManager.Instance.GetWorld(targetCell);

        Collider2D hit = Physics2D.OverlapCircle(
            worldPos,
            0.3f
        );

        if (hit == null)
            return;

        CompanionRobot robot =
            hit.GetComponent<CompanionRobot>();

        if (robot != null)
        {
            Vector3Int robotCell =
                GridManager.Instance.GetCell(robot.transform.position);

            Vector3Int kickDir =
                robotCell - playerCell;

            kickDir.x = Mathf.Clamp(kickDir.x, -1, 1);
            kickDir.y = Mathf.Clamp(kickDir.y, -1, 1);
            kickDir.z = 0;

            robot.Kick(kickDir);
        }
    }
}