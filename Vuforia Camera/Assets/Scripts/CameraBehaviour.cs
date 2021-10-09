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
    List<Color> myColours = new List<Color>();
    List<Color> colourChoices = new List<Color>();

    public List<RawImage> myImages;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);  
        int i = myImages.Count - 1;
        Color placeholderColor = new Color(1f, 1f, 1f,  1f);

        while (i >= 0)
        {
            myColours.Add(placeholderColor);
            i--;    
        }

        colourChoices = new List<Color>()
            {
                new Color(1f, 1f, 1f, 1f), // white
                new Color(0f, 0f, 1f, 1f), // blue
                new Color(1f, 0f, 0f, 1f), // red
                new Color(0f, 1f, 0f, 1f), // green
                new Color(1f, 0f, 1f, 1f), // purple
                new Color(1f, 1f, 0f, 1f), // yellow
                new Color(0f, 0f, 0f, 1f) // black
            };

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
            grab = false;
            checking = false;

            int i = myImages.Count - 1;
            while (i >= 0)
            {
                if (i == 0)
                {
                    ColourDisplay = myImages[i].GetComponent<ColourDisplay>();
                    myColours[i] = myPixelColour;
                    ColourDisplay.updateColour(myPixelColour);
                } else {
                    PrevColourDisplay = myImages[i-1].GetComponent<ColourDisplay>(); 
                    myColours[i] = myColours[i-1]; 
                    ColourDisplay = myImages[i].GetComponent<ColourDisplay>();
                    ColourDisplay.updateColour(PrevColourDisplay.getColour());
                }
                i--;
            }
        }
    }

    // Gets the closest colour for every colour in the list 
    public List<Color> getClosestColour()
    {
        int i = 0;
        Color currentColour;

        List<Color> bestColors = new List<Color>();
        
        Color placeholderColor = new Color(1f, 1f, 1f,  1f);
        while (i < myColours.Count)
        {
            bestColors.Add(placeholderColor);
            i++;    
        }
        i = 0;

        while (i < myColours.Count)
        {
            var bestColour = placeholderColor;
            float bestOffset = float.PositiveInfinity;
            currentColour = myColours[i];
            int u = 0;
            while (u < colourChoices.Count)
            {
                var a = FindDifference(currentColour[0], colourChoices[u][0]);
                var b = FindDifference(currentColour[1], colourChoices[u][1]);
                var c = FindDifference(currentColour[2], colourChoices[u][2]);
                float total = (float)(a + b + c);
                if (total < bestOffset)
                {
                    bestColour = colourChoices[u];
                    bestOffset = total;
                }
                u++;
            }
            print(bestColour);
            bestColors[i] = bestColour;
            i ++;
        }

        //TEST CODE FOR COLOUR REASSIGNMENT!!!
        int q = myImages.Count -1;
        while (q >= 0)
        {
            ColourDisplay = myImages[q].GetComponent<ColourDisplay>();
            ColourDisplay.updateColour(bestColors[q]);
            q--;
        } 


        return bestColors;
    }
    
    //gets the difference between two numbers
    public float FindDifference(float nr1, float nr2)
    {
        
        return System.Math.Abs(nr1 - nr2);
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