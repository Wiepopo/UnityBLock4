using UnityEngine;

public class DestroyOnCollisiontest : MonoBehaviour
{
    public GameObject theText;

    [SerializeField] GameObject emptyBowl1;
    [SerializeField] GameObject fullBowl1;
    [SerializeField] GameObject emptyBowl2;
    [SerializeField] GameObject fullBowl2;
    [SerializeField] GameObject emptyBowl3;
    [SerializeField] GameObject fullBowl3;

    [SerializeField] private SpecialSubtitleController specialSubtitleController;

    private bool hasTriggeredSpecial = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Feedable>(out var feedable))
        {
            GameObject hitBowl = other.gameObject;

            if (hitBowl == emptyBowl1)
            {
                emptyBowl1.SetActive(false);
                fullBowl1.SetActive(true);
            }
            else if (hitBowl == emptyBowl2)
            {
                emptyBowl2.SetActive(false);
                fullBowl2.SetActive(true);
            }
            else if (hitBowl == emptyBowl3)
            {
                emptyBowl3.SetActive(false);
                fullBowl3.SetActive(true);
            }
            else
            {
                return;
            }

            Destroy(gameObject);

            if (GameManager.Instance == null)
                return;

            if (GameManager.Instance.Interactions < GameManager.Instance.MaxInteractions)
            {
                GameManager.Instance.Interactions++;

                if (GameManager.Instance.Interactions == GameManager.Instance.MaxInteractions)
                {
                    theText.GetComponent<UnityEngine.UI.Text>().text = "Take photo's for evidence";
                }
            }
        }

        if (!hasTriggeredSpecial &&
            fullBowl1.activeSelf && fullBowl2.activeSelf && fullBowl3.activeSelf)
        {
            hasTriggeredSpecial = true;
            specialSubtitleController.PlaySpecialSubtitle("These monkeys look kind of weird let me take some photo's as evidence");
        }
    }
}
