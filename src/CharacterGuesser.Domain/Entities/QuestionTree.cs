using CharacterGuesser.Domain.Constants;
using CharacterGuesser.Domain.Factories;
using CharacterGuesser.Domain.Interfaces;
using CharacterGuesser.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace CharacterGuesser.Domain.Entities
{
    public class QuestionTree : IQuestionTree
    {
        public IQuestionTree Parent { get; set; }
        public IQuestionTree QuestionYes { get; set; }
        public IQuestionTree QuestionNo { get; set; }
        public string Text { get ; set; }

        #region Constructors
        
        public QuestionTree(string text) => Text = text;
        public QuestionTree(string text, IQuestionTree parent) : this(text) => Parent = parent;

        #endregion

        #region Getters / Setters
        public void SetPositiveQuestion(IQuestionTree question)
        {
            QuestionYes = question;
            question.SetParent(this);
        }
        public void SetPositiveQuestion(string text) => QuestionYes = new QuestionTree(text, this);
        public IQuestionTree GetPositiveQuestion() => QuestionYes;
        public void SetNegativeQuestion(string text) => QuestionNo = new QuestionTree(text, this);
        public void SetNegativeQuestion(IQuestionTree question)
        {
            QuestionNo = question;
            question.SetParent(this);
        }
        public IQuestionTree GetNegativeQuestion() => QuestionNo;
        public void SetParent(IQuestionTree question) => Parent = question;
        public IQuestionTree GetParent() => Parent;        
        public string GetText() => Text;
        #endregion

        public IQuestionTree GetNextQuestion()
        {
            var key = Console.ReadKey().Key;

            Console.Clear();

            //Only accepting Yes or No
            if (key != ConsoleKey.S && key != ConsoleKey.N)
                return this;            

            if (IsLeaf())
            {
                if (key == ConsoleKey.S)
                {
                    Console.WriteLine($"\n{MessageConstants.SUCCESS_MESSAGE_FIRST_TIME}");
                    Console.ReadKey();
                }
                else
                    AddQuestion();

                ConsoleHelper.ResetScreen();

                return First();
            }
            else
                return key == ConsoleKey.S ? GetPositiveQuestion() : GetNegativeQuestion();
        }

        public void AddQuestion()
        {
            Console.Write($"\n\n{ResourceHelper.GetString("LearningWhat")} {MessageConstants.SUFIX_ENTER_MESSAGE}");

            var newPlate = ConsoleHelper.ReadWholeWord();

            Console.Write(string.Format(ResourceHelper.GetString("ThinkDiffers"), newPlate, GetText()) + $"{MessageConstants.SUFIX_ENTER_MESSAGE}");
            var differenceBetweenLastPlate = ConsoleHelper.ReadWholeWord();

            //Creating new question
            IQuestionTree newPlateQuestion = QuestionFactory.CreateQuestion(differenceBetweenLastPlate, GetParent());
            newPlateQuestion.SetPositiveQuestion(newPlate);

            //Setting old parent to point to this newest question
            if (GetParent().GetPositiveQuestion().Equals(this))
                GetParent().SetPositiveQuestion(newPlateQuestion);
            else
                GetParent().SetNegativeQuestion(newPlateQuestion);

            //The new node negative question must pointing to the current question.
            newPlateQuestion.SetNegativeQuestion(this);

            //Save to file
            Save();
        }

        private void Save()
        {
            var file = new List<string>();

            var saveNode = First();
            file.Add($"{saveNode.GetText()}|{saveNode.GetPositiveQuestion()?.GetText()}|{saveNode.GetNegativeQuestion()?.GetText()}");

            var rightNode = saveNode.GetPositiveQuestion();
            while (rightNode != null)
            {
                file.Add($"{rightNode.GetText()}|{rightNode.GetPositiveQuestion()?.GetText()}|{rightNode.GetNegativeQuestion()?.GetText()}");
                rightNode = rightNode.GetPositiveQuestion();
            }

            var leftNode = saveNode.GetNegativeQuestion();
            while (leftNode != null)
            {
                file.Add($"{leftNode.GetText()}|{leftNode.GetPositiveQuestion()?.GetText()}|{leftNode.GetNegativeQuestion()?.GetText()}");
                leftNode = leftNode.GetNegativeQuestion();
            }

            File.WriteAllLines(GeneralConstants.QUESTION_DATABASE, file);
        }

        public IQuestionTree First() => Parent == null ? this : Parent.First();
        public bool IsLeaf() => QuestionYes == null && QuestionNo == null;

        #region Default overrides

        public override string ToString() => string.Format(ResourceHelper.GetString("Questioning"), Text);
        public override bool Equals(object obj) => (obj as QuestionTree).Text == Text;
        public override int GetHashCode() => base.GetHashCode();

        public IQuestionTree Find(string text)
        {
            if (Text == text)
                return this;

            var result = QuestionYes?.Find(text);

            if (result == null)
                return QuestionNo?.Find(text);
            else
                return result;
        }

        #endregion
    }
}
