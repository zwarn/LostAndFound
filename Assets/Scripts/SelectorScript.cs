using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    private List<Interactable> _selectables = new List<Interactable>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        var selectable = other.GetComponent<Interactable>();
        if (selectable != null)
        {
            selectable.OnSelect();
            _selectables.Add(selectable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var selectable = other.GetComponent<Interactable>();
        if (selectable != null)
        {
            selectable.OnDeselect();
            _selectables.Remove(selectable);
        }
    }

    public void Interact(PlayerController playerController)
    {
        //TODO: this will interact with all selected things
        _selectables.ForEach(interactable => interactable.OnInteract(playerController));
    }
}