using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;
public class GameBehaviour : MonoBehaviour, IManager
{
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;


    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int maxItems = 4;
    public string labelText = "Collect all 4 items and win your freedom!";


    private int _itemCollected = 0;
    //Использование set и get нужно для того, чтобы получить (get) или присвоить (set) данные в (из) приватное(-го) поля  
    public int Items
    {
        get
        {
            return _itemCollected;
        }

        set
        {
            _itemCollected = value;
            if (_itemCollected >= maxItems)
            {

                UpdateLevelState("You found all items", ref showWinScreen, 0f);
                Debug.Log(showWinScreen);
                //labelText = "You found all items";
                //showWinScreen = true;

                //Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemCollected) + " more to go!";
            }
        }

    }

    private int _playerHP = 3;

    public int HP
    {
        get
        {
            return _playerHP;
        }

        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                UpdateLevelState("You want another life with that?", ref showLossScreen, 0f);
                //labelText = "You want another life with that?";
                //showLossScreen = true;
                //Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch...That's got hurt";
            }
            Debug.LogFormat("Lives - {0}", _playerHP);
        }
    }
    // Start is called before the first frame update

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 125, 25), "Player health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 125, 25), "Items collected: " + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 50), "Y O U  W O N !!!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "Y O U  L O S E !!!"))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch(System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene {0}" + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }
            }

        }
    }

    void UpdateLevelState(string text, ref bool screen, float timeScale)
    {
        labelText = text;
        screen = true;
        Time.timeScale = timeScale;
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.setItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "Manager initialized...";
        _state.FancyDebug();
        debug(_state);

        GameObject player = GameObject.Find("Player");

        PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();

        playerBehaviour.playerJump += HandlePlayerJump;

    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
   

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
