using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContact : MonoBehaviour {


    public ContactFilter2D contactFilter;
    private Collider2D myCollider;
    public LayerMask layerMask;
    
    // Use this for initialization
    void Start () {
        myCollider = GetComponent<Collider2D>();
        contactFilter.useTriggers = true;
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        //contactFilter.useTriggers = false;
        //contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        //contactFilter.useLayerMask = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (myCollider.IsTouching(contactFilter))
            {
                Debug.Log(" Есть контакт");
            }
            else
            {
                Debug.Log("Нет контакта");
            }
        }
        
    }
    
}
