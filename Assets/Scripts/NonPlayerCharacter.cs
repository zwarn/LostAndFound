using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter: MonoBehaviour, Interactable
{
    private SpriteRenderer _spriteRenderer;
    private Input _input;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private GameObject selector;
    private SelectorScript _selectorScript;
    private Vector2 _viewDirection;
    private float _selectorDistance;
    private GameObject _carriedItem;

    public void OnSelect(){
        _spriteRenderer.color = Color.yellow;
    }
    public void OnDeselect(){
        _spriteRenderer.color = Color.white;
    }
    public void OnInteract(PlayerController playerController){
        //playerController.giveItem(this); falls der Spieler dem NPC ein Item geben will
        //Textbox erzeugen, Text abspielen; falls der Spieler mit dem NPC reden will
    }
}