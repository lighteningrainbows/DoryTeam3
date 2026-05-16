using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Tilemap wallTilemap;
    public Grid grid;

    private HashSet<Vector3Int> blockedCells = new HashSet<Vector3Int>();

    void Awake()
    {
        Instance = this;
    }

    public void SetStage(StageInfo stage)
    {
        grid = stage.grid;
        wallTilemap = stage.wallTilemap;
    }

    public Vector3Int GetCell(Vector3 pos)
    {
        return grid.WorldToCell(pos);
    }

    public Vector3 GetWorld(Vector3Int cell)
    {
        return grid.GetCellCenterWorld(cell);
    }

    public bool IsWall(Vector3Int cell)
    {
        return wallTilemap != null && wallTilemap.HasTile(cell);
    }

    public bool IsBlocked(Vector3Int cell)
    {
        return IsWall(cell) || blockedCells.Contains(cell);
    }

    public void AddBlock(Vector3Int cell)
    {
        blockedCells.Add(cell);
    }

    public void RemoveBlock(Vector3Int cell)
    {
        blockedCells.Remove(cell);
    }
}