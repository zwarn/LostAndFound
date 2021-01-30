using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private static Input _input;

    public static Input Instance()
    {
        if (_input == null)
        {
            _input = new Input();
        }

        return _input;
    }
}