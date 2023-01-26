namespace QuizJsonGenerator;

[Serializable]
public class Quiz
{
    public List<Question> questions;

    public string title;

    public string url;

    public Quiz()
    {
        questions = new List<Question>();
    }
}