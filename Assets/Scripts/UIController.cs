using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_dialoguePanel;

    public void AppendParagraph( string _appendName, string _appendText ) {
        m_dialoguePanel.text += "\n\n";
        m_dialoguePanel.text += _appendName;
        m_dialoguePanel.text += "\n\n";
        m_dialoguePanel.text += _appendText; 
    }
}
