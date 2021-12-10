﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void exit() {
        GameManager.instance.Quit();
    }
    public void load() {
        GameManager.instance.loadFile();
    }
    public void start() {
        GameManager.instance.ChangeScene("Assets/UI/PlayerSelect/CharacterSelect.unity");
    }
}
