﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void Play() {
        // TODO: Change this later to the correct scene name.
        SceneManager.LoadScene("SampleSceneMicha");
    }

    public void Quit() {
        Application.Quit();
    }
}
