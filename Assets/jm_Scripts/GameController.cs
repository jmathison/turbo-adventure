using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public List<GameObject> players;
	public Text characterTimer;
	public Text nextCharacter;

    private int characterSpawnWait = 15;
    private int alertTime = 5;

    public TutorialManager tutorialManager;
    public GameObject blankExplainer;

	private float initXPos = -0.5f;

    private int deadPlayers = 0;
    public ks_code_score scoreScript;
	// Use this for initialization
	void Start ()
	{
        StartCoroutine(LevelTransition());
	}

    //This is not working because of the time.timescale bidness.
    IEnumerator LevelTransition()
    {
        tutorialManager.NoNextButton();
        ExplainThing(blankExplainer, "Get Ready", new Vector2(0, 0));
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(1.0f);
        tutorialManager.EndExplanationNoButton();

        ExplainThing(blankExplainer, "Go!", new Vector2(0, 0));
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(1f);
        tutorialManager.EndExplanationNoButton();
        //Possibly start the score here.
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StartSpawningPlayers());
    }

    IEnumerator GameOverTransition()
    {
        scoreScript.StopIncreasingScore();
        tutorialManager.NoNextButton();
        ExplainThing(blankExplainer, "Game Over", new Vector2(0, 300));
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(2.0f);
        tutorialManager.EndExplanationNoButton();
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevel(2);
    }

    IEnumerator StartSpawningPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(i == 0)
            {
                SpawnPlayers(i);
            }
            else
            {
                yield return new WaitForSeconds(characterSpawnWait);
                SpawnPlayers(i);
            }
        }
    }

    public void PlayerDeath()
    {
        if (deadPlayers < 3)
            deadPlayers++;
        else
            StartCoroutine(GameOverTransition());
    }

    void SpawnPlayers(int playerToSpawn)
    {
        PlayerController thePlayer = players[playerToSpawn].GetComponent<PlayerController>();
        thePlayer.Spawn(initXPos);
        initXPos += 2.0f;
    }

    private void ExplainThing(GameObject thing, string explanation, Vector2 offset)
    {
        thing.GetComponent<ExplanationManager>().SetExplanation(explanation);
        tutorialManager.BeginExplanation(thing, offset);
    }
}
