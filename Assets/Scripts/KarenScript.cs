using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenScript : MonoBehaviour, Interactable
{
    public string[] initial_dialog;
    public string[] repeating_dialog;
    private SpriteRenderer _spriteRenderer;

    private bool isInitial = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSelect()
    {
        _spriteRenderer.material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), 1);
    }

    public void OnDeselect()
    {
        _spriteRenderer.material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), 0);
    }

    public void OnInteract(PlayerController playerController)
    {
        if (isInitial)
        {
            DialogManager.Instance().openDialog(initial_dialog, null);
            isInitial = false;
        }
        else
        {
            DialogManager.Instance().openDialog(repeating_dialog, null);
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }
}