using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    

    //Public Variables //
    public AudioSource objFX;
    public GameObject theobjective;
    public GameObject Objectivetrigger;
    public GameObject theText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        StartCoroutine(missionObj());

    }

    private IEnumerator missionObj()
    {
        objFX.Play();
        theText.GetComponent<Text>().text = "Inspect the zoo, feed and take photo's of the animals";
        yield return null;  
       
    }


}

