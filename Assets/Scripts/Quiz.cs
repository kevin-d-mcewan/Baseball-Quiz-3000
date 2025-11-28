using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header( "Questions" )]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header( "Answers" )]
    [SerializeField] GameObject [ ] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header( "Button Colors" )]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header( "Timer" )]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header( "Scoring" )]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header( "ProgressBar" )]
    [SerializeField] Slider progressBar;

    // Bool to see if thing is in progress or complete (i.e. gameOver or still going)
    public bool isComplete;


    private void Awake( )
    {
        /* timer = FindObjectOfType<Timer>();               ***OBSOLETE***     */
        // Use this to find the 1st 'Timer' Obj in the Scene
        timer = FindFirstObjectByType<Timer>();

        /* scoreKeeper = FindObjectOfType<ScoreKeeper>();   ***OBSOLETE***     */
        // Use this to find the 1st 'ScoreKeeper'obj in the scene
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Start( )
    {
        // If get NULL EXCEPTION MOVE BELOW ITEMS TO 'Awake()'

        // Makes the max value of Progress bar equal to however many questions we put in
        progressBar.maxValue = questions.Count;
        // The starting point for our progress bar
        progressBar.value = 0;


    }

    private void Update( )
    {
        // setting timerImage's Image Component fill amount = to the equation for fillFraction that is made up in Timer.cs class
        timerImage.fillAmount = timer.fillFraction;

        if ( timer.loadNextQuestion )
        {
            // If value and maxValue are equal then all questions are complete and game is done
            if ( progressBar.value == progressBar.maxValue )
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if ( !hasAnsweredEarly && !timer.isAnsweringQuestion )
        {
            // We are passing in -1 bc we dont want the correctAnswer Accidently pressed on AND by passing in -1 into the index it automatically places user in the ELSE block
            // in the DisplayAnswer method
            DisplayAnswer( -1 );
            SetButtonState( false );
        }

    }



    public void OnAnswerSelected( int index )
    {
        hasAnsweredEarly = true;
        DisplayAnswer( index );

        // We are setting all the buttons states to false (off/not clickable) after the answer is selected 
        SetButtonState( false );
        // call 'CancelTimer()' when the question has been answered early
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";




    }

    void DisplayAnswer( int index )
    {
        Image buttonImage;

        if ( index == currentQuestion.GetCorrectAnswerIndex() )
        {
            questionText.text = "Correct";

            // Create Temp var in order to get 'Image Component' for the answerButton at that index
            buttonImage = answerButtons [ index ].GetComponent<Image>();
            // Change the variable above {buttonImage}'s sprite to the "correctAnswersSprite" {which is a new image}
            buttonImage.sprite = correctAnswerSprite;
            // If player gets correct answer increment score by calling the  scorekeeper script
            scoreKeeper.IncrementCorrectAnswers();
        } else
        {
            // Getting the INDEX for the Correct Answer not the picked answer
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            // Temp var (correctAnswer) and make it equal to 'correctAnswerIndex' from QuestionSO.cs -> 'currentQuestion' var in other script -> and answer index from that question
            string correctAnswer = currentQuestion.GetAnswer( correctAnswerIndex );

            questionText.text = "Sorry, the correct answer was... \n" + correctAnswer;

            buttonImage = answerButtons [ correctAnswerIndex ].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    /* We are going to Get another qustion @ which time we change the button clicking state to true (able to) and then display that next currentQuestion*/
    private void GetNextQuestion( )  // this method Hasn't been implemented into game yet 
    {
        if ( questions.Count > 0 )
        {
            SetButtonState( true );
            SetDefaultButtonState();
            GetRandomQuestion();
            DisplayQuestion();
            // increment progessbar by 1
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }

    }

    void GetRandomQuestion( )
    {
        int index = Random.Range( 0 , questions.Count );
        currentQuestion = questions [ index ];

        // To ensure that 'currentQuestion' is removed we will use if statment below and not... {questions.Remove(currentQuestion()}   *this way will work but best to do if statement especially as game gets bigger
        if ( questions.Contains( currentQuestion ) )
        {
            questions.Remove( currentQuestion );
        }
    }

    private void DisplayQuestion( )
    {
        questionText.text = currentQuestion.GetQuestion();



        // 'FOR LOOP' used to get Text for each Question and the Text for each Answer Button for each Question

        for ( int i = 0; i < answerButtons.Length; i++ )
        {
            TextMeshProUGUI buttonText = answerButtons [ i ].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer( i );
        }
    }

    /* Method for setting the buttons on or off. Passing in a Bool for the Btns current state*/

    private void SetButtonState( bool state )
    {

        for ( int i = 0; i < answerButtons.Length; i++ )
        {
            // Creates temp var 'button' of type Button & each time it goes through for loop will chose a different button selecting the button component for that button
            Button button = answerButtons [ i ].GetComponent<Button>();

            // this will set the each button when it goes thru for loop to the state (T/F) that is passed in to this method
            button.interactable = state;
        }

    }

    // Changing the button sprite back to its default sprite state (the blue one)
    private void SetDefaultButtonState( )
    {
        Image buttonImage;

        for ( int i = 0; i < answerButtons.Length; i++ )
        {
            buttonImage = answerButtons [ i ].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
