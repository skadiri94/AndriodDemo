using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTouchManagerScript : MonoBehaviour, ITouchController
{


    IInteractable selected_object;
    float starting_distance_to_selected_object;
    public void drag(Vector2 current_position)
    {
      
        Ray ourRay = Camera.main.ScreenPointToRay(current_position);

        Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);

            RaycastHit info;
            if (Physics.Raycast(ourRay, out info))
            {
                IInteractable objectHit = info.transform.GetComponent<IInteractable>();
        
                selected_object = objectHit;
            Vector3 a = Camera.main.transform.position;
            Vector3 b = info.transform.position;
       
            b.z = 0;

          
     
            starting_distance_to_selected_object = Vector3.Distance(a, b);

                    Ray new_positional_ray = Camera.main.ScreenPointToRay(current_position);

                    { (selected_object as CubeControl).MoveTo(new_positional_ray.GetPoint(starting_distance_to_selected_object)); }

                    // selected_object.MoveTo(new_positional_ray.GetPoint(starting_distance_to_selected_object));
            
        }

        

    
}

    public void pinch(Vector2 position_1, Vector2 position_2, float relative_distance)
    {
        throw new System.NotImplementedException();
    }

    public void tap(Vector2 position)
    {
        print("Im the manager and I recieved a tap from gesture");


        Ray our_ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.red, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();

           
            the_object.select_toggle();
            selected_object = the_object;
            

            if (the_object is CubeControl)
            { (the_object as CubeControl).Do_cube_stuff(); }
            print("I hit something!!");

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
