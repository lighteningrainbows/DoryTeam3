using UnityEngine;
using System.Collections;

public class CompanionRobot : MonoBehaviour
{
    public float moveTime = 0.12f;

    bool isMoving;

    bool isFixed;

    public void MoveToBehind(
        Vector3Int targetCell)
    {
        if (isFixed) return;

        if (isMoving) return;

        StartCoroutine(
            MoveRoutine(targetCell));
    }

    IEnumerator MoveRoutine(
        Vector3Int targetCell)
    {
        isMoving = true;

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

        isMoving = false;
    }

    public void ToggleFix()
    {
        isFixed = !isFixed;
    }

    public void Kick(Vector3Int dir)
    {
        print("p");
        if (isMoving) return;

        StartCoroutine(
            KickRoutine(dir));
    }

    IEnumerator KickRoutine(
        Vector3Int dir)
    {
        isMoving = true;

        Vector3Int current =
            GridManager.Instance
            .GetCell(transform.position);

        Vector3Int next =
            current + dir;

        while (!GridManager.Instance
            .IsWall(next))
        {
            current = next;
            next += dir;
        }

        Vector3 start =
            transform.position;

        Vector3 end =
            GridManager.Instance
            .GetWorld(current);

        float t = 0;

        while (t < 0.15f)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(
                    start,
                    end,
                    t / 0.15f);

            yield return null;
        }

        transform.position = end;

        isMoving = false;
    }

    public void ResetState()
    {
        StopAllCoroutines();
        isMoving = false;
        isFixed = false;
    }
}