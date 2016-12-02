using UnityEngine;
using System.Collections;

public class MainMenuPanelCameraFollow : MonoBehaviour {
   
    
    public GameObject mainMenuPanel;
    public Transform target;
    // Use this for initialization
    void Start () {
       
       mainMenuPanel.transform.LookAt(target);
        mainMenuPanel.transform.Rotate(0, 180, 0);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
