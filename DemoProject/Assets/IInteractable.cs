using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    void drag_start();
    void select_toggle();
    void drag_update(Ray r);
    
}
