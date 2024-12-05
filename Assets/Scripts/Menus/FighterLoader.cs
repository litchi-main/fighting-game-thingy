using UnityEngine;

public class FighterLoader : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerInputController _p1HumanInputs;
    [SerializeField] private PlayerInputController _p2HumanInputs;

    public GameObject P1;
    public GameObject P2;

    public void Awake()
    {
        generateHumanPlayer(out P1, true);
        if (MainMenu.PVP)
            generateHumanPlayer(out P2, false);
        else
            generateCPUPlayer(out P2, false);

        P1.GetComponent<Player>().opponent = P2.GetComponent<Player>();
        P2.GetComponent<Player>().opponent = P1.GetComponent<Player>();

        Destroy(_p1HumanInputs);
        Destroy(_p2HumanInputs);

        //main camera, health bar left or right, camera in pixel converter
    }

    public void generateHumanPlayer(out GameObject player, bool isP1)
    {
        player = (GameObject)_playerPrefab.GetComponent<Player>().Clone();
        player.AddComponent<PlayerInputController>();
        player.GetComponent<Player>().inputSource = player.GetComponent<BaseInputReader>();
        if (isP1)
        {
            player.GetComponent<PlayerInputController>().ButtonConfig(_p1HumanInputs.GetButtonConfig());
            player.tag = "P1";
        }
        else
        {
            player.GetComponent<PlayerInputController>().ButtonConfig(_p2HumanInputs.GetButtonConfig());
            player.tag = "P2";
            player.GetComponent<Player>().baseOrientation = true;
        }
    }
    public void generateCPUPlayer(out GameObject player, bool isP1)
    {
        player = (GameObject)_playerPrefab.GetComponent<Player>().Clone();
        player.AddComponent<CPUInputController>();
        player.GetComponent<CPUInputController>()._player = player.GetComponent<Player>();
        player.GetComponent<Player>().inputSource = player.GetComponent<CPUInputController>();
        if (isP1)
        {
            player.tag = "P1";
        }
        else
        {
            player.tag = "P2";
            player.GetComponent<Player>().baseOrientation = true;
        }
    }
}
