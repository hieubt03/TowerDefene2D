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
        if (DataPersistenceManager.instance.projectileData != null) {
            musicSlider.value = DataPersistenceManager.instance.projectileData.sound;
            sfxSlider.value = DataPersistenceManager.instance.projectileData.sfx;
            isMusicOn = DataPersistenceManager.instance.projectileData.isMusicOn;
            isSfxOn = DataPersistenceManager.instance.projectileData.isSfxOn;
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
        if (DataPersistenceManager.instance.projectileData != null) {
            DataPersistenceManager.instance.projectileData.sound = musicSlider.value;
            DataPersistenceManager.instance.projectileData.sfx = sfxSlider.value;
            DataPersistenceManager.instance.projectileData.isMusicOn = isMusicOn;
            DataPersistenceManager.instance.projectileData.isSfxOn = isSfxOn;
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
