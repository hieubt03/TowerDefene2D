using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectsMenu : MonoBehaviour
{
    private LevelSelect[] levelSelects;
    public GameObject upgradeUi;
    public GameObject helpsUi;
    private void Awake() {
        levelSelects = GetComponentsInChildren<LevelSelect>();
    }
    void Start() {
        ActiveMenu();
    }
    public void ActiveMenu() {
        Dictionary<string, LevelData> levelSelectsGameData = DataPersistenceManager.instance.GetAllLevelsGameData();
        foreach (LevelSelect levelSelect in levelSelects) {
            LevelData levelData = null;
            levelSelectsGameData.TryGetValue(levelSelect.GetLevelSelectId(), out levelData);
            levelSelect.SetLevelSelectSprite(levelData);
        }
    }

    public void GotoUpgradeMenu() {
        upgradeUi.SetActive(true);
    }

    public void ExitUpgradeMenu() {
        upgradeUi.SetActive(false);
    }
    public void GotoHelpsMenu() {
        helpsUi.SetActive(true);
    }

    public void ExitHelpsMenu() {
        helpsUi.SetActive(false);
    }

}
