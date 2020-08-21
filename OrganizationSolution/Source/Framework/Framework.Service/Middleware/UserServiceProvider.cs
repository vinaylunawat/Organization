namespace Framework.Service
{
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="UserServiceProvider" />.
    /// </summary>
    public class UserServiceProvider : IUserServiceProvider
    {
        /// <summary>
        /// Defines the MemoryCacheLock.
        /// </summary>
        private static readonly ReaderWriterLockSlim MemoryCacheLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the UserId.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// The SetUserId.
        /// </summary>
        /// <param name="userId">The userId<see cref="long"/>.</param>
        public void SetUserId(long userId)
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    if (UserId == default)
                    {
                        UserId = userId;
                    }
                }
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }
    }
}
