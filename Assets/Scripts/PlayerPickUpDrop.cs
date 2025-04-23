using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
     // Start is called once before the first execution of Update after the MonoBehaviour is created

     private ObjectGrabbable objectGrabbable;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null){
            //Not carrying an object, try to grab 
            float pickUpDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                }
            }
            } 
            else
            {
                //carrying
                objectGrabbable.Drop();
                objectGrabbable = null;
            }      
        }
    }
} 
