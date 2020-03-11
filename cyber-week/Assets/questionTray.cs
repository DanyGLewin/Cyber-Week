using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionTray : MonoBehaviour
{

    [SerializeField] GameObject tab;
    [SerializeField] GameObject body;

    [SerializeField] bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            toggleOpen();
        }
    }

    void toggleOpen()
    {
        toggleActive(tab);
        toggleActive(body);
    }

    void OnMouseDown() {
        toggleOpen();
    }

    void toggleActive(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
