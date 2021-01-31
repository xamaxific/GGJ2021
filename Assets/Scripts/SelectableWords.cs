using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectableWords : MonoBehaviour
{
    private UIManager m_UIController;
    public string m_myShownName;
    public KeywordsENUM m_myKeyword;
    private void Start() {
        m_UIController = GameController.m_instance.GetUIController();
        if( m_UIController == null ) {
            Debug.LogWarning( "Could not find m_UIController!" );
        }
    }

    public void KeywordClicked() {
        m_UIController.AppendParagraph(m_myShownName, m_myKeyword);
    }

}
