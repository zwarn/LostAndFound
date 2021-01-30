using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HasItem
{
    bool hasItem();

    GameObject takeItem();

    void putItem(GameObject item);
}