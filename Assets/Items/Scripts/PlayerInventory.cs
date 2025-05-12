using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList;
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


    [Space(20)]
    [Header("Item Prefabs")]
    [SerializeField] GameObject keys_prefab;

    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[4];
    [SerializeField] Image[] inventoryBGImage = new Image[4];
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItem_gameobject;


    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };

    void Start()
    {
        itemSetActive.Add(itemType.Keys, keys_item);


        itemInstantiate.Add(itemType.Keys, keys_item);

        NewItemSelected();
    }

    void Update()
    {
        //Items pickup
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, playerReach) && Input.GetKey(pickItemKey))
        {
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                pickUpItem_gameobject.SetActive(true); 
                if (Input.GetKey(pickItemKey))
                {
                    inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type);
                    item.PickItem();
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

        //Items throw

        if(Input.GetKeyDown(throwItemKey) && inventoryList.Count > 1)
        {
            Instantiate(itemInstantiate[inventoryList[selectedItem]], position: throwItem_gameobject.transform.position, new Quaternion());
            inventoryList.RemoveAt(selectedItem);

            if(selectedItem != 0)
            {
                selectedItem -= 1;
            }
            NewItemSelected();
        }
        //UI

        for (int i = 0; i < 3; i++)
        {
            if (i < inventoryList.Count)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i]].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else 
            {
                inventorySlotImage[i].sprite = emptySlotSprite;
            }
        }

        int a = 0;
        foreach(Image image in inventoryBGImage)
        {
            if(a == selectedItem)
            {
                image.color = new Color32(145, 254, 126, 255);
            }
            else
            {
                image.color = new Color32(219, 219, 219, 255);
            }
            a++;
        }




        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
    }

    private void NewItemSelected()
    {
        keys_item.SetActive(false);

        GameObject selectedItemGameobject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameobject.SetActive(true);
    } 
}

public interface IPickable
{
    void PickItem(); 
}
