namespace QuizJsonGenerator;

[Serializable]
public class Quiz
{
    public List<Question?> questions;

    public string title;

    public string url;

    public Quiz()
    {
        questions = new List<Question?>();
    }

    public void AddQuestion(Question? question)
    {
        if (question == null) return;

        if (question.correct == null)
        {
            Console.WriteLine("Question " + question.number + " doesn't have a correct answer! Not adding.");
            return;
        }

        Console.WriteLine("Adding question " + question.number + ".");

        questions.Add(question);
    }
}