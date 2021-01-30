using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectableWords : MonoBehaviour
{
    private UIController m_UIController;
    [SerializeField] private string m_appendName;
    [SerializeField] private string m_appendText;

    private void Start() {
        m_UIController = GameController.m_instance.GetUIController();
        if( m_UIController == null ) {
            Debug.LogWarning( "Could not find m_UIController!" );
        }
    }

    public void AppendText() {
        m_UIController.AppendParagraph( m_appendName, m_appendText );
    }

}
