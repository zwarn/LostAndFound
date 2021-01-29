using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, Interactable
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSelect()
    {
        _spriteRenderer.color = Color.yellow;
    }

    public void OnDeselect()
    {
        _spriteRenderer.color = Color.white;
    }

    public void OnInteract(PlayerController playerController)
    {
        playerController.GrabItem(this);
    }
}