using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _prefabButton;
    [SerializeField] private Transform _buttonContainer;

    public static bool PVP = false;

    private void Start()
    {
        Button versusButton = Instantiate(_prefabButton, _buttonContainer);
        versusButton.GetComponentInChildren<TMP_Text>().text = "VERSUS";
        versusButton.onClick.AddListener(() => { PVP = true;  SceneManager.LoadScene(1); });
        versusButton.gameObject.SetActive(true);
        
        Button cpuButton = Instantiate(_prefabButton, _buttonContainer);
        cpuButton.GetComponentInChildren<TMP_Text>().text = "VS CPU";
        cpuButton.onClick.AddListener(() => { PVP = false;  SceneManager.LoadScene(1); });
        cpuButton.gameObject.SetActive(true);
        cpuButton.transform.position -= new Vector3(0, cpuButton.GetComponent<RectTransform>().sizeDelta.y, 0);

        Button quitButton = Instantiate(_prefabButton, _buttonContainer);
        quitButton.GetComponentInChildren<TMP_Text>().text = "Quit";
        quitButton.onClick.AddListener(() => OnQuitButtonClick());
        quitButton.gameObject.SetActive(true);
        quitButton.transform.position -= new Vector3(0, quitButton.GetComponent<RectTransform>().sizeDelta.y * 2, 0);
    }

    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
