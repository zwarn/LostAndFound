using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Input _input;
    [SerializeField] private float movementSpeed = 5;

    void Awake()
    {
        _input = new Input();
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = _input.Play.Movement.ReadValue<Vector2>();
        transform.Translate(movementDirection * (movementSpeed * Time.deltaTime));
    }
}