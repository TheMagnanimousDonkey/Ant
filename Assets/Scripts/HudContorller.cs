using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudContorller : MonoBehaviour
{
    public Text blackText;
    public Text redText;
    private int blackCount = 0;
    private int redCount = 0;

    public void Update()
    {
        blackText.text = blackCount.ToString();
        redText.text = redCount.ToString();
    }

    public void handleBlack(bool black)
    {
        if (black)
        {
            blackCount++;
        }
        else 
        {
            blackCount--;
        }
    }
    public void handleRed(bool red)
    {
        if (red)
        {
            redCount++;
        }
        else
        {
            redCount--;
        }
    }

}
