using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject defeatMenu;
    public GameObject victoryMenu;
    public GameObject levelUI;
    public TextMeshProUGUI goldAmount;
    public TextMeshProUGUI healthPoint;
    public TextMeshProUGUI waveNumber;
    public LevelStar star;
    private bool paused = false;

    void OnEnable() {
        EventManager.StartListening("ZombieDead", ZombieDead);
    }

    void OnDisable() {
        EventManager.StopListening("ZombieDead", ZombieDead);
    }

    void Awake() {
        Debug.Assert(levelUI && goldAmount, "Wrong initial parameters");
    }

    void Start() {
        GoToLevel();
    }

    void Update() {
        if (paused == false) {       
            if (Input.GetMouseButtonDown(0) == true) {
                // Check if pointer over UI components
                GameObject hittedObj = null;
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                foreach (RaycastResult res in results) {
                    if (res.gameObject.CompareTag("ActionIcon") || res.gameObject.CompareTag("SellIcon")) {
                        hittedObj = res.gameObject;
                        break;
                    }
                }
                if (results.Count <= 0) {
                    // Check if pointer over colliders
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
                    foreach (RaycastHit2D hit in hits) {
                        // If this is tower collider
                        if (hit.collider.gameObject.CompareTag("Tower"))
                        {
                            hittedObj = hit.collider.gameObject;
                            break;
                        }
                    }
                }
                EventManager.TriggerEvent("UserClick", hittedObj, null);
            }
        }
    }

    private void LoadScene(string sceneName) {
        EventManager.TriggerEvent("SceneQuit", null, null);
        EventManager.TriggerEvent("TriggerSaveGame", null, null);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToMainMenu() {
        LoadScene("SelectLevel");
    }

    private void CloseAllUI() {
        pauseMenu.SetActive(false);
        defeatMenu.SetActive(false);
        victoryMenu.SetActive(false);
        levelUI.SetActive(false);
    }

    private void PauseGame(bool pause) {
        paused = pause;
        // Stop the time on pause
        Time.timeScale = pause ? 0f : 1f;
        EventManager.TriggerEvent("GamePaused", null, pause.ToString());
    }

    public void GoToPauseMenu() {
        PauseGame(true);    
        pauseMenu.SetActive(true);
    }

    public void GoToLevel() {
        CloseAllUI();
        levelUI.SetActive(true);
        PauseGame(false);
    }

    public void GoToDefeatMenu() {
        AudioManager.instance.PlaySfx("GameOver");
        PauseGame(true);
        defeatMenu.SetActive(true);
    }

    public void GoToVictoryMenu() {
        AudioManager.instance.PlaySfx("LevelComplete");
        PauseGame(true);
        int currentHealthPoint = GetHealthPoint();
        star.SetSprite(currentHealthPoint);
        if (DataPersistenceManager.instance.levelData.levelStar < currentHealthPoint) {
            star.SetStar(currentHealthPoint);
            int gap = currentHealthPoint / 5 - DataPersistenceManager.instance.levelData.levelStar / 5;
            switch (gap) {
                case 3:
                    DataPersistenceManager.instance.gameData.totalPoint += 9;
                    break;
                case 2:
                    DataPersistenceManager.instance.gameData.totalPoint += 6;
                    break;
                case 1:
                    DataPersistenceManager.instance.gameData.totalPoint += 3;   
                    break;
                case 0:
                    break;
                }
            EventManager.TriggerEvent("TriggerSaveGame", null, null);
        }   
        victoryMenu.SetActive(true);
    }

    public void RestartLevel() {
        string activeScene = SceneManager.GetActiveScene().name;
        LoadScene(activeScene);
    }

    private int GetGold() {
        int gold;
        int.TryParse(goldAmount.text, out gold);
        return gold;
    }

    private void SetGold(int gold) {
        goldAmount.text = gold.ToString();
    }

    public void AddGold(int gold) {
        SetGold(GetGold() + gold);
    }

    public bool SpendGold(int cost) {
        bool res = false;
        int currentGold = GetGold();
        if (currentGold >= cost) {
            SetGold(currentGold - cost);
            res = true;
        }
        return res;
    }

    private int GetHealthPoint() {
        int currentHealthPoint;
        int.TryParse(healthPoint.text, out currentHealthPoint);
        return currentHealthPoint;
    }

    private void SetHealthPoint(int currentHealthPoint) {
        healthPoint.text = currentHealthPoint.ToString();
    }

    public void LoseHealthPoint() {
        int currentHealthPoint = GetHealthPoint();
        if (currentHealthPoint > 1) {
            SetHealthPoint(currentHealthPoint - 1);
        }
        if (currentHealthPoint == 1) {
            GoToDefeatMenu();
        }
    }
    public int GetWaveNumber() {
        int currentWave;
        int.TryParse(waveNumber.text, out currentWave);
        return currentWave;
    }
    public void SetWave(int currentWave) {
        waveNumber.text = currentWave.ToString();
    }

    private void ZombieDead(GameObject gameObject, string param) {
        if (gameObject.CompareTag("Zombie")) {
            Price price = gameObject.GetComponent<Price>();
            if (price != null) {
                AddGold(price.price);
            }
        }
    }
}
