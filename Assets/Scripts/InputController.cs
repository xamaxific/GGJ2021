using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool isInteractionLocked;
    private GameController m_gameController;
    private InteractableCharacters m_currIntChar;
    [SerializeField] private SelectableWords[] m_selectableWordsArray;
    private List<SelectableWords> m_selectableWordsInventory;

    private void Start() {
        m_gameController = GameController.m_instance;
    }

    private void Update() {
        OnMouseClick();
        OnLeaveConversation();
    }

    private void OnMouseClick() {
        if( Input.GetMouseButtonDown( 0 ) ) {
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
                    m_gameController.ClickOnCharacter( camPos, camSize );
                }
            } else {

            }
        }

    }

    private void OnLeaveConversation() {
        if( isInteractionLocked && Input.GetMouseButtonDown( 1 ) ) {
            m_gameController.LeaveConversation();
            isInteractionLocked = false;
        }
    }
}
