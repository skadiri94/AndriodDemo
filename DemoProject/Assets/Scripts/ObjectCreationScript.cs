using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ObjectCreationScript : MonoBehaviour
{
    
    GameObject plane;
    GameObject cube;
    GameObject sphere;
    GameObject capsule;
    Material grass;
    GameObject gestureIndentifier;
    GameObject touchManager;
    GameObject buttonGameObject;
    Canvas canvas;
    GameObject resetButton;


    // Start is called before the first frame update
    void Start()
    {
        //Adding Eventsystem.
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        //Canvas & button creation
        buttonGameObject = new GameObject("Button Canvas");
        buttonGameObject.AddComponent<Canvas>();


        canvas = buttonGameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.gameObject.layer = LayerMask.NameToLayer("UI");
        canvas.transform.position = new Vector3(960, 540, 0);
        buttonGameObject.AddComponent<CanvasScaler>();
        buttonGameObject.AddComponent<GraphicRaycaster>();

        resetButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        resetButton.transform.SetParent(canvas.transform, false);
        RectTransform rt = resetButton.GetComponent<Button>().GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(250, 120);
        rt.anchoredPosition = new Vector3(-810, 465, -18);
        resetButton.GetComponent<Button>().GetComponent<Image>().sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd"); ;
        resetButton.GetComponent<Button>().onClick.AddListener(resetGame);
   
        resetButton.GetComponentInChildren<Text>().text = "Reset";
        resetButton.GetComponentInChildren<Text>().fontSize = 50;




        Camera.main.gameObject.AddComponent<CameraControl>();

    

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

    void resetGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
