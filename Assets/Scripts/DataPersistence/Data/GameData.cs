using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public float sound;
    public float sfx;
    public bool isMusicOn;
    public bool isSfxOn;
    public string curentSelectedProtectile;
    public int totalPoint;
    public int arrowDamageScale;
    public int arrowSpeed;
    public int arrowAttackSpeed;
    public int bulletDamageScale;
    public int bulletSpeed;
    public int bulletAttackSpeed;
    public int bombDamageScale;
    public int bombSpeed;
    public int bombAttackSpeed;
    public int lightballDamageScale;
    public int lightbalSpeed;
    public int lightballAttackSpeed;
    public GameData() {
        isMusicOn = true;
        isSfxOn = true;
        sound = 0.5f;
        sfx = 0.5f;
        totalPoint = 0;
        arrowDamageScale = 0;
        arrowAttackSpeed = 0;
        arrowSpeed = 0;
        bulletAttackSpeed = 0;
        bulletDamageScale = 0;
        bulletSpeed = 0;
        bombAttackSpeed = 0;
        bombDamageScale = 0;
        bombSpeed = 0;
        lightballAttackSpeed = 0;
        lightbalSpeed = 0;
        lightballDamageScale = 0;
    }
    public void SpendPoint() {
        totalPoint -= 1;
        EventManager.TriggerEvent("SpendTotalPoint", null, null);
    }
}
