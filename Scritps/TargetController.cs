using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour 
{
    public float minSpeed;
    public float maxSpeed;

    float m_CurSpeed;    ///speed hien tai
    bool m_isFalling; // bia ban roi
    Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        m_CurSpeed = Random.Range(minSpeed, maxSpeed);
    }
    private void Update()
    {
        if(m_rb && !m_isFalling)
        {
            m_rb.velocity = Vector2.down * m_CurSpeed; //(0,-1)
        }
    }
    public void Fall()
    {
        m_isFalling = true;
        if (m_rb)
        {
            m_rb.isKinematic = false;// nếu chưa chịu td vật lý thì bia sẽ chịu td của trọng lực và rơi dần xuống
            Vector2 vector2 = Vector2.down * 3f;
            m_rb.velocity = vector2;

        }
    }
   
}
