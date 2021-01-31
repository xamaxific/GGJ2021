using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool isInteractionLocked;
    private GameController m_gameController;
    private InteractableCharacters m_currIntChar;
    private List<SelectableWords> m_selectableWordsInventory;
    [SerializeField] private AudioSource m_uiClick;

    private void Start() {
        m_gameController = GameController.m_instance;
    }

    private void Update() {
        OnMouseClick();
        OnLeaveConversation();
    }

    private void OnMouseClick() {
        if( Input.GetMouseButtonDown( 0 ) ) {
            m_uiClick.Play();
            if( GameController.m_instance.m_isGameStart && !GameController.m_instance.GetIsAnimating() ) {
                if( !isInteractionLocked ) {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
                    Vector2 mousePos2D = new Vector2( mousePos.x, mousePos.y );
                    RaycastHit2D hit = Physics2D.Raycast( mousePos2D, Vector2.zero );
                    if( hit.collider != null && hit.collider.CompareTag( "Interactable" ) ) {
                        isInteractionLocked = true;
                        float camSize;
                        Vector3 camPos;
                        m_currIntChar = hit.collider.GetComponent<InteractableCharacters>();
                        m_currIntChar.OnInteracted( out camPos, out camSize );
                        Debug.Log( m_currIntChar.name + " is clicked!" );
                        m_gameController.ClickOnCharacter( camPos, camSize );
                    }
                }
            }
        }



    }

    private void OnLeaveConversation() {
        if( GameController.m_instance.m_isGameStart ) {
            if( !GameController.m_instance.GetIsAnimating() && isInteractionLocked && Input.GetMouseButtonDown( 1 ) ) {
                m_gameController.LeaveConversation();
                m_currIntChar.CheckEndLeave();
                m_currIntChar = null;
                isInteractionLocked = false;
            }
        }
    }

    public InteractableCharacters GetCurrentIntChar() {
        return m_currIntChar;
    }
}
