using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GestureIdentifierScript : MonoBehaviour
{
    enum idMode { determiningGesture,lockedToPinch, lockedToRotate, lockedToTwoFDrag, lockedToPress, lockedToDrag }
    idMode isCurrently = idMode.determiningGesture;
    private float tapTimer;
    private bool hasMoved, isStationed;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    public bool cameraZoomPinch;

    private Vector2 startingAveragePos;

    //Pinch
    float startingDistance;

    //rotate
    private float startingAngle = 0; 
    private float newAngle = 0;

    ITouchController[] managers;

    // Start is called before the first frame update
    void Start()
    {
        managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();


    }


    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1)
        {
            tapTimer += Time.deltaTime;
            Touch[] allTouches = Input.touches;
            Touch firstTouch = allTouches[0];
            print(firstTouch.phase);

            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    tapTimer = 0f;
                    hasMoved = false;

                    break;
                case TouchPhase.Stationary:
                    if (tapTimer > 1.5f)
                    {
                        isCurrently = idMode.lockedToPress;
                        if (isCurrently == idMode.lockedToPress)
                        {
                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).press();
                        }
                    }
                    break;
                case TouchPhase.Moved:

                 
                    isCurrently = idMode.lockedToDrag;
                    foreach (ITouchController manager in managers)
                    {
                        (manager as ITouchController).endPress();

                    }



                    if (isCurrently == idMode.lockedToDrag)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).drag(firstTouch.position);
                       
                    }
                    break;

                case TouchPhase.Ended:

                    isCurrently = idMode.determiningGesture;
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).endPress();
                
                    if ((tapTimer < MAX_ALLOWED_TAP_TIME) && !hasMoved)
                    {
                     

                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).tap(firstTouch.position);
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
            Touch secondTouch = all_touches[1];


            switch (secondTouch.phase)
            {
                case TouchPhase.Began:

                    startingAveragePos = ((first_touch.position + secondTouch.position) / 2);         
                    startingDistance = Vector2.Distance(secondTouch.position, first_touch.position);
                    startingAngle = Mathf.Atan2(secondTouch.position.y - first_touch.position.y, secondTouch.position.x - first_touch.position.x) * Mathf.Rad2Deg; 
                    hasMoved = false;
                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
                    hasMoved = true;
                    Vector2 currentAvgPos = ((first_touch.position + secondTouch.position) / 2);
                    float endDistance = Vector2.Distance(first_touch.position, secondTouch.position);
                    float relDistance = endDistance / startingDistance;
                    newAngle = Mathf.Atan2((secondTouch.position.y - first_touch.position.y), (secondTouch.position.x - first_touch.position.x)) * Mathf.Rad2Deg;

                    switch (isCurrently)
                    {

                        case idMode.determiningGesture:

                            if (Mathf.Abs(newAngle - startingAngle) > 5)
                                isCurrently = idMode.lockedToRotate;
                            else if (Mathf.Abs(relDistance - 1) > 0.2f)
                                isCurrently = idMode.lockedToPinch;
                            else
                                if (Vector2.Distance(startingAveragePos, currentAvgPos) > 70)
                                isCurrently = idMode.lockedToTwoFDrag;
                            if (isCurrently != idMode.determiningGesture) print(isCurrently);
                            break;


                        case idMode.lockedToRotate:

                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).rotate(newAngle - startingAngle);

                            break;


                        case idMode.lockedToPinch:
                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).pinch(startingDistance, endDistance, relDistance);
                            break;
                        case idMode.lockedToTwoFDrag:
                            foreach (ITouchController manager in managers)
                                (manager as ITouchController).twoFDrag(currentAvgPos - startingAveragePos);
                            break;


                    }

                    break;


                case TouchPhase.Ended:
                    isCurrently = idMode.determiningGesture;
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).dragEnd();
                    break;

            }
   
        }
        else
        {
            foreach (ITouchController manager in managers)
            {
                //one method for all ended.
                (manager as ITouchController).pinchEnded();
                (manager as ITouchController).rotatedEnded();
            }

        }

    }
}
