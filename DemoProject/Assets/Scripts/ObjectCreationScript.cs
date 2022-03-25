using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectCreationScript : MonoBehaviour
{
    
    GameObject plane;
    GameObject cube;
    GameObject sphere;
    GameObject capsule;
    Material grass;
    GameObject gestureIndentifier;
    GameObject touchManager;


    // Start is called before the first frame update
    void Start()
    {



        gestureIndentifier = new GameObject("GestureIdentifier");
        gestureIndentifier.AddComponent<GestureIdentifierScript>();

        touchManager = new GameObject("TouchManager");
        touchManager.AddComponent<TouchManagerScript>();


   
        grass = Resources.Load("Grass", typeof(Material)) as Material;
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.GetComponent<Renderer>().material = grass;
        plane.layer = LayerMask.NameToLayer("Ground"); 
        plane.transform.position = new Vector3(0f, 0f, 0f);
        plane.transform.localScale = new Vector3(2f, 2f, 2f);





        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(-1.8f, 0.5f,-6f);
        cube.AddComponent<CubeControl>();

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new  Vector3(4.85f, 1.10f, 1.20f);
        sphere.AddComponent<SphereControlScript>();

        capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(0.5f, 3.5f, 0f);
        capsule.AddComponent<CapsuleControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnClick()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}
}
