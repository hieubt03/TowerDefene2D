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
    private GameData gameData;
    public ProjectileData projectileData;
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
    public void NewGameData() {
        gameData = new GameData();
    }

    public void NewProjectileData() {
        projectileData = new ProjectileData();
    }

    public void LoadGame() {
        //Load star of levelSelectId
        gameData = fileDataHandler.LoadLevelData(selecedtLevelId);
        if (gameData == null && initializeDataIfNull) {
            NewGameData();
        }
        if (gameData == null) {
            Debug.Log("No data was found.");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
        //load projectile data
        projectileData = fileDataHandler.LoadProjectileData();
        if (projectileData == null) {
            NewProjectileData();
        }
    }
    private void SaveGame() {
        if (gameData == null) {
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.SaveData(gameData);
        }
        fileDataHandler.SaveLevelData(gameData, selecedtLevelId);
        // save projectile data
        fileDataHandler.SaveProjectileData(projectileData);
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
    public Dictionary<string, GameData> GetAllLevelsGameData() {
        return fileDataHandler.LoadAllLevels();
    }
}
