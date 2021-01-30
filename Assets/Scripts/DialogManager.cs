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

    private void Awake()
    {
        _instance = this;
        _input = InputController.Instance();
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

    public void openDialog(string[] dialog)
    {
        _input.Play.Disable();
        _input.Dialog.Enable();
        currentDialog = dialog;
        index = 0;

        dialogBox.SetActive(true);

        showText();
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
            closeDialog();
            return;
        }

        dialogText.text = currentDialog[index];
    }
}