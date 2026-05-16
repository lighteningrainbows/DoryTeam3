using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour
{
    public float jumpTime = 0.18f;

    bool isJumping;

    public void KickByPlayer(Transform player)
    {
        print("KICK");
        if (isJumping) return;

        Vector3Int padCell =
            GridManager.Instance.GetCell(transform.position);

        Vector3Int playerCell =
            GridManager.Instance.GetCell(player.position);

        Vector3Int dir =
            playerCell - padCell;

        dir.x = Mathf.Clamp(dir.x, -1, 1);
        dir.y = Mathf.Clamp(dir.y, -1, 1);
        dir.z = 0;

        if (dir == Vector3Int.zero)
            return;

        StartCoroutine(JumpPlayer(player, padCell, dir));
    }

    IEnumerator JumpPlayer(
        Transform player,
        Vector3Int padCell,
        Vector3Int dir)
    {
        isJumping = true;

        Vector3Int obstacleCell =
            padCell + dir;

        Vector3Int landingCell =
            padCell + dir * 3;

        Vector3Int finalCell;

        if (GridManager.Instance.IsWall(landingCell))
        {
            finalCell = obstacleCell;
        }
        else
        {
            finalCell = landingCell;
        }

        Vector3 start =
            player.position;

        Vector3 end =
            GridManager.Instance.GetWorld(finalCell);

        float t = 0;

        while (t < jumpTime)
        {
            t += Time.deltaTime;

            player.position =
                Vector3.Lerp(
                    start,
                    end,
                    t / jumpTime);

            yield return null;
        }

        player.position = end;

        isJumping = false;
    }
}