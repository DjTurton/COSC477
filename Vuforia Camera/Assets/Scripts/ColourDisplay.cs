using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColourDisplay : MonoBehaviour
{

    

    public void updateColour(Color newColour)
    {
        GetComponent<RawImage>().color = newColour;
    }

    public Color getColour()
    {
        Color color = GetComponent<RawImage>().color;
        return color;  
    }
}
