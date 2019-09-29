using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject menu;
    public GameObject level;
    public GameObject caracter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    public void quitgame() {
        Application.Quit();
    }

    public void menuselectplay(){

        menu.SetActive(false);
        level.SetActive(true);
        caracter.SetActive(false);

    }

    public void charselect() {

        menu.SetActive(false);
        level.SetActive(false);
        caracter.SetActive(true);

    }

    public void characterselect1() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void characterselect2() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

}


