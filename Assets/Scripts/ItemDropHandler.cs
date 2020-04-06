using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject sheep;
    private Ray ray;
    private RaycastHit hit;

    public void OnDrop (PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        // Change into calling the appropriate method for each skill
        if (Physics.Raycast (ray, out hit))
        {
            if (hit.collider.name == "sheep")
            {
                if (invPanel.name == "Slot_1")
                {
                    sheep.GetComponent<MouseOver> ().feelingLoved ();
                }
                else if (invPanel.name == "Slot_2")
                {
                    sheep.GetComponent<MouseOver> ().feelingSatisfied ();
                }
            }
            else if ( hit.collider.name == "poop(Clone)")
            {
                sheep.GetComponent<MouseOver> ().feelingClean ();
                Destroy(hit.collider.gameObject);
            }

        }

        else if (!RectTransformUtility.RectangleContainsScreenPoint (invPanel, Input.mousePosition))
        {
            Debug.Log ("Item not used");
        }

    }

}