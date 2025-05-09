using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;

    void Update()
    {
        // Remove previous highlight
        if (highlight != null)
        {
            var oldOutline = highlight.GetComponent<Outline>();
            if (oldOutline != null)
                oldOutline.enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            Transform candidate = raycastHit.transform;
            if (candidate.CompareTag("Selectable"))
            {
                Outline outline = candidate.GetComponent<Outline>();
                if (outline == null)
                {
                    outline = candidate.gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.magenta;
                    outline.OutlineWidth = 7.0f;
                }

                outline.enabled = true;
                highlight = candidate;
            }
        }
    }
}
