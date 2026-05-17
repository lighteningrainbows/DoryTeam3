using UnityEngine;
using System.Collections.Generic;

public class PlayerTrail : MonoBehaviour
{
    public static PlayerTrail Instance;

    Queue<Vector3Int> trail =
        new Queue<Vector3Int>();

    void Awake()
    {
        Instance = this;
    }

    public void Record(Vector3Int cell)
    {
        trail.Enqueue(cell);
    }

    public bool HasTrail()
    {
        return trail.Count > 0;
    }

    public Vector3Int GetNext()
    {
        return trail.Dequeue();
    }
}