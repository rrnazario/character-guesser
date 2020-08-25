using CharacterGuesser.Domain.Interfaces;
using CharacterGuesser.Infra.Helpers;

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
