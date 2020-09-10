using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Transform cam;
    [SerializeField] private float mouseSensitive = 100f;
    private float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponent<InputManager>();
        cam = GetComponent<Transform>().GetChild(0);

        input.mouseX *= mouseSensitive;
        input.mouseY *= mouseSensitive;    
    }

    void LateUpdate()
    {
        Controller();
    }

    private void Controller() 
    {
        xRotation -= input.mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRotation,0f, 0f);
        transform.Rotate(Vector3.up * input.mouseX);
    }
}