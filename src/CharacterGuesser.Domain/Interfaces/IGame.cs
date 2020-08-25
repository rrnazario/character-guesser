namespace CharacterGuesser.Domain.Interfaces
{
    public interface IGame
    {
        /// <summary>
        /// Start the game.
        /// </summary>
        void Play();

        /// <summary>
        /// Set default states to start the game again
        /// </summary>
        void Reset();

        /// <summary>
        /// Give to game new information.
        /// </summary>
        void Learn();

        /// <summary>
        /// Save current game state.
        /// </summary>
        void Save();

        /// <summary>
        /// Load last game state.
        /// </summary>
        void Load();
    }
}
