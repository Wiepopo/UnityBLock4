using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectivetest2 : MonoBehaviour
{
    [SerializeField] string missionObjectiveText = "Test message";

    //Public Variables //
    public AudioSource objFX;
    public GameObject theobjective;
    public GameObject Objectivetrigger;
    public GameObject theText;

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        StartCoroutine(missionObj());
        Debug.Log("it triggers");

    }

    private IEnumerator missionObj()
    {
        objFX.Play();
        theText.SetActive(true);
        theText.GetComponent<Text>().text = missionObjectiveText;
        yield return null;
       
    }


}
