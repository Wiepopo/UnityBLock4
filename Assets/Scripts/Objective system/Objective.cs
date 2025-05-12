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
        theText.GetComponent<Text>().text = "Get to the other side of the zoo";
        yield return null;
       
    }


}

