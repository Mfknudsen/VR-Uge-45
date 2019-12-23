#region System
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#endregion

public class DisplayScreen : MonoBehaviour
{
    public void RemoveText(TextMeshPro text)
    {
        text.text = "";
    }

    public void DisplayFromInt(TextMeshPro text, int textToAdd)
    {
        text.text = text.text + " " + textToAdd;
    }

    public void DisplayFromFloat(TextMeshPro text, float textToAdd)
    {
        text.text = text.text + " " + textToAdd;
    }

    public void DisplayFromString(TextMeshPro text, string textToAdd)
    {
        text.text = text.text + " " + textToAdd;
    }

    public void DisplayFromBool(TextMeshPro text, bool textToAdd)
    {
        text.text = text.text + " " + textToAdd;
    }
}
