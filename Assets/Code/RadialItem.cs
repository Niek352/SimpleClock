using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadialItem : MonoBehaviour
{
    public TMP_Text Text;
    public Image Image;

    public int Value;
    public bool IsTextShowing;

    public void Hightlight()
    {
        Image.enabled = true;
        Text.enabled = true;
       

    }
    public void StopHighlight()
    {
        Image.enabled = false;
        if (IsTextShowing == false)
        {
            Text.enabled = false;
        }
    }
}
