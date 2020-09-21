using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private InputManager input;
    [SerializeField] private Transform cam = null;
    [SerializeField] private float mouseSensitive = 100f;
    private float xRotation;

    private void Start() {
        input = GetComponent<InputManager>();

        Cursor.lockState = CursorLockMode.Locked;

        input.mouseX *= mouseSensitive;
        input.mouseY *= mouseSensitive;
    }

    private void LateUpdate() => Controller();

    private void Controller() {
        xRotation -= input.mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * input.mouseX);
    }
}