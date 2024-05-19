using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUiManager : MonoBehaviour
{
    public GameObject soundUi;
    public Image musicIcon;
    public Image sfxIcon;
    private bool isMusicOn;
    private bool isSfxOn;
    public List<Sprite> sprites;
    public Slider musicSlider, sfxSlider;

    void OnEnable() {
        if (DataPersistenceManager.instance.gameData != null) {
            musicSlider.value = DataPersistenceManager.instance.gameData.sound;
            sfxSlider.value = DataPersistenceManager.instance.gameData.sfx;
            isMusicOn = DataPersistenceManager.instance.gameData.isMusicOn;
            isSfxOn = DataPersistenceManager.instance.gameData.isSfxOn;
            if (isMusicOn) {
                musicIcon.sprite = sprites[0];
            } else {
                musicIcon.sprite = sprites[1];
            }
            if (isSfxOn) {
                sfxIcon.sprite = sprites[2];
            } else {
                sfxIcon.sprite = sprites[3];
            }
        }   
    }
    void OnDisable () {
        if (DataPersistenceManager.instance.gameData != null) {
            DataPersistenceManager.instance.gameData.sound = musicSlider.value;
            DataPersistenceManager.instance.gameData.sfx = sfxSlider.value;
            DataPersistenceManager.instance.gameData.isMusicOn = isMusicOn;
            DataPersistenceManager.instance.gameData.isSfxOn = isSfxOn;
        }
    }

    public void ToggleMusic() {
        isMusicOn = !isMusicOn;
        if (isMusicOn) {
            musicIcon.sprite = sprites[0];
        } else {
            musicIcon.sprite = sprites[1];
        }
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSfx() {
        isSfxOn = !isSfxOn;
        if (isSfxOn) {
            sfxIcon.sprite = sprites[2];
        } else {
            sfxIcon.sprite = sprites[3];
        }
        AudioManager.instance.ToggleSfx();
    }

    public void MusicVolume() {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void SfxVolume() {
        AudioManager.instance.SfxVolume(sfxSlider.value);
    }

    public void GotoSoundMenu() {
        soundUi.SetActive(true);
    }

    public void ExitSoundMenu() {
        soundUi.SetActive(false);
    }
}
