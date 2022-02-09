using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControlScript : MonoBehaviour, IInteractable
{
    bool is_selected = false;
    Renderer my_renderer;
    GameObject ourCameraPlane;
    Vector3 drag_position;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        my_renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,drag_position, 0.5f);
    }

    public void drag_start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    public void drag_update(Ray r)
    {
        drag_position = r.GetPoint(distance);
    }

    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
        {
            my_renderer.material.color = Color.green;

        }
        else
            my_renderer.material.color = Color.white;
    }
}
