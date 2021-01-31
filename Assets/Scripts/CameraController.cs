using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera m_mainCamera;
    public float m_moveSpeed;
    public AnimationCurve m_moveAnim;
    //Camera Defaults
    private GameController m_gameController;
    private float m_defCamSize;
    private Vector3 m_defCamPos;

    private void Start() {
        m_gameController = GameController.m_instance;
        if( m_mainCamera == null ) {
            Debug.Log( "apparently the main cam is not here? Setting it now." );
            m_mainCamera = Camera.main;
        }
        m_defCamSize = m_mainCamera.orthographicSize;
        m_defCamPos = m_mainCamera.transform.position;
    }

    public void MoveCamera(Vector3 _camPos, float _camSize, bool _b) {
        StartCoroutine( SmoothMove( _camPos, _camSize, _b ) );
    }

    private IEnumerator SmoothMove(Vector3 _targPos, float _targSize, bool _b) {
        m_gameController.SetIsAnimating( true );
        float t = 0;
        float curSize = m_mainCamera.orthographicSize;
        Vector3 curPos = m_mainCamera.transform.position;
        curPos.z = -10;
        while( t < 1 ) {
            t += Time.deltaTime * 1/m_moveSpeed;
            m_mainCamera.orthographicSize = Mathf.Lerp( curSize, _targSize, m_moveAnim.Evaluate( t ) );
            m_mainCamera.transform.position = Vector3.Lerp( curPos, _targPos, m_moveAnim.Evaluate( t ) );
            yield return null;
        }
        if( _b ) {
            m_gameController.TurnOnPanel();
        }
        m_gameController.SetIsAnimating( false );

    }

    public void ReturnCamera() {
        StartCoroutine( SmoothMove(m_defCamPos, m_defCamSize, false) );
    }


}
