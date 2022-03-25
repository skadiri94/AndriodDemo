using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerScript : MonoBehaviour, ITouchController
{
    //Press
    float speed = 100.0f; //how fast it shakes
    float amount = 0.02f; //how much it shakes
    Vector3 startPos;
    bool pressStart = false;

    IInteractable selected_object;
    Quaternion startOrientation;
    Vector3 scale;
 
    CameraControl my_camera;
    Vector2 startingDragPos;

    bool twoFinDrag = false;
    bool drag_started = false;
    private bool pinchStarted;

    bool rotate_started = false;
    private Vector3 startingCameraPos;

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

        else
        {
            if (!drag_started)
            {
                startingDragPos = current_position;
                drag_started = true;
            }
            my_camera.get_dragged(current_position - startingDragPos);
            startingDragPos = current_position;
        }



    }

    public void dragEnd()
    {
        drag_started = false;
        twoFinDrag = false;
      if (selected_object != null)
        {
            selected_object.drag_ended();
        }
    }

    public void endGestures()
    {
        throw new System.NotImplementedException();
    }

    public void endPress()
    {
        pressStart = false;
    }

    public void pinch(float startDist, float endDist, float relativeDistance)
    {

        if (!pinchStarted)
        {
            if (selected_object != null)
            {
                
                scale = selected_object.gameObject.transform.localScale;
            }
            else
            {
              
                my_camera.pinch_start();
              
            }
            pinchStarted = true;
        }
        else
        {
            if (selected_object != null)
            {
                selected_object.gameObject.transform.localScale = scale * relativeDistance;
            }
            else
             
                my_camera.pinch(startDist, endDist);
            
        }
     
       
    }

    public void pinchEnded()
    {
        pinchStarted = false;
    }

//Touching surface for extended period of time.
    public void press()
    {

        if (selected_object != null)
        {
            if (!pressStart) { 
            startPos = selected_object.gameObject.transform.position;
            pressStart = true;

        }
            startPos.x = startPos.x + (Mathf.Sin(Time.time * speed) * amount);
            startPos.y = startPos.y + (Mathf.Sin(Time.time * speed) * amount);
            selected_object.gameObject.transform.position = new Vector3(startPos.x,startPos.y, startPos.z);
          
        }
        else
        {
            if (!pressStart)
            {
                my_camera.startShake();
                drag_started = true;
            }
            my_camera.shakeCam();
            
        }
    }

    public void rotate(float angle)
    {
        if (selected_object != null)
        {
            if (!rotate_started)
            {
                //selected_object.rotate_start();
                startOrientation = selected_object.gameObject.transform.rotation;
                rotate_started = true;
            }
            selected_object.gameObject.transform.rotation = startOrientation * Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        }

        else
        {
            if (!rotate_started)
            {
                my_camera.rotate_start();
                rotate_started = true;
            }
            else
                my_camera.rotate(angle);
                
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
                my_camera.twoFDragStart();

            }
            else
            {

                my_camera.twoFingerDrag(p);

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        my_camera = Camera.main.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
    

    }
}
