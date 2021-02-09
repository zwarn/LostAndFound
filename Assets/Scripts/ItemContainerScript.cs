using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerScript : MonoBehaviour, Interactable, HasItem
{
    public GameObject carriedItem;
    public GameObject itemCarry;

    public void OnSelect()
    {
        changeHighlight(true);
        showItemName();
    }

    public void OnDeselect()
    {
        changeHighlight(false);
        hideItemName();
    }

    private void changeHighlight(bool highlight)
    {
        float value = highlight ? 1 : 0;
        if (carriedItem == null)
        {
            GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), value);
        }
        else
        {
            carriedItem.GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), value);
        }
    }

    private void showItemName()
    {
        if (carriedItem != null)
        {
            UiScript.Instance().showItemName(getName(), transform.position);
        }
    }

    private void hideItemName()
    {
        UiScript.Instance().showItemName(null, transform.position);
    }

    public void OnInteract(PlayerController playerController)
    {
        bool playerHasItem = playerController.hasItem();
        bool containerHasItem = hasItem();

        if (playerHasItem && containerHasItem)
        {
            //Swap
            GameObject playerItem = playerController.takeItem();
            GameObject containerItem = takeItem();
            playerController.putItem(containerItem);
            putItem(playerItem);
        }

        if (playerHasItem && !containerHasItem)
        {
            //Give
            GameObject item = playerController.takeItem();
            putItem(item);
        }

        if (!playerHasItem && containerHasItem)
        {
            //Take
            GameObject item = takeItem();
            playerController.putItem(item);
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }

    public string getName()
    {
        return carriedItem?.GetComponent<ItemScript>()?.itemName ?? null;
    }

    public bool hasItem()
    {
        return carriedItem != null;
    }

    public GameObject takeItem()
    {
        if (!hasItem())
        {
            Debug.LogError("Player tried to give Item while not having any");
            return null;
        }

        changeHighlight(false);
        GameObject item = carriedItem;
        carriedItem = null;
        changeHighlight(true);
        return item;
    }

    public void putItem(GameObject item)
    {
        if (hasItem())
        {
            Debug.LogError("Player tried to take Item while already holding an Item");
            return;
        }

        changeHighlight(false);
        carriedItem = item;
        carriedItem.transform.parent = itemCarry.transform;
        carriedItem.transform.localPosition = Vector3.zero;
        changeHighlight(true);
    }
}