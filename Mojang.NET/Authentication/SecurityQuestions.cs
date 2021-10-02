namespace Mojang.NET.Authentication
{
    public readonly struct SecurityQuestions
    {
        public SecurityQuestions(string questionOne, string questionTwo, string questionThree)
        {
            QuestionOne = questionOne;
            QuestionTwo = questionTwo;
            QuestionThree = questionThree;
        }

        public string QuestionOne { get; }
        public string QuestionTwo { get; }
        public string QuestionThree { get; }
    }
}