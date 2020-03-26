using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class questionTray : MonoBehaviour
{

    [SerializeField] GameObject tab;
    [SerializeField] GameObject body;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            toggleOpen();
        }
    }

    public void toggleOpen()
    {

        toggleActive(tab);
        toggleActive(body);
    }

    void toggleActive(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}