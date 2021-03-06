﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, HasItem
{
    private Input _input;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private GameObject selector;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    private SelectorScript _selectorScript;
    private Vector2 _viewDirection;
    private float _selectorDistance;
    private GameObject _carriedItem;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;

    public string[] introDialog =
    {
        "",
        "You are Nemo, a friendly Ghost learning under your mentor Karen to take care of the \"Lost and Found\".",
        "A place for the living to find what they lost and share what they have found.",
        "Hear there stories, find out what they are missing and accept what they give you."
    };

    void Awake()
    {
        _input = InputController.Instance();
        _input.Play.Interact.performed += context => Interact();
        _selectorScript = selector.GetComponent<SelectorScript>();
        _particleSystem = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _selectorDistance = selector.transform.localPosition.magnitude;
        DialogManager.Instance().openDialog(introDialog, null);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = _input.Play.Movement.ReadValue<Vector2>();
        transform.Translate(movementDirection * (movementSpeed * Time.deltaTime));
        updateView(movementDirection);
    }

    private void updateView(Vector2 moveDirection)
    {
        if (moveDirection.magnitude < 0.01)
        {
            return;
        }

        if (Math.Abs(moveDirection.x) > Math.Abs(moveDirection.y))
        {
            _viewDirection = moveDirection.x < 0 ? Vector2.left : Vector2.right;
        }
        else
        {
            _viewDirection = moveDirection.y < 0 ? Vector2.down : Vector2.up;
        }

        setAnimation(selectSprite());

        selector.transform.localPosition = _viewDirection * new Vector2(_selectorDistance, _selectorDistance);
    }

    private Sprite selectSprite()
    {
        if (_viewDirection == Vector2.left)
        {
            return left;
        }

        if (_viewDirection == Vector2.right)
        {
            return right;
        }

        if (_viewDirection == Vector2.up)
        {
            return up;
        }

        if (_viewDirection == Vector2.down)
        {
            return down;
        }

        return left;
    }

    private void setAnimation(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _particleSystem.textureSheetAnimation.SetSprite(0, sprite);
    }

    void Interact()
    {
        _selectorScript.Interact(this);
    }

    public bool hasItem()
    {
        return _carriedItem != null;
    }

    public GameObject takeItem()
    {
        if (!hasItem())
        {
            Debug.LogError("Player tried to give Item while not having any");
            return null;
        }

        GameObject item = _carriedItem;
        _carriedItem = null;
        return item;
    }

    public GameObject peekItem()
    {
        if (!hasItem())
        {
            Debug.LogError("Wanted to peek at the player item while not having any");
            return null;
        }

        return _carriedItem;
    }

    public void putItem(GameObject item)
    {
        if (hasItem())
        {
            Debug.LogError("Player tried to take Item while already holding an Item");
            return;
        }

        _carriedItem = item;
        _carriedItem.transform.parent = selector.transform;
        _carriedItem.transform.localPosition = Vector3.zero;
    }
}