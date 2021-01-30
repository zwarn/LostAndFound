using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    public string[] dialog;
    public string[] dialogForIncorrectItem;
    //public string[] dialogForCorrectItem;
    public GameObject holdsItem;
    public GameObject searchedItem;
    private Vector2 direction = Vector2.zero;
    private SpriteRenderer _spriteRenderer;
    public float fadeSpeed = 5;
    public float walkSpeed = 2;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSelect()
    {
        GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), 1);
    }

    public void OnDeselect()
    {
        GetComponent<SpriteRenderer>().material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), 0);
    }

    public void OnInteract(PlayerController playerController)
    {
        if (playerController.hasItem())
        {
            DialogManager.Instance().openYesNo(() => OnReceiveItem(playerController));
        }
        else
        {
            DialogManager.Instance().openDialog(dialog, null);
        }
    }

    private void OnReceiveItem(PlayerController playerController)
    {
        if (playerController.peekItem() != searchedItem)
        {
            DialogManager.Instance().openDialog(dialogForIncorrectItem, null);
            Debug.LogError("Player wanted to give the wrong item");
            return;
        }
        GameObject item = playerController.takeItem();
        playerController.putItem(holdsItem);
        //DialogManager.Instance().openDialog(dialogForCorrectItem, null);
        DialogManager.Instance().openItemReceived(holdsItem.GetComponent<ItemScript>());
        //TODO: NPC holds item
        Destroy(item);
        direction = Vector2.down;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * (Time.deltaTime * walkSpeed));
            var color = _spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            _spriteRenderer.color = color;
            if (color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }
}