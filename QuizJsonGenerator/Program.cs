// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using Newtonsoft.Json;
using QuizJsonGenerator;

Console.WriteLine("Reading original file...");

var random = new Random();

//string text = File.ReadAllText(args[0]);

Console.WriteLine("Parsing to quiz class...");

var quiz = new Quiz();

Question currentQuestion = null;
int currentAnswerIndex = 0;

foreach (var data in File.ReadLines(args[0]))
{
    var line = data.TrimStart(' ');

    if (line.StartsWith("//")) continue;

    if (line.StartsWith("url: "))
    {
        quiz.url = line.Remove(0, 4);
    }

    if (line.StartsWith("# "))
    {
        quiz.title = line.Remove(0, 2);
    }

    if (Regex.IsMatch(line, @"[0-9]+\)\s+.+"))
    {
        if (currentQuestion != null) quiz.questions.Add(currentQuestion);

        currentQuestion = new Question
        {
            number = int.Parse(new string(line.TakeWhile(char.IsDigit).ToArray())),
            prompt = Regex.Replace(line, @"[0-9]+\)\s+", "")
        };

        currentAnswerIndex = 0;
    }

    if (Regex.IsMatch(line, @"\-\s+.+"))
    {
        currentQuestion.answers.Add(Regex.Replace(line, @"\-\s+", ""));
        currentAnswerIndex++;
    }
    
    if (Regex.IsMatch(line, @"\*\s+.+"))
    {
        currentQuestion.answers.Add(Regex.Replace(line, @"\*\s+", ""));
        currentQuestion.correct = new Correct(currentAnswerIndex);
        currentAnswerIndex++;
    }
}

if (currentQuestion != null) quiz.questions.Add(currentQuestion);

Console.WriteLine("Generated class with " + quiz.questions.Count + " questions.");
Console.WriteLine("Parsing to json and writing to " + args[1] + "...");

var jsonText = JsonConvert.SerializeObject(quiz, Formatting.Indented);

File.WriteAllText(args[1], jsonText);

Console.WriteLine("Finished, press any key to exit.");

Console.ReadKey();