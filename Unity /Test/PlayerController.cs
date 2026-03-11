using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private SpeedController speedController;

    [Header("Vertical Run")]
    [SerializeField] private float verticalSpeedMultiplier = 1f;

    [Header("Horizontal")]
    [SerializeField] private float horizontalSpeed = 6f;
    [SerializeField] private float limitX = 2.5f;

    private Camera cam;
    private bool isTouching;
    private Vector3 touchStartWorld;
    private Vector3 playerStartPos;

    private void Awake()
    {
        cam = Camera.main;
        if (speedController == null) speedController = FindObjectOfType<SpeedController>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        AutoRunUp();
        HandleKeyboard();
        HandleTouchDrag();
    }

    private void AutoRunUp()
    {
        if (speedController == null) return;

        float v = speedController.CurrentSpeed * verticalSpeedMultiplier * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y += v;
        transform.position = pos;
    }

    private void HandleKeyboard()
    {
        if (Keyboard.current == null) return;

        float dir = 0f;
        if (Keyboard.current.aKey.isPressed) dir = -1f;
        else if (Keyboard.current.dKey.isPressed) dir = 1f;

        if (Mathf.Abs(dir) > 0.01f)
        {
            float newX = transform.position.x + dir * horizontalSpeed * Time.deltaTime;
            newX = Mathf.Clamp(newX, -limitX, limitX);

            Vector3 pos = transform.position;
            pos.x = newX;
            transform.position = pos;
        }
    }

    private void HandleTouchDrag()
    {
        if (Touchscreen.current == null || cam == null) return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            isTouching = true;
            playerStartPos = transform.position;

            Vector2 p = touch.position.ReadValue();
            touchStartWorld = cam.ScreenToWorldPoint(
                new Vector3(p.x, p.y, Mathf.Abs(cam.transform.position.z))
            );
        }

        if (touch.press.isPressed && isTouching)
        {
            Vector2 p = touch.position.ReadValue();
            Vector3 currentWorld = cam.ScreenToWorldPoint(
                new Vector3(p.x, p.y, Mathf.Abs(cam.transform.position.z))
            );

            float deltaX = currentWorld.x - touchStartWorld.x;
            float newX = Mathf.Clamp(playerStartPos.x + deltaX, -limitX, limitX);

            Vector3 pos = transform.position;
            pos.x = newX;
            transform.position = pos;
        }

        if (touch.press.wasReleasedThisFrame)
        {
            isTouching = false;
        }
    }
}