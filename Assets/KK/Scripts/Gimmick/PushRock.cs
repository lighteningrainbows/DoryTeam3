using UnityEngine;
using System.Collections;

public class PushRock : MonoBehaviour
{
    public float moveTime = 0.12f;

    private bool isMoving;

    public bool IsMoving => isMoving;

    public bool TryPush(Vector3Int dir)
    {
        if (isMoving) return false;

        Vector3Int currentCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int targetCell =
            currentCell + dir;

        // ‰Ÿ‚·æ‚É•Ê‚ÌŠâ‚ª‚ ‚é‚È‚ç‰Ÿ‚¹‚È‚¢
        PushRock otherRock = FindRockAt(targetCell);

        if (otherRock != null)
            return false;

        // •Ç‚âŠJ‚¢‚Ä‚¢‚éŒŠ
        if (GridManager.Instance.IsBlocked(targetCell))
        {
            // ‚½‚¾‚µŠJ‚¢‚Ä‚¢‚éŒŠ‚È‚ç—‚Æ‚¹‚é
            Hole hole = FindHoleAt(targetCell);

            if (hole != null && !hole.IsFilled)
            {
                StartCoroutine(FallIntoHole(hole, targetCell));
                return true;
            }

            return false;
        }

        StartCoroutine(MoveRoutine(targetCell));
        return true;
    }

    IEnumerator MoveRoutine(Vector3Int targetCell)
    {
        isMoving = true;

        Vector3 start = transform.position;
        Vector3 end = GridManager.Instance.GetWorld(targetCell);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(start, end, t / moveTime);

            yield return null;
        }

        transform.position = end;

        isMoving = false;
    }

    IEnumerator FallIntoHole(Hole hole, Vector3Int holeCell)
    {
        isMoving = true;

        Vector3 start = transform.position;
        Vector3 end = GridManager.Instance.GetWorld(holeCell);

        float t = 0;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(start, end, t / moveTime);

            yield return null;
        }

        transform.position = end;

        hole.Fill();

        gameObject.SetActive(false);
    }

    Hole FindHoleAt(Vector3Int cell)
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                GridManager.Instance.GetWorld(cell),
                0.3f
            );

        foreach (Collider2D hit in hits)
        {
            Hole hole = hit.GetComponent<Hole>();

            if (hole != null)
                return hole;
        }

        return null;
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

            if (rock != null && rock != this)
                return rock;
        }

        return null;
    }
}