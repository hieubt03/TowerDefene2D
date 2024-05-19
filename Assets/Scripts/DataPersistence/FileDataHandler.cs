using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption;
    private string encryptionCodeWord = "abc";
    private string backupExtension = ".bak";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption) {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }
    public LevelData LoadLevelData(string levelId, bool allowRestoreFromBackup = true) {
        if (levelId == null) {
            return null;
        }
        string fullPath = Path.Combine(dataDirPath, levelId, dataFileName);
        LevelData loadedData = null;
        if (File.Exists(fullPath)) {
            try {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (useEncryption) {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadedData = JsonUtility.FromJson<LevelData>(dataToLoad);
            } catch (Exception e) {
                if (allowRestoreFromBackup) {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess) {
                        loadedData = LoadLevelData(levelId, false);
                    }
                } else {
                    Debug.Log("Error when trying  to load file");
                }
            }
        }
        return loadedData;
    }
    
    public GameData LoadGameData(bool allowRestoreFromBackup = true) {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) {
            try {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (useEncryption) {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } catch (Exception e) {
                if (allowRestoreFromBackup) {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess) {
                        loadedData = LoadGameData(false);
                    }
                } else {
                    Debug.Log("Error when trying  to load file");
                }
            }
        }
        return loadedData;
    }
    public void SaveLevelData(LevelData data, string levelId) {
        if (levelId == null) {
            return;
        }
        string fullPath = Path.Combine(dataDirPath, levelId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            if (useEncryption) {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(dataToStore);
                }
            }
            LevelData verifiedGameData = LoadLevelData(levelId);
            if (verifiedGameData != null) {
                File.Copy(fullPath, backupFilePath, true);
            } else {
                throw new Exception("Save file could not be verified, no backup");
            }
        } catch (Exception e) {
            Debug.Log("Error when trying to save data" + "\n" + e);
        }
    }

     public void SaveGameData(GameData data) {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            if (useEncryption) {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(dataToStore);
                }
            }
            GameData verifiedProjectileData = LoadGameData();
            if (verifiedProjectileData != null) {
                File.Copy(fullPath, backupFilePath, true);
            } else {
                throw new Exception("Save file could not be verified, no backup");
            }
        } catch (Exception e) {
            Debug.Log("Error when trying to save data" + "\n" + e);
        }
    }

    public Dictionary<string, LevelData> LoadAllLevels() {
        Dictionary<string, LevelData> levelDictionary = new Dictionary<string, LevelData>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos) {
            string levelId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, levelId, dataFileName);
            if (!File.Exists(fullPath)) {
                Debug.LogWarning("Skipping directory : "+ levelId);
                continue;
            }
            LevelData levelData = LoadLevelData(levelId);
            if (levelData != null) {
                levelDictionary.Add(levelId, levelData);
            } else {
                Debug.LogError("Something went wrong." + levelId);
            }
        }
        return levelDictionary;
    }
    private string EncryptDecrypt(string data) {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
    private bool AttemptRollback(string fullPath) {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try {
            // if the file exists, attempt to roll back to it by overwriting the original file
            if (File.Exists(backupFilePath)) {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Had to roll back to backup file at: " + backupFilePath);
            }
            // otherwise, we don't yet have a backup file - so there's nothing to roll back to
            else {
                throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
            }
        }
        catch (Exception e) {
            Debug.LogError("Error occured when trying to roll back to backup file at: " 
                + backupFilePath + "\n" + e);
        }

        return success;
    }
}
