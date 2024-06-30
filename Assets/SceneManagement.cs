using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
	[SerializeField] private ObjectDataSaver saveManager;
	[SerializeField] private ObjectDataLoader loadManager;

	[SerializeField] private string saveSceneName;
	[SerializeField] private string loadSceneName;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			SceneManager.LoadSceneAsync(loadSceneName);
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			SceneManager.LoadSceneAsync(saveSceneName);
		}
		else if(Input.GetKeyDown(KeyCode.Return))
		{
			if(SceneManager.GetActiveScene().name == loadSceneName)
			{
				loadManager.Load();
			}
			else
			{
				saveManager.Save();
			}
		}
	}
}
