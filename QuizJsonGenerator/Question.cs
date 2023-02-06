namespace QuizJsonGenerator;

[Serializable]
public class Question
{
    public List<string> answers = new();

    public Correct? correct;

    public int number;

    public string prompt;
}