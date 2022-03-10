using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GestureIdentifierScript : MonoBehaviour
{

    private float tap_timer;
    private bool has_moved;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    public bool cameraZoomPinch;

    private Vector2 startingAveragePos;

    //Pinch
    float startingDistance;

    //rotate
 
    private float startingAngle = 0;
    
    private float newAngle = 0;

    //camera zooming
    private float zoomOutMinValue = 4.5f;
    private float zoomOutMaxValue = 12;
 


    ITouchController[] managers;
    // Start is called before the first frame update
    void Start()
    {
        managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();


    }

    public void cameraZoomInorOut(float incrementValue)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - incrementValue, zoomOutMinValue, zoomOutMaxValue);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1)
        {
            tap_timer += Time.deltaTime;
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            print(first_touch.phase);

            switch (first_touch.phase)
            {
                case TouchPhase.Began:
                    tap_timer = 0f;
                    has_moved = false;

                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
                    has_moved = true;
                   

                    if (has_moved == true)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).drag(first_touch.position);
                       
                    }
                    break;

                case TouchPhase.Ended:
                    if ((tap_timer < MAX_ALLOWED_TAP_TIME) && !has_moved)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).tap(first_touch.position);
                    }

                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).dragEnd();

                    break;

            }
            

        }

        if (Input.touchCount == 2)
        {
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            Touch second_touch = all_touches[1];




            switch (second_touch.phase)
            {
                case TouchPhase.Began:

                    startingAveragePos = ((first_touch.position + second_touch.position) / 2);
                    startingDistance = Vector2.Distance(second_touch.position, first_touch.position);
                    startingAngle = Mathf.Atan2(second_touch.position.y - first_touch.position.y, second_touch.position.x - first_touch.position.x) * Mathf.Rad2Deg; 
                    has_moved = false;
                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
                    has_moved = true;
                    Vector2 currentAvgPos = ((first_touch.position + second_touch.position) / 2);
                    float endDistance = Vector2.Distance(first_touch.position, second_touch.position);
                    float relDistance = endDistance / startingDistance;
                    newAngle = Mathf.Atan2((second_touch.position.y - first_touch.position.y), (second_touch.position.x - first_touch.position.x)) * Mathf.Rad2Deg;


                    if (has_moved)
                    {
                        foreach (ITouchController manager in managers)
                        {
                            (manager as ITouchController).pinch(startingDistance, endDistance, relDistance);
                            (manager as ITouchController).rotate(newAngle - startingAngle);
                            (manager as ITouchController).twoFDrag(currentAvgPos - startingAveragePos);
                        }

                    }
                    break;

                case TouchPhase.Ended:

                    break;

            }
   
        }
        else
        { 
            foreach (ITouchController manager in managers)
                (manager as ITouchController).pinchEnded();

        }

    }
}
