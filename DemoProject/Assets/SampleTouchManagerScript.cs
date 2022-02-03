using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTouchManagerScript : MonoBehaviour, ITouchController
{


    IInteractable selected_object;
    float starting_distance_to_selected_object;
    bool drag_started = false;
    public void drag(Vector2 current_position)
    {

        Ray ourRay = Camera.main.ScreenPointToRay(current_position);

        Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);


        if (selected_object != null)
        {

            if (!drag_started)
            {

                starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, (selected_object as MonoBehaviour).transform.position);
                drag_started = true;
            }

            Ray new_positional_ray = Camera.main.ScreenPointToRay(current_position);

            { (selected_object as CubeControl).MoveTo(new_positional_ray.GetPoint(starting_distance_to_selected_object)); }

        }



   }

    public void pinch(Vector2 position_1, Vector2 position_2, float relative_distance)
    {
        throw new System.NotImplementedException();
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

            the_object.select_toggle();
            selected_object = the_object;
            

            if (the_object is CubeControl)
            { (the_object as CubeControl).Do_cube_stuff(); }
            print("I hit something!!");

        }
        else
        {
            selected_object.select_toggle();
            selected_object = null;
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
