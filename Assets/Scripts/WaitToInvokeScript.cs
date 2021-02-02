using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitToInvokeScript : MonoBehaviour
{
    public float m_waitTime;
    public bool m_onStart;
    public UnityEvent m_triggerAfterWait;
    

    private void Start() {
        if( m_onStart ) {
            BeginWait();
        }
    }

    public void BeginWait() {
        StartCoroutine( Wait() );
    }

    private IEnumerator Wait() {
        float t = 0;
        while( t < m_waitTime ) {
            t += Time.deltaTime;
            yield return null;
        }
        m_triggerAfterWait.Invoke();
    }
}
