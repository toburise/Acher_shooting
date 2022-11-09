using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limited : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
    }
   
}
