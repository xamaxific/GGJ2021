using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RandomTimer : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onTimerEnd;
    [SerializeField] private bool m_loop;
    [SerializeField] private Vector2 m_waitRange;

    private void Start() {
        StartCoroutine( m_RandomTimer() );
    }

    private IEnumerator m_RandomTimer() {
        float t = 0;
        float w = Random.Range( m_waitRange.x, m_waitRange.y );
        while( t < w ) {
            t += Time.deltaTime;
            yield return null;
        }
        m_onTimerEnd.Invoke();
        if( m_loop ) {
            StartCoroutine( m_RandomTimer() );
        }
    }
}
