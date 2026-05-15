using UnityEngine;
using System.Collections;

public class PlayerGridMovement : MonoBehaviour
{
    public CompanionRobot robot;

    public float moveTime = 0.15f;

    bool isMoving;

    Vector2Int facing = Vector2Int.down;

    public Vector2Int LastMoveDir => facing;

    void Start()
    {
        Vector3Int startCell =
            GridManager.Instance.GetCell(transform.position);

        PlayerTrail.Instance.Record(startCell);
    }

    void Update()
    {
        if (isMoving) return;

        Vector3Int dir = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector3Int.up;
            facing = Vector2Int.up;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector3Int.down;
            facing = Vector2Int.down;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector3Int.left;
            facing = Vector2Int.left;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector3Int.right;
            facing = Vector2Int.right;
        }

        if (dir != Vector3Int.zero)
        {
            TryMove(dir);
        }
    }

    void TryMove(Vector3Int dir)
    {
        Vector3Int currentCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int targetCell =
            currentCell + dir;

        if (GridManager.Instance.IsWall(targetCell))
            return;

        StartCoroutine(MoveRoutine(targetCell));
    }

    IEnumerator MoveRoutine(
        Vector3Int targetCell)
    {
        isMoving = true;

        Vector3Int previousCell =
            GridManager.Instance
            .GetCell(transform.position);

        Vector3 start =
            transform.position;

        Vector3 end =
            GridManager.Instance
            .GetWorld(targetCell);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(
                    start,
                    end,
                    t / moveTime);

            yield return null;
        }

        transform.position = end;

        robot.MoveToBehind(previousCell);

        isMoving = false;
    }
}