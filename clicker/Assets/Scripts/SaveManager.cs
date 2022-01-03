using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance { get; private set; } = new SaveManager();

	public SaveState state;

	float elapsed = 0f;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Instance = this;
		Load();

		Debug.Log(Helper.Serialize<SaveState>(state));
		// Save path !!! Debug.Log(Application.persistentDataPath);
		/*Debug.Log(state.score);*/
	}

	public void Save()
    {
		PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

	public void Load()
    {
		if (PlayerPrefs.HasKey("save"))
        {
			state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        } else
        {
			state = new SaveState();
			Save();
        }
    }

    private void Update()
    {
		elapsed += Time.deltaTime;
		if (elapsed >= 30f)
		{
			elapsed %= 30f;
			Debug.Log(Helper.Serialize<SaveState>(state));
		}
	}
}
