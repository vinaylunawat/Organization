namespace Framework.Business
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WrapperObject{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class WrapperObject<T> : IEnumerable<T>
        where T : class
    {
        /// <summary>
        /// Defines the _models.
        /// </summary>
        private readonly List<T> _models = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapperObject{T}"/> class.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{T}"/>.</param>
        public WrapperObject(IEnumerable<T> models)
        {
            _models.AddRange(models);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapperObject{T}"/> class.
        /// </summary>
        protected WrapperObject()
        {
        }

        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="model">The model<see cref="T"/>.</param>
        public void Add(T model)
        {
            _models.Add(model);
        }

        /// <summary>
        /// The AddRange.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{T}"/>.</param>
        public void AddRange(IEnumerable<T> models)
        {
            _models.AddRange(models);
        }

        /// <summary>
        /// The GetEnumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _models.GetEnumerator();
        }

        /// <summary>
        /// The GetEnumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
