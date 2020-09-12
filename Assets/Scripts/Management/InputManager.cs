using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Character movement")]
    public float horizontal;
    public float vertical;
    public bool jump;

    [Header("Mouse movement")]
    public float mouseX;
    public float mouseY;

    [Header("Inventoy")]
    public bool openInventory;
    public bool saveInInventoy;

    [Header("Shoot")]
    public bool fire;
    public bool aim;
    public bool reload;


    [Header("Other inputs")]
    public bool interact;
    public bool throwItem;    

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump = Input.GetKeyDown(KeyCode.V);

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        openInventory = Input.GetKeyDown(KeyCode.C);
        saveInInventoy = Input.GetKeyDown(KeyCode.F);

        fire = Input.GetMouseButton(0);
        aim = Input.GetMouseButton(1);
        reload = Input.GetKeyDown(KeyCode.R);

        interact = Input.GetKeyDown(KeyCode.E);
        throwItem = Input.GetKeyDown(KeyCode.Q);
    }
}