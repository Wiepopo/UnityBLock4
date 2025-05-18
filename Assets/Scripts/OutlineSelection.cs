using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlightedGroup;
    private List<Outline> currentOutlines = new List<Outline>();

    // You can tweak this to control the outline thickness
    private float outlineWidth = 15f;
    private Color outlineColor = Color.red;

    void Update()
    {
        // Disable previous outlines
        if (highlightedGroup != null)
        {
            foreach (var outline in currentOutlines)
            {
                if (outline != null)
                    outline.enabled = false;
            }
            currentOutlines.Clear();
            highlightedGroup = null;
        }

        // Raycast from mouse to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform target = hit.transform;

            // Look for a parent with tag "Selectable"
            Transform group = target.CompareTag("Selectable") ? target : target.GetComponentInParent<Transform>();
            if (group != null && group.CompareTag("Selectable"))
            {
                Renderer[] renderers = group.GetComponentsInChildren<Renderer>();
                foreach (Renderer rend in renderers)
                {
                    Outline outline = rend.GetComponent<Outline>();
                    if (outline == null)
                    {
                        outline = rend.gameObject.AddComponent<Outline>();
                        outline.OutlineMode = Outline.Mode.OutlineAll;
                        outline.OutlineColor = outlineColor;
                        outline.OutlineWidth = outlineWidth;
                    }

                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = outlineColor;
                    outline.OutlineWidth = outlineWidth;
                    outline.enabled = true;

                    currentOutlines.Add(outline);
                }

                highlightedGroup = group;
            }
        }
    }
}
