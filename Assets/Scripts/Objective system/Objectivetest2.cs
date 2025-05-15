using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectivetest2 : MonoBehaviour
{
    public AudioSource objFX;
    public GameObject theobjective;
    public GameObject Objectivetrigger;
    public GameObject theText;

    private bool completed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !completed)
        {
            StartCoroutine(missionObj("Get to the other side of the zoo"));
        }
    }

    public void CompleteObjective(string message)
    {
        if (!completed)
        {
            StartCoroutine(missionObj(message));
        }
    }

    private IEnumerator missionObj(string message)
    {
        completed = true;
        objFX.Play();
        theText.GetComponent<Text>().text = "Objective Completed: " + message;
        theText.GetComponent<Text>().color = Color.green;

        yield return new WaitForSeconds(2f);

        theText.SetActive(false);
        if (Objectivetrigger != null) Objectivetrigger.SetActive(false);
        if (theobjective != null) theobjective.SetActive(false);
    }
}
