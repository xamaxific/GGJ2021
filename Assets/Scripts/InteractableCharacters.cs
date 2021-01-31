using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableCharacters : MonoBehaviour
{
    [SerializeField] private Transform m_cameraZoomPos;
    [SerializeField] private float m_cameraZoomSize;
    [SerializeField] private CharInfoObj m_charInfoObj;
    [SerializeField] private SelectableWordsUnlock[] m_selectableWordsUnlockArray;
    [SerializeField] private List<KeywordsENUM> m_usedKeys;
    [SerializeField] private UIController m_myUIController;
    private int m_unlockCounter;
    private bool m_endUnlocked = false;
    private bool m_firstInteraction = true;
    [SerializeField] private SelectableWords m_myNamesake;

    private void Start() {
        //m_usedKeys = new List<KeywordsENUM>();
        m_unlockCounter = m_usedKeys.Count;   
    }

    public void OnInteracted(out Vector3 _camPos, out float _camSize) {
        _camPos = m_cameraZoomPos.position;
        _camSize = m_cameraZoomSize;
    }

    public void FirstInteraction() {
        if( m_firstInteraction ) {
            m_firstInteraction = false;
            m_myNamesake.gameObject.SetActive( true );
        }
    }

    public bool GetIsEndUnlocked() {
        return m_endUnlocked;
    }

    public string GetCharName() {
        return m_charInfoObj.m_shownName;
    }

    public string GetOpener() {
        return m_charInfoObj.m_opener;
    }

    public string GetCloser() {
        return m_charInfoObj.m_complete;
    }

    public string GetKeywordDialogue(KeywordsENUM _key) {
        for( int i = 0; i < m_charInfoObj.m_keywordInfo.Length; i++ ) {
            if( _key == m_charInfoObj.m_keywordInfo[ i ].m_keywordEnum ) {
                for( int j = 0; j < m_selectableWordsUnlockArray.Length; j++ ) {
                    if( m_selectableWordsUnlockArray[ j ].m_isUnlocked == false && _key == m_selectableWordsUnlockArray[ j ].m_wordUnlockKey ) {
                        m_selectableWordsUnlockArray[ j ].m_isUnlocked = true;
                        m_selectableWordsUnlockArray[ j ].m_selectableWord.gameObject.SetActive( true );
                    }
                }
                if( !m_endUnlocked ) {
                    for( int j = 0; j < m_usedKeys.Count; j++ ) {
                        if( _key == m_usedKeys[ j ] ) {
                            m_unlockCounter -= 1;
                            m_usedKeys.Remove( m_usedKeys[ j ] );
                            if( m_unlockCounter <= 0 ) {
                                m_endUnlocked = true;
                                GameController.m_instance.GetUIController().SetRememberWordActive( true );
                            }
                        }

                    }
                }
                return m_charInfoObj.m_keywordInfo[ i ].m_keywordInfo;
            }
        }
        Debug.LogWarning( "Couldn't find keyword!" );
        return "ERROR!";
    }

    public void CheckEndLeave() {
        if( m_endUnlocked ) {
            GameController.m_instance.m_checkGameEnd -= 1;
            m_myUIController.Hide();
        }
    }

    [Serializable]
    public struct SelectableWordsUnlock {
        public KeywordsENUM m_wordUnlockKey;
        public bool m_isUnlocked;
        public SelectableWords m_selectableWord;
    }
}
