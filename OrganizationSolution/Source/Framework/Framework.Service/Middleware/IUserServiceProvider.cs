namespace Framework.Service
{
    /// <summary>
    /// Defines the <see cref="IUserServiceProvider" />.
    /// </summary>
    public interface IUserServiceProvider
    {
        /// <summary>
        /// Gets the UserId.
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// The SetUserId.
        /// </summary>
        /// <param name="userId">The userId<see cref="long"/>.</param>
        void SetUserId(long userId);
    }
}
