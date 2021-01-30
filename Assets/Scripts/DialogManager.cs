using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private static DialogManager _instance;

    public static DialogManager Instance()
    {
        return _instance;
    }

    private Input _input;
    private string[] currentDialog;
    private int index = 0;
    public TMP_Text dialogText;
    public GameObject dialogBox;
    private Action _action;

    private void Awake()
    {
        _instance = this;
        _input = InputController.Instance();
        _input.Dialog.Disable();
        _input.Dialog.Yes.performed += context => Yes();
        _input.Dialog.No.performed += context => No();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void openDialog(string[] dialog, Action action)
    {
        _input.Play.Disable();
        _input.Dialog.Enable();
        currentDialog = dialog;
        index = 0;
        _action = action;

        dialogBox.SetActive(true);

        showText();
    }

    private void finishDialog()
    {
        closeDialog();
        _action?.Invoke();
        _action = null;
    }

    private void closeDialog()
    {
        _input.Play.Enable();
        _input.Dialog.Disable();
        currentDialog = null;

        dialogBox.SetActive(false);
    }

    private void Yes()
    {
        if (currentDialog != null)
        {
            index++;
            showText();
        }
    }

    private void No()
    {
        closeDialog();
    }

    private void showText()
    {
        if (index >= currentDialog.Length)
        {
            finishDialog();
            return;
        }

        dialogText.text = currentDialog[index];
    }

    public void openYesNo(Action action)
    {
        openDialog(new[]
        {
            "Is this the Item you want to give?"
        }, action);
    }

    public void openItemReceived(ItemScript itemScript)
    {
        openDialog(new[] {"Thank you so much! This is exactly what i needed.", itemScript.itemName + " was found"}, null);
    }
}