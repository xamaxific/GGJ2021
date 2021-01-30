using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacters : MonoBehaviour
{
    [SerializeField] private Transform m_cameraZoomPos;
    [SerializeField] private float m_cameraZoomSize;
    private List<SelectableWords> m_selectableWordsInventory;

    public void OnInteracted(out Vector3 _camPos, out float _camSize) {
        _camPos = m_cameraZoomPos.position;
        _camSize = m_cameraZoomSize;
        Debug.Log( "hi, " + this.name + " is clicked" );
    }
}
