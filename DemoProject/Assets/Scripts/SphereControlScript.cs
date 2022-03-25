using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControlScript : MonoBehaviour, IInteractable
{
    bool isSelected = false;
    Renderer myRenderer;
    Vector3 dragPosition;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        dragPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,dragPosition, 0.5f);
    }

    public void drag_start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    public void drag_update(Ray r)
    {
        dragPosition = r.GetPoint(distance);
    }

    public void select_toggle()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            myRenderer.material.color = Color.green;

        }
        else
            myRenderer.material.color = Color.white;
    }

    public void drag_ended()
    {
     
    }

}
