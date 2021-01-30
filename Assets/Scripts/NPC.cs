using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    public string[] dialog;

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
            //TODO: give Item
        }
        else
        {
            DialogManager.Instance().openDialog(dialog);
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }
}