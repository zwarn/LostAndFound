using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    private static UiScript instance = null;

    public static UiScript Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public TMP_Text itemName;
    public GameObject itemNamePanel;

    public void showItemName(ItemScript itemScript)
    {
        if (itemScript == null)
        {
            itemNamePanel.SetActive(false);
        }
        else
        {
            itemNamePanel.SetActive(true);
            itemName.text = itemScript.itemName;
        }
    }
}