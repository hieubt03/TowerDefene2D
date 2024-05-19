using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    private bool initializeDataIfNull = true;
    public string fileName;
    private bool useEncryption = false;
    public GameData gameData;
    public LevelData levelData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    private string selecedtLevelId = "";
    public static DataPersistenceManager instance {
        get;
        private set;
    }
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.StartListening("TriggerSaveGame", TriggerSaveGame);
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventManager.StopListening("TriggerSaveGame", TriggerSaveGame);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void ChangeSelectedProfileId(string newLevelId) {
        selecedtLevelId = newLevelId;
        LoadGame();
    }
    public void NewLevelData() {
        levelData = new LevelData();
    }

    public void NewGameData() {
        gameData = new GameData();
    }

    public void LoadGame() {
        //Load level data
        levelData = fileDataHandler.LoadLevelData(selecedtLevelId);
        if (levelData == null && initializeDataIfNull) {
            NewLevelData();
        }
        if (levelData == null) {
            Debug.Log("No data was found.");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(levelData);
        }
        //Load game data
        gameData = fileDataHandler.LoadGameData();
        if (gameData == null) {
            NewGameData();
        }
    }
    private void SaveGame() {
        if (levelData == null) {
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.SaveData(levelData);
        }
        fileDataHandler.SaveLevelData(levelData, selecedtLevelId);
        fileDataHandler.SaveGameData(gameData);
    }
    private void TriggerSaveGame(GameObject gameObj, string param) {
        SaveGame();
    }
    private void OnApplicationQuit() {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public Dictionary<string, LevelData> GetAllLevelsGameData() {
        return fileDataHandler.LoadAllLevels();
    }
}
