using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void OnSelect();

    void OnDeselect();

    void OnInteract(PlayerController playerController);

    Vector3 Position();
}