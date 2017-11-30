using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour {

	public static PlayerHealthController Instance { get; set;}
	[SerializeField] private AudioClip eatingSound;
	[SerializeField] private AudioClip drinkSound;
	public float health = 100f;
	public float hunger = 100f;
	public float thirst = 100f;
	public float dream = 100f;

	public float hungerMult=0.25f;
	public float thirstMult=0.15f;
	public float dreamMult=0.1f;

	AudioSource audioSource;
	Color bgColor;
	Image healthBar, hungerBar, thirstBar, dreamBar, BackgroundImg;

	void Start () {
		Instance = this;
		audioSource = GetComponent<AudioSource> ();
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();
		hungerBar = GameObject.Find ("HungerBar").GetComponent<Image> ();
		thirstBar = GameObject.Find ("ThirstBar").GetComponent<Image> ();
		dreamBar = GameObject.Find ("DreamBar").GetComponent<Image> ();
		BackgroundImg = GameObject.Find ("BGImage").GetComponent<Image> ();
		BackgroundImg.enabled = false;
		bgColor.a = 0f;
	}

	void Update()
	{
		CheckDeath ();
		//голод
		if (hunger > 0)
			hunger -= Time.deltaTime * hungerMult;
		else
			health -= Time.deltaTime * hungerMult * 5;
		//жажда
		if (thirst > 0)
			thirst -= Time.deltaTime * thirstMult;
		else
			health -= Time.deltaTime * thirstMult * 5;
		//сон
		if (dream > 0)
			dream -= Time.deltaTime * dreamMult;
		else
			InteractMenuImage.iMenuImg.Activate (7);

		healthBar.fillAmount = health / 100;
		hungerBar.fillAmount = hunger / 100;
		thirstBar.fillAmount = thirst / 100;
		dreamBar.fillAmount = dream / 100;

		//для финальной заставки (когда персонаж погиб)
		if(BackgroundImg.enabled) {
			bgColor += Color.Lerp (Color.clear, Color.black, Time.deltaTime * 0.07f);
			BackgroundImg.color = bgColor;
			if (bgColor.a >=1)
				InteractMenuImage.iMenuImg.Activate (11);
			//SceneManager.LoadScene ("mainMenu");
		}
	}

	public void AddHunger(float value)
	{
		hunger += value;
		UpdateHealthLimits ();
		audioSource.clip = eatingSound;
		audioSource.Play();
	}

	public void AddThirst(float value)
	{
		thirst += value;
		UpdateHealthLimits ();
		audioSource.clip = drinkSound;
		audioSource.Play();
	}

	public void UpdateHealthLimits()
	{
		if (health > 100)
			health = 100f;
		if (hunger > 100)
			hunger = 100f;
		if (thirst > 100)
			thirst = 100f;
		if (dream > 100)
			dream = 100f;
	}

	public void CheckDeath()
	{
		if (health <= 0)
			DeathAnimation ();
		else if (health <= 10 && health > 0) {
			health -= Time.deltaTime * 0.02f;
			InteractMenuImage.iMenuImg.Activate (8);
			//Debug.Log ("Персонаж в критическом состоянии. Жизнь тает на глазах!");
		} else if (health <= 20 && health > 10)
			InteractMenuImage.iMenuImg.Activate (9);
			//Debug.Log ("У персонажа очень мало здоровья.");
	}

	public void DeathAnimation()
	{
		BackgroundImg.enabled = true;
		InteractMenuImage.iMenuImg.Activate (10);
		//Debug.Log ("Персонаж погиб!");

	}
}
