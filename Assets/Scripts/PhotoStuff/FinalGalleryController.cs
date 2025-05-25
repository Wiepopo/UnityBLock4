using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FinalGalleryController : MonoBehaviour
{
    [SerializeField] private GameObject finalGalleryPanel;
    [SerializeField] private GameObject screenshotCardPrefab;
    [SerializeField] private Transform contentParent; // the Content object in the ScrollView

    void OnEnable()
    {
        PopulateGallery();
    }

    void PopulateGallery()
    {
        // Clear existing content
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<Texture2D> photos = PhotoSaveToGallery.GetGallery();

        foreach (Texture2D tex in photos)
        {
            GameObject card = Instantiate(screenshotCardPrefab, contentParent);
            ScreenshotCardUI cardUI = card.GetComponent<ScreenshotCardUI>();
            if (cardUI != null)
            {
                cardUI.SetPhoto(tex);
            }
        }
    }
}
