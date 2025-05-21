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
    //new
    [SerializeField] private Image[] inventoryItemImage = new Image[4];
    //---
    [SerializeField] Image[] inventoryBGImage = new Image[4];
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItem_gameobject;


    [Header("Clipboard")]
    [SerializeField] GameObject clipboard;

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

    // Make sure slot 0 is selected on start
    selectedItem = 0;

    // Show the clipboard if we're starting on slot 0
    clipboard.SetActive(true);
    cameraCanvas.SetActive(false);
    cameraController.SetActive(false);

    // Only show the held item if there is one
    
        NewItemSelected();
    
    }

    void Update()
    {
        
        // Items pickup
        // --- Cast a ray to detect a pickable item ---
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, playerReach))
        {
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                // Show the pickup prompt
                pickUpItem_gameobject.SetActive(true);

                // Handle the pickup only when E is pressed
                if (Input.GetKeyDown(pickItemKey))
                {
                    itemType newItem = hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type;
                    bool placed = false;
                    int[] allowedSlots = { 2, 3 };

                    foreach (int i in allowedSlots)
                    {
                        if (inventoryList[i] == null)
                        {
                            inventoryList[i] = newItem;
                            placed = true;
                            item.PickItem();

                            if (i == selectedItem)
                            {
                                NewItemSelected();
                            }
                            break;
                        }
                    }

                    if (!placed)
                    {
                        Debug.Log("Inventory full or slot 1 blocked.");
                    }
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

            //shifts to another slot
            // for (int i = selectedItem - 1; i >= 0; i--)
            // {
            //     if (inventoryList[i].HasValue && i != 1 || i != 0)
            //     {
            //         selectedItem = i;
            //         break;
            //     }
            // }

            NewItemSelected();
        }

        // UI update
        for (int i = 0; i < 4; i++)
        {

            if (i == 1)
                continue; // skip second slot completely

            if (i == 0)
                continue;
            if (inventoryList[i].HasValue)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i].Value].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {

                inventorySlotImage[i].sprite = emptySlotSprite;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            // always update background color — even for slot 1
            inventoryBGImage[i].color = (i == selectedItem)
                ? new Color32(145, 254, 126, 255) // green
                : new Color32(219, 219, 219, 255); // gray

            // skip dynamic item logic only for slot 1 (camera)
            if (i == 1)
                continue;

            if (i == 0)
                continue;

            // show item image only if there's an item
            if (inventoryList[i].HasValue)
            {
                var itemSprite = itemSetActive[inventoryList[i].Value]
                    .GetComponent<Item>().itemScriptableObject.item_sprite;

                inventorySlotImage[i].sprite = itemSprite;
                inventoryItemImage[i].enabled = true;
                inventoryItemImage[i].sprite = itemSprite;
            }
            else
            {
                inventorySlotImage[i].sprite = emptySlotSprite;
                inventoryItemImage[i].enabled = false;
            }
        }


        // Inventory slot selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
            clipboard.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedItem = 1;
            NewItemSelected();
            cameraCanvas.SetActive(true);
            cameraController.SetActive(true);
            clipboard.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedItem = 2;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
            clipboard.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedItem = 3;
            NewItemSelected();
            cameraCanvas.SetActive(false);
            cameraController.SetActive(false);
            clipboard.SetActive(false);
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