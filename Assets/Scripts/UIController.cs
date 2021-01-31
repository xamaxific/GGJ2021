using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] GameObject m_dialoguePanel;
    [SerializeField] TextMeshProUGUI m_dialogueText;
    [SerializeField] TextMeshProUGUI m_nameText;
    [SerializeField] Scrollbar m_scrollBar;
    [SerializeField] ScrollRect m_scrollRect;
    [SerializeField] SelectableWords m_rememberWord;

    private bool m_updateScroll;
    private void Start() {
        m_dialoguePanel.SetActive( false );
        m_dialogueText.text = "";
    }

    private void FixedUpdate() {
        if(m_updateScroll) {
            m_updateScroll = false;
            Vector3 v3 = m_scrollRect.content.localPosition;
            v3.y = m_dialogueText.rectTransform.sizeDelta.y;
            m_scrollRect.content.localPosition = v3;
        }
    }

    public void SetRememberWordActive( bool _b ) {
        m_rememberWord.gameObject.SetActive( _b );
    }

    public void ActivatePanel(bool _b) {
        if( _b ) {
            m_dialogueText.text = GameController.m_instance.GCGetCurrIntChar().GetOpener();
            m_nameText.text = GameController.m_instance.GCGetCurrIntChar().GetCharName();
            if( GameController.m_instance.GCGetCurrIntChar().GetIsEndUnlocked() ) {
                SetRememberWordActive( true );
            } else {
                SetRememberWordActive( false );
            }
            m_dialoguePanel.SetActive( true );
        } else {
            m_dialoguePanel.SetActive( false );
            m_dialogueText.text = "";
        }

    }

    public void AppendParagraph( string _appendName, KeywordsENUM _key ) {
        string _appendText = GameController.m_instance.GCGetCurrIntChar().GetKeywordDialogue( _key );
        _appendText = _appendText.Replace( "\\n", "\n" );
        m_dialogueText.text += "\n\n";
        m_dialogueText.text += "<size=150%><b><color=yellow>"+_appendName+"</color></b></size>";
        m_dialogueText.text += "\n\n";
        m_dialogueText.text += _appendText;
        m_dialogueText.ForceMeshUpdate();
        m_updateScroll = true;
    }

}
