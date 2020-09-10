using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform objectZone = null;
    [SerializeField] private float throwForce = 4f;
    [SerializeField] private Camera cam;

    private bool DropOrThrow => input.interact || input.throwItem && Item.inUse;
    private bool Grab => input.interact && Item.inUse == false; 

    void Start()
    {
        input = GetComponent<InputManager>();
        inventory = GetComponent<Inventory>();
        cam = GetComponent<Transform>().GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        InteractWithObject();
    }

    private void InteractWithObject()
    {
        Ray ray = cam.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Item.efectiveRadius))
        {
            Item getItem = hit.collider.GetComponent<Item>();
            if (getItem != null)
            {
                if (Grab)
                    GrabItem(getItem);
                else if (DropOrThrow)
                    DropItem(getItem);
                
                if (input.saveInInventoy)
                    inventory.AddToInventory(getItem);
            }
        }
    }

    private void GrabItem(Item item)
    {
        Item.inUse = true;

        item.rb.useGravity = false;
        item.rb.isKinematic = true;
        item.rb.velocity = Vector3.zero;
        item.rb.angularVelocity = Vector3.zero;

        item.transform.position = objectZone.transform.position;
        item.transform.SetParent(objectZone);
    }

    private void DropItem(Item item)
    {
        Item.inUse = false;

        item.rb.useGravity = true;
        item.rb.isKinematic = false;

        if (input.throwItem)
            item.rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        item.transform.SetParent(null);
        item = null;
    }
}