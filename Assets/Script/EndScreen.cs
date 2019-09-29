using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void menuselect()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }


}
