using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Tilemap wallTilemap;
    public Grid grid;

    void Awake()
    {
        Instance = this;
    }

    public bool IsWall(Vector3Int cell)
    {
        return wallTilemap.HasTile(cell);
    }

    public Vector3 GetWorld(Vector3Int cell)
    {
        return grid.GetCellCenterWorld(cell);
    }

    public Vector3Int GetCell(Vector3 pos)
    {
        return grid.WorldToCell(pos);
    }
}