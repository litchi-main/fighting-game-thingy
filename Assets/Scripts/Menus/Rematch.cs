using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rematch : MonoBehaviour
{
    [SerializeField] private Button _prefabButton;
    [SerializeField] private Transform _buttonContainer;

    public static bool PVP = false;

    private void Start()
    {
        Button versusButton = Instantiate(_prefabButton, _buttonContainer);
        versusButton.GetComponentInChildren<TMP_Text>().text = "Rematch";
        versusButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        versusButton.gameObject.SetActive(true);

        Button quitButton = Instantiate(_prefabButton, _buttonContainer);
        quitButton.GetComponentInChildren<TMP_Text>().text = "Main Menu";
        quitButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        quitButton.gameObject.SetActive(true);
        quitButton.transform.position -= new Vector3(0, quitButton.GetComponent<RectTransform>().sizeDelta.y , 0);
    }
}
