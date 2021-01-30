using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerScript : MonoBehaviour, Interactable, HasItem
{
    public GameObject carriedItem;
    public GameObject itemCarry;

    public void OnSelect()
    {
        changeColor(Color.yellow);
        showItemName();
    }

    public void OnDeselect()
    {
        changeColor(Color.white);
        hideItemName();
    }

    private void changeColor(Color color)
    {
        if (carriedItem == null)
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            carriedItem.GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void showItemName()
    {
        if (carriedItem != null)
        {
            UiScript.Instance().showItemName(carriedItem.GetComponent<ItemScript>());
        }
    }

    private void hideItemName()
    {
        UiScript.Instance().showItemName(null);
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

        GameObject item = carriedItem;
        carriedItem = null;
        item.GetComponent<SpriteRenderer>().color = Color.white;
        changeColor(Color.yellow);
        return item;
    }

    public void putItem(GameObject item)
    {
        if (hasItem())
        {
            Debug.LogError("Player tried to take Item while already holding an Item");
            return;
        }

        carriedItem = item;
        carriedItem.transform.parent = itemCarry.transform;
        carriedItem.transform.localPosition = Vector3.zero;
        GetComponent<SpriteRenderer>().color = Color.white;
        changeColor(Color.yellow);
    }
}