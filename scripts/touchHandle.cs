using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class touchHandle : MonoBehaviour
{
    Vector3 worldPos;
    Plane plane = new Plane(Vector3.down, 0);
    public string sceneName;
    public bool title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (title && touch.phase == TouchPhase.Ended)
            {
                SceneManager.LoadScene(sceneName);
            }
            else if(!title)
            {
                this.transform.position = Input.mousePosition;

                Ray ray = Camera.main.ScreenPointToRay(this.transform.position);
                if(plane.Raycast(ray, out float distance))
                {
                    worldPos = ray.GetPoint(distance);
                }

                this.transform.position = worldPos;
                Vector3 p = new Vector3(this.transform.position.x, 0.575f, this.transform.position.z);
                this.transform.position = p;
            }
            
        }
    }
}
