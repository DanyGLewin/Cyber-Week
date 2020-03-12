using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using UnityEngine;

public class questionTray : MonoBehaviour
{

    [SerializeField] GameObject tab;
    [SerializeField] GameObject body;

    [SerializeField] GameObject redPlatform;
    [SerializeField] GameObject greenPlatform;
    [SerializeField] GameObject bluePlatform;
    [SerializeField] GameObject yellowPlatform;

    // Start is called before the first frame update
    void Start()
    {
        loadQuestionsToObject();
    }

    void loadQuestionsToObject()
    {
        string text = System.IO.File.ReadAllText("questions.json");
        print(text);
        // var serializer = new JavaScriptSerializer();
    }

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
