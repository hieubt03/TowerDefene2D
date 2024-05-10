using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTree : MonoBehaviour
{
    [HideInInspector]
    public Tower myTower;
    
    void Start() {
        Debug.Assert(myTower, "Wrong initial parameters");
    }

    public void Build(GameObject prefab) {
        AudioManager.instance.PlaySfx("TowerLevelUp");
        myTower.BuildTower(prefab);
    }
    public void Sell(GameObject prefab) {
        AudioManager.instance.PlaySfx("SellTower");
        myTower.SellTower(prefab);
    }
}
