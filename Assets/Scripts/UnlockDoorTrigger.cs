using UnityEngine;
using System.Collections;

public class UnlockDoorTrigger : MonoBehaviour
{
    public GameObject lockedDoor;
    public GameObject unlockedDoor;

    [Header("Tooltip")]
    public GameObject tooltipObject;
    public float tooltipDuration = 2f;

    public itemType requiredItem = itemType.Keys;

    private PlayerInventory playerInventory;
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            TryUnlockDoor();
        }
    }

    public void TryUnlockDoor()
    {
        if (playerInventory == null) return;

        // check if the required key is in the inventory
        for (int i = 0; i < playerInventory.inventoryList.Length; i++)
        {
            if (playerInventory.inventoryList[i].HasValue && playerInventory.inventoryList[i].Value == requiredItem)
            {
                // remve the key from inventory
                playerInventory.inventoryList[i] = null;

                // hide the key from the players hand (if its the active item)
                if (playerInventory.itemSetActive.ContainsKey(requiredItem))
                {
                    playerInventory.itemSetActive[requiredItem].SetActive(false);
                }

                // hde locked door and show unlocked one
                lockedDoor.SetActive(false);
                unlockedDoor.SetActive(true);

                Debug.Log("Door unlocked!");
                return;
            }
        }

        Debug.Log("You need a key to open this door.");
        if (tooltipObject != null)
        {
            StartCoroutine(ShowTooltipForDuration());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            playerInventory = null;
        }
    }
    IEnumerator ShowTooltipForDuration()
    {
        tooltipObject.SetActive(true);
        yield return new WaitForSeconds(tooltipDuration);
        tooltipObject.SetActive(false);
    }

}
