using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_dialoguePanel;
    [SerializeField] Scrollbar m_scrollBar;
    [SerializeField] ScrollRect m_scrollRect;
    public void AppendParagraph( string _appendName, string _appendText ) {
        m_dialoguePanel.text += "\n\n";
        m_dialoguePanel.text += _appendName;
        m_dialoguePanel.text += "\n\n";
        m_dialoguePanel.text += _appendText;
        m_scrollBar.value = 0;
    }
}
