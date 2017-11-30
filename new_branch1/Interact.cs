using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interact : MonoBehaviour {

	public static Interact Instance { get; set;}
	public float interactRange = 3f;

	string previousLevel;
	string currentLevel;
	Vector3 posOfEnter, rotOfEnter;

	GameObject player;
	public Text timeText, attentionText;

	Ray ray;
	RaycastHit hit;
	void Start(){
		Instance = this;
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		//взаимодействие с уровнями
		previousLevel = PlayerPrefs.GetString("prevLevel");
		PlayerPrefs.SetString ("prevLevel", null);
		currentLevel = SceneManager.GetActiveScene().name;

		if (currentLevel == "Level01" && previousLevel == "Interior") {
			posOfEnter = new Vector3 (PlayerPrefs.GetFloat ("posX"), PlayerPrefs.GetFloat ("posY"), PlayerPrefs.GetFloat ("posZ"));
			rotOfEnter = new Vector3 (0, PlayerPrefs.GetFloat ("rotY") - 180, 0);
			player.transform.position = posOfEnter;
			player.transform.rotation = Quaternion.Euler (rotOfEnter);
		}
	}

	void Update(){
		ray = Camera.main.ScreenPointToRay (new Vector2 (Screen.width / 2, Screen.height / 2));
	
		if (Physics.Raycast (ray, out hit, interactRange)) {

			//взаимодействие с недвижимостью (вход в здание, ремонт, анализ, осмотр)
			if (hit.collider.tag == "House") {
				print ("Войти в дом (выйти)");
				//вход и выход из дома
				if (Input.GetKeyDown (KeyCode.E)) {
					PlayerPrefs.SetString ("prevLevel", SceneManager.GetActiveScene ().name);
					if (SceneManager.GetActiveScene ().name == "Level01") {
						PlayerPrefs.SetFloat ("posX", player.transform.position.x);
						PlayerPrefs.SetFloat ("posY", player.transform.position.y);
						PlayerPrefs.SetFloat ("posZ", player.transform.position.z);
						PlayerPrefs.SetFloat ("rotY", player.transform.rotation.eulerAngles.y);
						EnterBuilding ();
					} else
						ExitBuilding ();
				}
			}
		}

		Debug.DrawRay (ray.origin, ray.direction * interactRange, Color.yellow);
		//timeText.text = "День: " + SunRotation.Instance.GetGameDay () + "-й. Время: " + SunRotation.Instance.GetGameTime () + " часов";
	}

	public void EnterBuilding() {
		SceneManager.LoadScene("Interior");
	}

	public void ExitBuilding() {
		SceneManager.LoadScene("Level01");
	}
}
