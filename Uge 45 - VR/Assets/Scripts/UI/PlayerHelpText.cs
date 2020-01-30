#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
#endregion

public class PlayerHelpText : MonoBehaviour
{
    #region public DATA
    public GameObject HelpText;
    #endregion

    #region private DATA
    Image Rendere;
    TextMeshProUGUI textMesh;
    int currentHint = 0;
    IEnumerator currentAnimation;
    #endregion

    void Start()
    {
        textMesh = HelpText.GetComponent<TextMeshProUGUI>();
        textMesh.text = "";
        Rendere = GetComponent<Image>();
        Rendere.enabled = false;

        StartCoroutine(startCoroutine());
    }

    void Update()
    {
        if (textMesh.text == "" && Rendere.enabled == true)
        {
            Rendere.enabled = false;
        }
        else if (textMesh.text != "" && Rendere.enabled == false)
        {
            Rendere.enabled = true;
        }
    }

    public void ChangeHelpTextOnScreen(int step)
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        bool nextConteniusText = false;

        if (step == 1)
        {
            string newHint = "Hello Player 1. \n\nThis box will show hints and other messages.";
            nextConteniusText = true;
            currentAnimation = AnimateText(newHint, 0, nextConteniusText);
            StartCoroutine(currentAnimation);
            currentHint = 1;
        }
        else if (step == 2)
        {
            string line1 = "You and Player 2 will need to work together to beat this Escape Room!";
            string line2 = "The text in this box is only visible to you, so the only way for Player 2 to know what it is writing is for you to tell them.";
            string newHint = line1 + "\n\n" + line2;
            nextConteniusText = true;
            currentAnimation = AnimateText(newHint, 0, nextConteniusText);
            StartCoroutine(currentAnimation);
            currentHint = 2;
        }
        else if (step == 3)
        {
            string newHint = "Some objects is interactable. \n\nThese objects will show a YELLOW outline when one of your hands is near it!";
            currentAnimation = AnimateText(newHint, 0, nextConteniusText);
            StartCoroutine(currentAnimation);
            currentHint = 3;
        }

    }

    IEnumerator AnimateText(string newText, int nextCharacter, bool nextConteniusText)
    {
        string temptext = "";

        if (nextCharacter + 1 > newText.Length)
        {
            temptext = "" + newText[nextCharacter] + newText[nextCharacter + 1];
        }

        if (temptext == "\n")
        {
            textMesh.text = textMesh.text + "\n";
            nextCharacter += 1;
        }
        else
        {
            textMesh.text = textMesh.text + newText[nextCharacter];
        }

        yield return new WaitForSeconds(0.05f);

        if (textMesh.text.Length < newText.Length)
        {
            currentAnimation = AnimateText(newText, nextCharacter + 1, nextConteniusText);
            StartCoroutine(currentAnimation);
        }
        else if (textMesh.text.Length == newText.Length)
        {
            currentAnimation = RemoveText(nextConteniusText);
            StartCoroutine(currentAnimation);
        }
    }

    IEnumerator RemoveText(bool t)
    {
        if (t)
        {
            yield return new WaitForSeconds(5f);
            textMesh.text = "";
            ChangeHelpTextOnScreen(currentHint + 1);
        }
        else
        {
            yield return new WaitForSeconds(10f);
            textMesh.text = "";
        }
    }

    IEnumerator startCoroutine()
    {
        yield return new WaitForSeconds(1);

        ChangeHelpTextOnScreen(1);
    }

    public void Dev_NextHint()
    {
        ChangeHelpTextOnScreen(currentHint + 1);
    }
}
