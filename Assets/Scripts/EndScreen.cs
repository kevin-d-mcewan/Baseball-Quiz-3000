using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake( )
    {

        /*scoreKeeper = FindObjectOfType<ScoreKeeper>();        ***OBSOLETE*** */
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();

    }


    public void ShowFinalScore( )
    {

        finalScoreText.text = "Congratulations! \n You Scored " + scoreKeeper.CalculateScore() + "%";


    }


}