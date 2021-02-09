using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenScript : MonoBehaviour, Interactable
{
    public string npcName;
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
        showName();
    }

    public void OnDeselect()
    {
        _spriteRenderer.material.SetFloat(Shader.PropertyToID("_OutlineEnabled"), 0);
        hideName();
    }

    private void showName()
    {
        UiScript.Instance().showItemName(getName(), transform.position);
    }

    private void hideName()
    {
        UiScript.Instance().showItemName(null, transform.position);
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

    public string getName()
    {
        return npcName ?? "Karen";
    }
}