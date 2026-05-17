using UnityEngine;

public class Hole : MonoBehaviour
{
    public bool IsFilled { get; private set; }

    private Vector3Int cell;

    [Header("Œ©‚½–Ú")]
    public GameObject openVisual;
    public GameObject filledVisual;

    void Start()
    {
        cell = GridManager.Instance.GetCell(transform.position);

        SetFilled(false);
    }

    public void Fill()
    {
        if (IsFilled) return;

        SetFilled(true);
    }

    void SetFilled(bool filled)
    {
        IsFilled = filled;

        if (filled)
        {
            GridManager.Instance.RemoveBlock(cell);
        }
        else
        {
            GridManager.Instance.AddBlock(cell);
        }

        if (openVisual != null)
            openVisual.SetActive(!filled);

        if (filledVisual != null)
            filledVisual.SetActive(filled);
    }
}