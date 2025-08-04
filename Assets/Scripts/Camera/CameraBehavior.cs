using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void Start()
    {
        GameObject gameObject = GameObject.FindWithTag("Player");

        if (gameObject != null)
        {
            target = gameObject.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
