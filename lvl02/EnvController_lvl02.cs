using UnityEngine;
using System.Collections;

public class EnvController_lvl02 : MonoBehaviour {

	public static EnvController_lvl02 Instance { get; set;}

	public AudioClip[] _bgMusic;

	GameObject[] Structures = new GameObject[1];
	public ParticleSystem partices;

	Vector3 posSun;
	Terrain terra;
	[HideInInspector]public AudioSource source;
	public bool isDefaultMusic = true;

	void Start () {
		Instance = this;
		terra = Terrain.activeTerrain;
		source = GetComponent<AudioSource> ();
		source.volume = PlayerPrefs.GetFloat ("musicVolume");
		Structures[0] = Resources.Load<GameObject> ("Prefabs/Trees/tree01");

		//CreateAnEnvironment ();
	}
	void Update()
	{
		TurnOnOffStars ();
		PlayMusic ();
	}

	void PlayMusic()
	{
		int x = 0;
		if(!source.isPlaying && isDefaultMusic)
		{
			if (x > 2)
				x = 0;
			source.clip = _bgMusic [x];
			source.Play ();
			x++;
		}
	}

/*	void CreateAnEnvironment()
	{
		#region деревья
		int treeNo;
		int randomCount = Random.Range (10, 20);

		for (int i = 0; i < randomCount; i++) {
			treeNo = Random.Range (0, Structures.Length);
			Vector3 randomPos = (Random.insideUnitSphere + Vector3.up * 2) * 1.5f;

			SpawnObjects (treeNo, randomPos, Quaternion.identity);
		}
		#endregion
	}

	void SpawnObjects (int index, Vector3 pos, Quaternion rotation)
	{
		Transform centerPt = GameObject.Find("_CenterOfMap").transform;
		Vector3 posOnTerra = new Vector3(pos.x, terra.SampleHeight (pos),pos.z);
		Instantiate (Structures [index], posOnTerra, rotation);

	}*/

	void TurnOnOffStars()
	{
		if(!SunRotation.Instance.IsDayNow())
		{
			partices.Simulate(30);
		}
		else
		{
			partices.Stop();

		}
		//Debug.Log (partices.isStopped);

	}
}


