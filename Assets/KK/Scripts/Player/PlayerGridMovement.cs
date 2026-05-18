using UnityEngine;
using System.Collections;

public class PlayerGridMovement : MonoBehaviour
{
    public float moveTime = 0.15f;
    public CompanionRobot robot;

    bool isMoving;

    Vector2Int facing = Vector2Int.down;

    PlayerAnimation playerAnimation;

    public bool CanControl { get; set; } = true;
    public Vector2Int LastMoveDir => facing;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();

        if (playerAnimation != null)
        {
            playerAnimation.SetDirection(facing);
            playerAnimation.SetMoving(false);
        }
    }

    void Update()
    {
        if (!CanControl) return;
        if (isMoving) return;

        HandleLookInput();
        HandleMoveInput();
    }

    void HandleLookInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            SetFacing(Vector2Int.up);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            SetFacing(Vector2Int.down);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            SetFacing(Vector2Int.left);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SetFacing(Vector2Int.right);
    }

    void HandleMoveInput()
    {
        Vector3Int dir = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W))
            dir = Vector3Int.up;

        else if (Input.GetKeyDown(KeyCode.S))
            dir = Vector3Int.down;

        else if (Input.GetKeyDown(KeyCode.A))
            dir = Vector3Int.left;

        else if (Input.GetKeyDown(KeyCode.D))
            dir = Vector3Int.right;

        if (dir != Vector3Int.zero)
        {
            // ˆÚ“®‚µ‚½•ûŒü‚ðŒü‚­
            SetFacing(new Vector2Int(dir.x, dir.y));

            TryMove(dir);
        }
    }

    void TryMove(Vector3Int dir)
    {
        Vector3Int currentCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int targetCell =
            currentCell + dir;

        PushRock rock = FindRockAt(targetCell);

        if (rock != null)
        {
            print("ROCK");
            bool pushed = rock.TryPush(dir);

            if (!pushed)
                return;

            StartCoroutine(MoveRoutine(targetCell));
            return;
        }

        if (GridManager.Instance.IsBlocked(targetCell))
        {
            Debug.Log("•Ç”»’è‚Å’Ê‚ê‚È‚¢: " + targetCell);
            return;
        }

        StartCoroutine(MoveRoutine(targetCell));
    }

    PushRock FindRockAt(Vector3Int cell)
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                GridManager.Instance.GetWorld(cell),
                0.3f
            );

        foreach (Collider2D hit in hits)
        {
            PushRock rock = hit.GetComponent<PushRock>();

            if (rock != null)
                return rock;
        }

        return null;
    }

    IEnumerator MoveRoutine(Vector3Int targetCell)
    {
        isMoving = true;

        if (playerAnimation != null)
        {
            playerAnimation.SetMoving(true);
        }

        Vector3Int previousCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3 start = transform.position;

        Vector3 end =
            GridManager.Instance.GetWorld(targetCell);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(start, end, t / moveTime);

            yield return null;
        }

        transform.position = end;

        if (robot != null)
        {
            robot.MoveToBehind(previousCell);
        }

        isMoving = false;

        if (playerAnimation != null)
        {
            playerAnimation.SetMoving(false);
        }
    }

    public void ResetState()
    {
        StopAllCoroutines();
        isMoving = false;

        if (playerAnimation != null)
        {
            playerAnimation.SetMoving(false);
            playerAnimation.SetDirection(facing);
        }
    }

    void SetFacing(Vector2Int dir)
    {
        facing = dir;

        if (playerAnimation != null)
        {
            playerAnimation.SetDirection(facing);
        }
    }
}