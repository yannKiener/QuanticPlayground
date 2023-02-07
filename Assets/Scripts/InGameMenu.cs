using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject IngameMenu;

    private bool isMenuActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        IngameMenu.SetActive(isMenuActivated);
    }

    // Update is called once per frame
    void Update()
    {
        IngameMenu.SetActive(isMenuActivated);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchMenuActivation();
        }
    }

    public void SwitchMenuActivation()
    {
        isMenuActivated = !isMenuActivated;
        GameUtils.PauseGame(isMenuActivated);
        if (isMenuActivated)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
