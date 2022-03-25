using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Renderer my_renderer;
    Vector3 init_pos;
    Quaternion init_angle;
    Quaternion init_Orientation;
    private float phi;
    private float theta;
    Vector3 dir;
    float speed = 50.0f;
    float amount = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void get_dragged(Vector2 v)
    {
        transform.position -= 0.01f * v.x * transform.right;
        transform.position -= 0.01f * v.y * transform.up;
    }



    public void pinch_start()
    {
        init_pos = transform.position;
        
    }

    public void pinch(float startDist, float endDist)
    {
       transform.position = init_pos + ((endDist - startDist) / 1000) * transform.forward;
        
    }



    public void rotate_start()
    {
       
        init_angle = transform.rotation;
    }

    public void rotate(float angle)
    {
        transform.rotation = init_angle * Quaternion.AngleAxis((-angle), Camera.main.transform.forward);
    }

    internal void twoFingerDrag(Vector2 p)
    {

        phi = 90 - 180 * p.y / Screen.height;
        theta = 90 - 180 * p.x / Screen.width;
        phi *= Mathf.Deg2Rad;
        theta *= Mathf.Deg2Rad;
     
        Vector3 v = new Vector3(p.x / Screen.width, p.y / Screen.height, 1.0f);

        dir = new Vector3(Mathf.Cos(theta) * Mathf.Sin(phi), Mathf.Cos(phi), Mathf.Sin(theta) * Mathf.Sin(phi));

        transform.LookAt(transform.position+ dir);

        
      
    }



    public void twoFDragStart()
    {
        dir = transform.InverseTransformPoint(dir);
    }

    internal void startShake()
    {
        init_pos = transform.position;
    }

    internal void shakeCam()
    {
  
        transform.position = new Vector3(init_pos.x + (Mathf.Sin(Time.time * speed) * amount), init_pos.y + (Mathf.Sin(Time.time * speed) * amount), init_pos.z);
    }
}
