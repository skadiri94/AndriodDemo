using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IInteractable
{

    bool is_selected = false;
    Renderer my_renderer;
    LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {

        my_renderer = GetComponent<Renderer>();
        mask = LayerMask.GetMask("Ground");
    

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
        {
            my_renderer.material.color = Color.red;
            gameObject.layer = 2;
        }

        else
        {
            my_renderer.material.color = Color.white;
            gameObject.layer = 0;
        }

    }



    public void drag_start()
    {
 
      
    }

    public void drag_update(Ray r)
    {
        RaycastHit info;

        if (Physics.Raycast(r, out info, 1000.0f, (int)mask))
        {
            transform.position = info.point + info.normal * 0.5f;

        }
    }

    public void drag_ended()
    {
     
    }

}
