using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public itemType?[] inventoryList = new itemType?[4];
    public int selectedItem;
    public float playerReach;

    [SerializeField] GameObject throwItem_gameobject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("Item gameobjects")]
    [SerializeField] GameObject keys_item;
    [SerializeField] GameObject capsule_item;

    [Space(20)]
    [Header("Item Prefabs")]
    [SerializeField] GameObject keys_prefab;
    [SerializeField] GameObject capsule_prefab;

    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[4];
    [SerializeField] Image[] inventoryBGImage = new Image[4];
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItem_gameobject;

    [Header("PhotoCamera")]
    [SerializeField] GameObject cameraController;
    [SerializeField] GameObject cameraCanvas;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>();
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>();

    void Start()
    {
        itemSetActive.Add(itemType.Keys, keys_item);
        itemSetActive.Add(itemType.Capsule, capsule_item);

        itemInstantiate.Add(itemType.Keys, keys_prefab);
        itemInstantiate.Add(itemType.Capsule, capsule_prefab);

        NewItemSelected();
    }

    void Update()
    {
        // Items pickup
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, playerReach) && Input.GetKey(pickItemKey))
        {
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                pickUpItem_gameobject.SetActive(true);

                itemType newItem = hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type;
                bool placed = false;
                int[] allowedSlots = { 0, 2, 3 };

                foreach (int i in allowedSlots)
                {
                    if (inventoryList[i] == null)
                    {
                        inventoryList[i] = newItem;
                        placed = true;
                        item.PickItem();
                        break;
                    }
                }

                if (!placed)
                {
                    Debug.Log("Inventory full or slot 1 blocked.");
                }
            }
            else
            {
                pickUpItem_gameobject.SetActive(false);
            }
        }
        else
        {
            pickUpItem_gameobject.SetActive(false);
        }

        // Items throw
        if (Input.GetKeyDown(throwItemKey) && selectedItem != 1 && inventoryList[selectedItem].HasValue)
        {
            Instantiate(itemInstantiate[inventoryList[selectedItem].Value], position: throwItem_gameobject.transform.position, rotation: Quaternion.identity);
            inventoryList[selectedItem] = null;

            // Automatically select next available item
            for (int i = selectedItem - 1; i >= 0; i--)
            {
                if (inventoryList[i].HasValue && i != 1)
                {
                    selectedItem = i;
                    break;
                }
            }
            NewItemSelected();
        }

        // UI update
        for (int i = 0; i < 4; i++)
        {

            if (i == 1)
                continue; // skip second slot completely
            if (inventoryList[i].HasValue)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i].Value].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {

                inventorySlotImage[i].sprite = emptySlotSprite;
            }
        }

        for (int i = 0; i < inventoryBGImage.Length; i++)
        {
            inventoryBGImage[i].color = (i == selectedItem)
                ? new Color32(145, 254, 126, 255)
                : new Color32(219, 219, 219, 255);
        }

        // Inventory slot selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedItem = 1;
            NewItemSelected();
            cameraCanvas.SetActive(true);
            cameraController.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedItem = 2;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedItem = 3;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
        }
    }

    private void NewItemSelected()
    {
        keys_item.SetActive(false);
        capsule_item.SetActive(false);

        if (selectedItem < 0 || selectedItem >= inventoryList.Length || !inventoryList[selectedItem].HasValue)
        {
            return;
        }

        GameObject selectedItemGameobject = itemSetActive[inventoryList[selectedItem].Value];
        selectedItemGameobject.SetActive(true);
    }
}

public interface IPickable
{
    void PickItem();
}