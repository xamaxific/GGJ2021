using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectableWords : MonoBehaviour
{
    private UIManager m_UIManager;
    public string m_myShownName;
    public KeywordsENUM m_myKeyword;
    public UIController m_myUIController;
    private void Start() {
        m_UIManager = GameController.m_instance.GetUIController();
        if( m_UIManager == null ) {
            Debug.LogWarning( "Could not find m_UIController!" );
        }
    }

    public void ShownFirstTime() {
        gameObject.SetActive( true );
        if( m_myUIController != null ) {
            m_myUIController.Show();
        }
    }

    public void ShowWord(bool _b) {
        if( _b ) {
            gameObject.SetActive( true );
            if( m_myUIController != null ) {
                Debug.Log( "hut" );
                m_myUIController.Show();
            }
        } else {
            gameObject.SetActive( false );
        }
        

    }

    public void KeywordClicked() {
        m_UIManager.AppendParagraph(m_myShownName, m_myKeyword);
    }

}
