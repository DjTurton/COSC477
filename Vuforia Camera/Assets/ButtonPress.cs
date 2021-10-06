using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    

public class ButtonPress : MonoBehaviour
{
    public Button yourButton;
    CameraBehaviour cameraBehaviour;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cameraBehaviour = FindObjectOfType<CameraBehaviour>();
    }

    void TaskOnClick()
    {
        cameraBehaviour.checking = true;
    }
}
