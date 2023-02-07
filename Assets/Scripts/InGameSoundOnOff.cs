using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSoundOnOff : MonoBehaviour
{

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchSoundOnOff()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            image.sprite = soundOnSprite;
        }
        else
        {
            image.sprite = soundOffSprite;
        }
        MusicManager.SwitchMusic(isSoundOn);

    }
}
