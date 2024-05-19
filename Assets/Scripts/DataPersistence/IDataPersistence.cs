using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(LevelData data);
    void SaveData(LevelData data);
}
