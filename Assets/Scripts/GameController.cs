using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_instance;
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private InputController m_inputController;
    [SerializeField] private UIManager m_uiManager;

    public bool m_isGameStart = false;
    public int m_checkGameEnd = 3;
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
            m_cameraController.MoveCamera( _pos, _size, true );
        }
    }

    public void LeaveConversation() {
        if( !m_isAnimating ) {
            m_uiManager.ActivatePanel( false );
            m_cameraController.ReturnCamera();
        }
    }

    public void TurnOnPanel() {
        m_uiManager.ActivatePanel( true );
        m_inputController.GetCurrentIntChar().FirstInteraction();
    }

    public void SetIsAnimating(bool _b) {
        Debug.Log( "Animating is " + _b );
        m_isAnimating = _b;
        if( m_checkGameEnd <= 0 ) {
            m_isGameStart = false;
            m_uiManager.ShowGameEndScreen();
        }
    }

    public bool GetIsAnimating() {
        return m_isAnimating;
    }

    public UIManager GetUIController() {
        return m_uiManager;
    }

    public InteractableCharacters GCGetCurrIntChar() {
        return m_inputController.GetCurrentIntChar();
    }

    public void StartGame() {
        m_isGameStart = true;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
