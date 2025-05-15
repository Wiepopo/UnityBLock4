using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PhotoSave : MonoBehaviour
{
    GameObject obj;
    Collider objCollider;
    Camera viewCam;
    Plane[] planes;


    void Start()
    {
        viewCam = Camera.main;
        //Calculate the planes visible for the main camera to capture what the camera can possibly see
        planes = GeometryUtility.CalculateFrustumPlanes(viewCam);

        //Creating a GameObject for each of the calculated planes 0 through 6
        //Ordering: [0] = Left, [1] = Right, [2] = Down, [3] = Up, [4] = Near, [5] = Far
        for (int i = 0; i < 6; i++)
        {
            //Creating a GameObject plane in the catagory viewPlanes
            GameObject viewPlanes = GameObject.CreatePrimitive(PrimitiveType.Plane);
            viewPlanes.name = "viewPlane " + i.ToString(); //Naming the planes according to their number (i)
            viewPlanes.transform.position = -planes[i].normal * planes[i].distance; //Setting the planes position according to their number
            viewPlanes.transform.rotation = Quaternion.FromToRotation(Vector3.up, planes[i].normal); //Setting the rotation according to their number
            //Quaternion.FromToRotation makes it so the planes can only rotate in one Quaternion which means only on one axis
            obj = this.gameObject;
            objCollider = GetComponent<Collider>();

        }
    }

    void Update() 
    {
        planes = GeometryUtility.CalculateFrustumPlanes(viewCam);

        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
        {
            Debug.Log(obj.name + " has been detected");
        }
        else
        {
            Debug.Log("nothing has been detected");
        }
    }
    
}
