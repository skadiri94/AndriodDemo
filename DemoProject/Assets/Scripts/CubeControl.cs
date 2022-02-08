using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour, IInteractable
{
   
    bool is_selected = false;
    Renderer my_renderer;
    private Vector3 drag_position;
    float distance;
    //for moving at a constant direction
    //float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

        my_renderer = GetComponent<Renderer>();
       // drag_position = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
      // transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
        //for moving at a constant direction
        /*  if(Vector3.Distance(drag_position,transform.position) < 0.05f)
          {
              transform.position = drag_position;
          }
          else
          {
              Vector3 direction = (drag_position - transform.position).normalized;
              transform.position += speed * direction * Time.deltaTime;
          }*/


    }

    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
        {
            my_renderer.material.color = Color.red;
  
        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }

    /*
    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
        
    }*/

    public void drag_start()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    public void drag_update(Ray r)
    {
        transform.position = r.GetPoint(distance);
    }


}
