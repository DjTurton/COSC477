using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    

public class ConvertColourButtonPress : MonoBehaviour
{
    public Button yourButton;
    CameraBehaviour cameraBehaviour;
    
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cameraBehaviour = FindObjectOfType<CameraBehaviour>();
    }


    void TaskOnClick()
    {
        cameraBehaviour.getClosestColour();
    }
}
