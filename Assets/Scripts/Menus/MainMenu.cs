using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _prefabButton;
    [SerializeField] private Transform _buttonContainer;

    private void Start()
    {
        Button playButton = Instantiate(_prefabButton, _buttonContainer);
        playButton.GetComponentInChildren<TMP_Text>().text = "Play";
        playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        playButton.gameObject.SetActive(true);
        
        Button quitButton = Instantiate(_prefabButton, _buttonContainer);
        quitButton.GetComponentInChildren<TMP_Text>().text = "Quit";
        quitButton.onClick.AddListener(() => OnQuitButtonClick());
        quitButton.gameObject.SetActive(true);
        quitButton.transform.position += new Vector3(0, quitButton.GetComponent<RectTransform>().sizeDelta.y, 0);
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
