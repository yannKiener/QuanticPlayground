using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject IngameMenu;

    public GameObject skipTutorialGameObject;
    public GameObject randomLevelGameObject;
    public GameObject InputFieldButton;
    public GameObject InputField;


    private bool isMenuActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        IngameMenu.SetActive(isMenuActivated);
        if (GameController.getInstance().isTutorial)
        {
            skipTutorialGameObject.SetActive(true);
            randomLevelGameObject.SetActive(false);
            InputFieldButton.SetActive(false);
            InputField.SetActive(false);
        } else
        {
            randomLevelGameObject.SetActive(true);
            InputFieldButton.SetActive(true);
            InputField.SetActive(true);
            skipTutorialGameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IngameMenu.SetActive(isMenuActivated);

        //Elapsed time to avoid menu while pre-game
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && GameUtils.GetElapsedTime() > 0.1f)
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
