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
            _selectables.Add(selectable);
            resetSelection();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var selectable = other.GetComponent<Interactable>();
        if (selectable != null)
        {
            selectable.OnDeselect();
            _selectables.Remove(selectable);
            resetSelection();
        }
    }

    public void Interact(PlayerController playerController)
    {
        closest()?.OnInteract(playerController);
    }

    private Interactable closest()
    {
        if (_selectables.Count == 0)
        {
            return null;
        }

        return _selectables.OrderBy(interactable => (interactable.Position() - transform.position).magnitude).First();
    }

    private void resetSelection()
    {
        _selectables.ForEach(interactable => interactable.OnDeselect());
        closest()?.OnSelect();
    }
}