using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public float normalSize = 8f;

    public float zoomSize = 4f;

    public float moveSpeed = 10f;

    Camera cam;

    bool zooming;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        zooming =
            Input.GetKey(KeyCode.Z);

        float targetSize =
            zooming ? zoomSize : normalSize;

        cam.orthographicSize =
            Mathf.Lerp(
                cam.orthographicSize,
                targetSize,
                Time.deltaTime * moveSpeed);
    }

    void LateUpdate()
    {
        Vector3 targetPos;

        if (zooming)
        {
            targetPos = player.position;
        }
        else
        {
            targetPos = Vector3.zero;
        }

        targetPos.z = -10;

        transform.position =
            Vector3.Lerp(
                transform.position,
                targetPos,
                Time.deltaTime * moveSpeed);
    }
}