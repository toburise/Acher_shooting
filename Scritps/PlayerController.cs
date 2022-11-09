using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Arrow arrowPb;
    public float dragCtr;        // 
    public float fireForce;
    public Transform arrowPoint;
    public Transform arrowSpawnPoint;
    public Transform arrowDirection;

    public float m_minLimited;
    public float m_maxLimited;
    Vector2 m_dragPos1;   // Vi tri keo cung 1
    Vector2 m_dragPos2;
    float m_dragDig;      // khoang cach player keo cung
    bool m_isDragging;
    Arrow m_arrowClone;
    // Start is called before the first frame update
    void Start()
    {
        SpawnArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Ins.state != GameManager.GameState.Playing) return;
        if (Input.GetButtonDown("Fire1")) // Fire1 in Unity / Edit - InputManerger- Axes - Fire1
        {
            m_isDragging = true;         // Đã kéo cung
            m_dragPos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);//Lấy tọa độ của player click screen in  Unity 
        }else if (Input.GetButtonUp("Fire1"))
        {
            m_isDragging = false;
            arrowPoint.localPosition = new Vector3(m_maxLimited,0f,0f); // sau khi kéo dây cung thì day cung về lại vị trí ban đầu
            arrowDirection.localScale = new Vector3(0, 0, 0);
            if (m_dragDig > 0.1f)   //khoang cach keo cung > 0.1f
                Fire();
        }
        if (m_isDragging)
        {
            m_dragPos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_dragDig = Vector2.Distance(m_dragPos1, m_dragPos2) * dragCtr;
            if (m_dragDig < 0.05f) return; /// lực kéo <0,5 thì return

            var dragDir = new Vector2(m_dragPos1.x - m_dragPos2.x, m_dragPos1.y - m_dragPos2.y); // A(a1,b1)-> B(a2,b2) = (a2 - a1 , b2 - b1)
            float alpha = Mathf.Atan2(dragDir.y, dragDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, alpha);
            float dragX = m_maxLimited - m_dragDig;
            dragX = Mathf.Clamp(dragX, m_minLimited, m_maxLimited);// gioi han trong khoang min and max
            arrowPoint.localPosition = new Vector3(dragX, 0, 0);

            float dirPointScaleX = Mathf.Clamp(m_dragDig, 0, 0.5f) * 2;
            arrowDirection.localScale = new Vector3(dirPointScaleX, 1, 1);

        }

    }
    void SpawnArrow()              // tao ra new arrow 
    {
        if (arrowPb == null) return;
        m_arrowClone = Instantiate(arrowPb);
        m_arrowClone.transform.SetParent(arrowSpawnPoint,false);
        m_arrowClone.transform.localScale = Vector3.one;
        m_arrowClone.transform.localPosition = Vector3.zero;
    }
    IEnumerator SpawnNextArrow(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnArrow();
    }
    public void Fire()
    {
        if (m_arrowClone == null) return;

        float curForce = Mathf.Clamp(m_dragDig,0,0.5f) * fireForce;
        m_arrowClone.Fire(curForce);
        StartCoroutine(SpawnNextArrow(0.2f)); /// Tạo mới mũi tên sau khi bắn
    }
}
