using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float limitX = 2.5f;

    private Camera mainCam;
    private Vector3 touchStartWorld;
    private Vector3 playerStartPos;
    private bool isTouching;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        HandleKeyboard();
        HandleTouch();
    }

    // ===== KEYBOARD (A / D) =====
    void HandleKeyboard()
    {
        if (Keyboard.current == null) return;

        float horizontal = 0f;

        if (Keyboard.current.aKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        if (Mathf.Abs(horizontal) > 0.01f)
        {
            float newX = transform.position.x + horizontal * moveSpeed * Time.deltaTime;
            newX = Mathf.Clamp(newX, -limitX, limitX);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    // ===== TOUCH DRAG =====
    void HandleTouch()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            isTouching = true;
            touchStartWorld = mainCam.ScreenToWorldPoint(
                new Vector3(touch.position.ReadValue().x,
                            touch.position.ReadValue().y,
                            Mathf.Abs(mainCam.transform.position.z)));

            playerStartPos = transform.position;
        }

        if (touch.press.isPressed && isTouching)
        {
            Vector2 currentPos = touch.position.ReadValue();

            Vector3 currentWorld = mainCam.ScreenToWorldPoint(
                new Vector3(currentPos.x,
                            currentPos.y,
                            Mathf.Abs(mainCam.transform.position.z)));

            float deltaX = currentWorld.x - touchStartWorld.x;
            float newX = playerStartPos.x + deltaX;

            newX = Mathf.Clamp(newX, -limitX, limitX);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        if (touch.press.wasReleasedThisFrame)
        {
            isTouching = false;
        }
    }
}