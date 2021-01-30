using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu( fileName = "Data", menuName = "ScriptableObjects/Keywords", order = 1 )]
public class KeywordsObj : ScriptableObject {
    public KeywordsENUM m_name;
    [SerializeField] private KeywordInfo[] m_keyword;

    [Serializable]
    private struct KeywordInfo {
        [SerializeField] private CharactersENUM m_char;
        [SerializeField] string m_keywordInfo;
    }
}
