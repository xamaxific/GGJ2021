using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_instance;
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private InputController m_inputController;
    
    private bool m_isAnimating;
  
    private void Awake() {
        if( m_instance == null ) {
            m_instance = this;
        } else {
            Destroy( this );
        }
    }

    public void ClickOnCharacter(Vector3 _pos, float _size) {
        if( !m_isAnimating ) { 
            m_cameraController.MoveCamera( _pos, _size );
        }
    }

    public void LeaveConversation() {
        if( !m_isAnimating ) {
            m_cameraController.ReturnCamera();
        }
    }

    public void SetIsAnimating(bool _b) {
        Debug.Log( "Animating is " + _b );
        m_isAnimating = _b;
    }

}
