using UnityEngine;

public class CameraFollowBottom : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float bottomPadding = 1.5f;
    [SerializeField] private float followSpeed = 100f;
    [SerializeField] private bool neverLagBehind = true;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null || cam == null) return;

        float desiredCamY = target.position.y + cam.orthographicSize - bottomPadding;

        Vector3 pos = transform.position;

        if (neverLagBehind && desiredCamY > pos.y)
        {
            pos.y = desiredCamY;
        }
        else
        {
            pos.y = Mathf.MoveTowards(pos.y, desiredCamY, followSpeed * Time.deltaTime);
        }

        transform.position = pos;
    }
}