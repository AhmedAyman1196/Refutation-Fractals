using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenusScript : MonoBehaviour {


	public Button resume, upgrade, quit;
	public Button movUp, attackUp, healthUp , back;
	public Button restart ,quit2 ;

	public GameObject pauseMenu , upgradeMenu , gameoverMenu ;

	// Use this for initialization
	void Start () {
		resume.onClick.AddListener(onResume);
		upgrade.onClick.AddListener(onUpgrade);
		quit.onClick.AddListener(onQuit);
		movUp.onClick.AddListener(onMovUP);
		attackUp.onClick.AddListener(onattackUp);
		healthUp.onClick.AddListener(onhealthUp);
		back.onClick.AddListener(onBack);
		restart.onClick.AddListener(onRestart);
		quit2.onClick.AddListener(onQuits);
	}
    public void onQuits(){
		onQuit();
	}
	public void onRestart(){
        Debug.Log("Upgraaaaakjfnwjiniefwaaaaaaade");
        Debug.Log("Upgraaaaaafwkenfinwiefniaaaaaade");
        KhattabController.paused = false;
        SceneManager.LoadScene("Level1");
	}
	void onResume(){
		KhattabController.paused = false ;
    }
	void onUpgrade(){
        Debug.Log("Upgraaaaaa8878777777777777777777777777aaaade");
        resume.gameObject.SetActive(false);
        upgrade.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);

	}
    public void onQuit(){
		SceneManager.LoadScene("Main Menu");
	}

	void onMovUP(){
		if(KhattabController.skillPoints>0){
			KhattabController.runSpeed = KhattabController.runSpeed +0.1f ;
			KhattabController.skillPoints = KhattabController.skillPoints-1;
		}
	}
	void onattackUp(){
		if(KhattabController.skillPoints>0){
			KhattabController.attack = KhattabController.attack +1 ;
			KhattabController.skillPoints = KhattabController.skillPoints-1;
		}
	}
	void onhealthUp(){
		if(KhattabController.skillPoints>0){
			KhattabController.maxHealth = KhattabController.maxHealth +10 ;
			KhattabController.health = KhattabController.maxHealth ;
			KhattabController.skillPoints = KhattabController.skillPoints-1;
		}
	}
	void onBack(){
        resume.gameObject.SetActive(true);
        upgrade.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        pauseMenu.active = true ;
		upgradeMenu.active = false ;
	}
	// Update is called once per frame
	void Update () {

	}
}
