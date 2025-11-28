using UnityEngine;

[CreateAssetMenu( menuName = "Quiz Question SO" , fileName = "New Question" )]
public class QuestionSO : ScriptableObject
{
    //TextArea(minLines & maxLines) in the Inspector in Unity
    [TextArea( 2 , 6 )]
    [SerializeField] string question = "Enter new question text here...";
    [SerializeField] string [ ] answers = new string [ 4 ];
    [Range( 0 , 3 )]
    [SerializeField] int correctAnswerIndex;

    /* Gets the question that is created when a new S.O. for the QuestionSO is created*/
    public string GetQuestion( )
    {
        return question;
    }

    /* Gets each answer string at each index that is written up when a new QuestionSO is created in Unity*/
    public string GetAnswer( int index )
    {
        // This is getting the answer at a specific index
        return answers [ index ];
    }

    /* Gets the correct answer to return from the S.O. created in Unity*/
    public int GetCorrectAnswerIndex( )
    {
        return correctAnswerIndex;
    }

}








/*public class Test         *** This is to just show the "Getter Method" being used not part of game***
{

    QuestionSO questionSO;

    void TestA()
    {
        // We are creating a new String VAR 'questionText' by the return object of "questionSO.GetQuestion()" which
        // returns a string type
        string questionText = questionSO.GetQuestion();
    }

}*/
