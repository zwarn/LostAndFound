using System.Collections.Generic;
using System.Linq;
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
        _selectables.OrderBy(interactable => (interactable.Position() - transform.position).magnitude).First()
            .OnInteract(playerController);
    }
}