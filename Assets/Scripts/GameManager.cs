using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Need access to these items/scripts
    Quiz quiz;
    EndScreen endScreen;

    private void Awake( )
    {
        /*quiz = FindObjectOfType<Quiz>();                   ***OBSOLETE***  */
        quiz = FindFirstObjectByType<Quiz>();
        /*endScreen = FindObjectOfType<EndScreen>();        ***OBSOLETE***  */
        endScreen = FindFirstObjectByType<EndScreen>();
    }

    void Start( )
    {



        // When game starts we need 'Quiz' screen visable and 'endScreen' not & in Game Over 'endScreen' visable & 'Quiz' screen not visible
        quiz.gameObject.SetActive( true );
        endScreen.gameObject.SetActive( false );

    }


    void Update( )
    {
        // Checking to See if Game is Complete to Set GameOver Screen to Active
        if ( quiz.isComplete == true )
        {
            quiz.gameObject.SetActive( false );
            endScreen.gameObject.SetActive( true );
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel( )
    {
        // Will load the game scene back up after hitting the button to continue
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
