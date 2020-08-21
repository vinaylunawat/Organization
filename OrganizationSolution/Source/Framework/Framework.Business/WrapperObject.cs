namespace Framework.Business
{
    using System.Collections;
    using System.Collections.Generic;

    public class WrapperObject<T> : IEnumerable<T>
        where T : class
    {
        private readonly List<T> _models = new List<T>();

        public WrapperObject(IEnumerable<T> models)
        {
            _models.AddRange(models);
        }

        protected WrapperObject()
        {
        }

        public void Add(T model)
        {
            _models.Add(model);
        }

        public void AddRange(IEnumerable<T> models)
        {
            _models.AddRange(models);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _models.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
