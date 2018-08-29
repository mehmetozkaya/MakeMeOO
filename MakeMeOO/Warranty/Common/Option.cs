using System.Collections;
using System.Collections.Generic;

namespace MakeMeOO.Warranty.Common
{
    internal class Option<T> : IEnumerable<T>
    {
        private IEnumerable<T> Content { get; }
        public Option(IEnumerable<T> content)
        {
            Content = content;
        }

        public static Option<T> Some(T value) => new Option<T>(new[] { value });
        public static Option<T> None() => new Option<T>(new T[0]);
        public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
