using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public List<GameObject> objectivesToShatter;
    public string nextSceneName;
    public GameObject player;

    void Start()
    {
        if (!GameController.getInstance().isTutorial)
        {
            Destroy(this);
        } else
        {
            if (player == null)
            {
                Debug.LogError("PLAYER IS NOT SET IN INSPECTOR");
            }
            if (objectivesToShatter.FindAll(GameObjectIsStillHere).Count == 0)
            {
                Debug.LogError("Something is wrong with tutorial objectives: check inspector for TutorialController.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectivesToShatter.FindAll(GameObjectIsStillHere).Count == 0 && !GameUtils.IsGamePaused() && !GameUtils.IsGameWon() && GameUtils.GetElapsedTime() > 1f)
        {
            winTutorial();
        }
    }

    private bool GameObjectIsStillHere(GameObject go) {
        return go != null;
    }

    private void winTutorial()
    {
        DestroyImmediate(player);
        GameUtils.GameWon();
    }

    // Usable for next level button
    public void StartScene()
    {
        GameUtils.StartScene(nextSceneName);
    }
}
