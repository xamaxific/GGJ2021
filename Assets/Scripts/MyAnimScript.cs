using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimScript : MonoBehaviour
{
    public SpriteRenderer m_spriteRenderer;
    public Sprite[] m_spriteImages;
    public float m_fps;
    public bool m_isAnimating;
    public bool m_isLoop = true;

    private void Start() {
        if( m_isAnimating ) {
            BeginAnimation();
        }
    }

    public void BeginAnimation() {
        StartCoroutine( Animate() );
    }

    private IEnumerator Animate() {
        float t = 0;
        float fps = 1 / m_fps;
        int spriteCounter = 0;

        while( m_isAnimating ) {
            while( t < fps ) {
                t += Time.deltaTime;
                yield return null;
            }
            if( spriteCounter + 1 < m_spriteImages.Length ) {
                spriteCounter += 1;
            } else {
                if( m_isLoop ) {
                    spriteCounter = 0;
                } else {
                    m_isAnimating = false;
                    break;
                }
                
            }
            m_spriteRenderer.sprite = m_spriteImages[ spriteCounter ];
            t = 0;
        }
    }

}
