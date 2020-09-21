using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField] private InputManager input;
    [SerializeField] private GameObject[] slots = null;
    [SerializeField] private bool[] isFull = null;
    [SerializeField] private GameObject inventoryPanel = null;
    [SerializeField] private bool isOpen;

    private void Start() {
        input = GetComponent<InputManager>();
        inventoryPanel.SetActive(false);
    }
    private void Update() => OpenInventoy();

    private void OpenInventoy() {
        if (input.openInventory && isOpen == false) {
            inventoryPanel.SetActive(true);
            isOpen = true;
        } else if (input.openInventory && isOpen) {
            inventoryPanel.SetActive(false);
            isOpen = false;
        }
    }

    public void AddToInventory(Item item) {
        for (int slot = 0; slot < slots.Length; slot++) {
            if (isFull[slot] == false) {
                isFull[slot] = true;
                UpdateInventory(slot);
                Destroy(item.gameObject);
                break;
            }
        }
    }

    private void UpdateInventory(int slot) {
        Image slotImage = slots[slot].GetComponent<Image>();
        slotImage.color = Color.green;
    }
}