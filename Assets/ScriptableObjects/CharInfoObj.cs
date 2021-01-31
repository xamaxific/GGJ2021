using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu( fileName = "Data", menuName = "ScriptableObjects/CharInfo", order = 2 )]
public class CharInfoObj : ScriptableObject
{
    public string m_shownName;
    public string m_opener;
    public string m_complete;
    public KeywordInfo[] m_keywordInfo;

    [Serializable]
    public struct KeywordInfo {
        public KeywordsENUM m_keywordEnum;
        public string m_keywordInfo;
    }
}
