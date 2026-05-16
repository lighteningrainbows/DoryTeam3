using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : GimmickReceiver
{
    public Tilemap wallTilemap;
    public Vector3Int doorCell;

    public TileBase closedTile;
    public GameObject visual;

    public bool startsOpen = false;

    bool isOpen;

    void Start()
    {
        // Doorオブジェクトの位置から自動でセル取得
        doorCell = wallTilemap.WorldToCell(transform.position);

        SetOpen(startsOpen);
    }

    public override void Activate()
    {
        SetOpen(!startsOpen);
    }

    public override void Deactivate()
    {
        SetOpen(startsOpen);
    }

    void SetOpen(bool open)
    {
        isOpen = open;

        if (isOpen)
        {
            wallTilemap.SetTile(doorCell, null);
        }
        else
        {
            wallTilemap.SetTile(doorCell, closedTile);
        }

        if (visual != null)
        {
            visual.SetActive(isOpen);
        }
    }
}