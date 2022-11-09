using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool m_isFiring = false;
    Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFiring)
        {
            Vector2 vec = m_rb.velocity;
            float alpha = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;// tinh goc hop boi vec x ,y ,change rad -> deg 
            transform.eulerAngles = new Vector3(0, 0, alpha);  /// xoay mui ten theo van toc

        }
    }
    public void Fire(float force)
    {
        if (m_rb == null) return;

        m_rb.isKinematic = false;   //ko td vl -> td trong luc
        transform.parent = null;    // loai bo cha
        m_isFiring = true;
        m_rb.AddRelativeForce(new Vector2(force, 0), ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        TargetController tg = col.transform.root.GetComponent<TargetController>();// truy xuat toi doi tuong cha de truy xuat doi tuong tagetcontroller
        if (col.gameObject.CompareTag(Const.APPLE_TAG)) // if mui ten ban trung apple
        {
            var c2D = col.GetComponent<Collider2D>();//
            if (c2D)                                //
                c2D.enabled = false;//
            col.transform.SetParent(transform);  //  setparent de apple is son for arrow (apple bay theo arrow)
            //score up
            GameManager.Ins.Score++;
            GameManager.Ins.SpawnTarget();
            GUIManager.Ins.UpdateApple(GameManager.Ins.Score);
            AudioController.Ins.PlaySound(AudioController.Ins.appleHit);
        }
        else if (col.gameObject.CompareTag(Const.HEAD_TAG))       // if arrow shoot head 
        {
            //Game over
            GameManager.Ins.state = GameManager.GameState.GameOver;
            GameManager.Ins.GameOver();
            AudioController.Ins.PlaySound(AudioController.Ins.bodyHit);
        }
        if (tg)
            tg.Fall(); 
        
    }
}
