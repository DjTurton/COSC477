using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    
public class CameraBehaviour : MonoBehaviour
{
    private Texture2D m_Texture;
    public Color myPixelColour;
    bool grab;
    public bool checking;

    ColourDisplay ColourDisplay;
    ColourDisplay PrevColourDisplay;
    List<Color> myColours;


    public List<RawImage> myImages;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);  

    }
    // Takes a screenshot at end of operations 
    void OnPostRender()
    {
        //yield return frameEnd;
        if (checking) 
        {
            m_Texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
            m_Texture.Apply();
            myPixelColour = m_Texture.GetPixel(Screen.width/2, Screen.height/2);
            Debug.Log(myPixelColour);
            grab = false;
            checking = false;

            int i = myImages.Count - 1;
            while (i >= 0)
            {
                if (i == 0)
                {
                    ColourDisplay = myImages[i].GetComponent<ColourDisplay>();
                    ColourDisplay.updateColour(myPixelColour);
                } else {
                    PrevColourDisplay = myImages[i-1].GetComponent<ColourDisplay>(); 
                    ColourDisplay = myImages[i].GetComponent<ColourDisplay>();
                    ColourDisplay.updateColour(PrevColourDisplay.getColour());
                }
                i--;
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (checking)
        {
            grab = true;
        }
    }
}