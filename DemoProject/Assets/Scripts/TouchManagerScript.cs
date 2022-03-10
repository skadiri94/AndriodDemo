using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerScript : MonoBehaviour, ITouchController
{


    IInteractable selected_object;
    Quaternion startOrientation;
    Vector3 scale;
    Quaternion cameraStartingOrientation;

    bool twoFinDrag = false;
    bool drag_started = false;
    private bool pinchStarted;

    bool rotate_started = false;
    //CameraControl my_camera;

    public void drag(Vector2 current_position)
    {

        Ray ourRay = Camera.main.ScreenPointToRay(current_position);

        Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);


        if (selected_object != null)
        {

            if (!drag_started)
            {
                selected_object.drag_start();
                drag_started = true;
            }

            selected_object.drag_update(ourRay);

        }



   }

    public void dragEnd()
    {
        drag_started = false;
      if (selected_object != null)
        {
            selected_object.drag_ended();
        }
    }

    public void pinch(float startDist, float endDist, float relativeDistance)
    {

        if (!pinchStarted)
        {
            if (selected_object != null)
            {
                startOrientation = selected_object.gameObject.transform.rotation;
                scale = selected_object.gameObject.transform.localScale;
            }
            else
            {
                startOrientation = Camera.main.transform.rotation;
            }
            pinchStarted = true;
        }
        else
        {
            if(selected_object != null)
            {
                selected_object.gameObject.transform.localScale = scale * relativeDistance;
            }
            else
                Camera.main.transform.position += ((endDist - startDist)/ 1000) * transform.forward;
        }
     
       
    }

    public void pinchEnded()
    {
        pinchStarted = false;
    }

    public void rotate(float angle)
    {
        if (selected_object != null)
        {
            if (!rotate_started)
            {
                //selected_object.rotate_start();
                rotate_started = true;
            }
            selected_object.gameObject.transform.rotation = startOrientation* Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        }

        else
        {
            if (!rotate_started)
            {
                //my_camera.rotate_start();
                rotate_started = true;
            }
            else
                Camera.main.transform.rotation = startOrientation * Quaternion.AngleAxis(-angle, Camera.main.transform.forward);
        }
    }

    public void rotatedEnded()
    {
        rotate_started = false;
    }

    public void rotateStarted()
    {
       
    }


    public void tap(Vector2 position)
    {

        Ray our_ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.red, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();

            if (selected_object != null)
                selected_object.select_toggle();

            
            selected_object = the_object;
            selected_object.select_toggle();


        }
        else
        {
            selected_object.select_toggle();
            selected_object = null;
        }
    }

    public void twoFDrag(Vector2 p)
    {
        if (selected_object == null)
        {

            if (!twoFinDrag) {
                twoFinDrag = true;

                cameraStartingOrientation = Camera.main.transform.rotation;
            }
            else
            {
                Camera.main.transform.rotation = Quaternion.AngleAxis(p.x, Camera.main.transform.up) * cameraStartingOrientation;
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    

    }
}
