namespace CharacterGuesser.Domain.Interfaces
{
    public interface IQuestionTree
    {
        /// <summary>
        /// Return the node that has not parent.
        /// </summary>
        /// <returns></returns>
        IQuestionTree First();

        /// <summary>
        /// Check if there are child nodes.
        /// </summary>
        /// <returns></returns>
        bool IsLeaf();

        /// <summary>
        /// Set negative node with one that already exists.
        /// </summary>
        /// <param name="question"></param>
        void SetNegativeQuestion(IQuestionTree question);

        /// <summary>
        /// Set negative node creating an answear.
        /// </summary>
        /// <param name="text"></param>
        void SetNegativeQuestion(string text);

        /// <summary>
        /// Set positive node with one that already exists.
        /// </summary>
        /// <param name="question"></param>
        void SetPositiveQuestion(IQuestionTree question);

        /// <summary>
        /// Set positive node creating an answear.
        /// </summary>
        /// <param name="text"></param>
        void SetPositiveQuestion(string text);

        /// <summary>
        /// Retrieve negative node.
        /// </summary>
        /// <returns></returns>
        IQuestionTree GetNegativeQuestion();
        
        /// <summary>
        /// Retrieve positive node.
        /// </summary>
        /// <returns></returns>
        IQuestionTree GetPositiveQuestion();

        /// <summary>
        /// Set the parent node.
        /// </summary>
        /// <param name="question"></param>
        void SetParent(IQuestionTree question);

        /// <summary>
        /// Retrieve the parent node.
        /// </summary>
        /// <returns></returns>
        IQuestionTree GetParent();

        /// <summary>
        /// Get the text of question.
        /// </summary>
        /// <returns></returns>
        string GetText();

        /// <summary>
        /// Find a specific node based on its text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IQuestionTree Find(string text);
    }
}
