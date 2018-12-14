using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	public GameObject[] Prefabs;
	public int PrefabNum;
	
	void OnGUI() {
		GUI.Label(new Rect(25f,10f, 320f,20f), "Houses");
		PrefabNum = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(25f,30f, 150f,20f), PrefabNum, 100f, 10000f));
		GUI.Label(new Rect(180f,25f, 50f,20f), PrefabNum.ToString());
		if(GUI.Button(new Rect(220f,25f, 140f,20f), "Populate (randomly)"))
		{
			GenerateLevel();
		}
	}
	
	void Start() {
		GenerateLevel();
	}
	
	void GenerateLevel()
	{
		IOCcam ioc = Camera.main.transform.GetComponent<IOCcam>();
		ioc.enabled = false;
		Vector3 prefabPos;
		GameObject go;
		GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach(GameObject g in gos)
		{
			if(g.layer == 8 & g.tag == "ioc")
			{
				Destroy(g);
			}
		}
		for (var i = 0; i < PrefabNum; i++)
		{
			prefabPos = new Vector3(Random.Range(50f, 950f), 0f, Random.Range(50f, 950f));
			go = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], prefabPos, Quaternion.identity) as GameObject;
			go.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
		}
		ioc.enabled = true;
	}
}
