using MakeMeOO.Warranty.Common.Implementation;
using MakeMeOO.Warranty.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MakeMeOO.Warranty.Common
{
    public class Option<T> : IOption<T>
    {
        private IEnumerable<T> Content { get; }
        public Option(IEnumerable<T> content)
        {
            Content = content;
        }

        public static Option<T> Some(T value) => new Option<T>(new[] { value });
        public static Option<T> None() => new Option<T>(new T[0]);

        public IFiltered<T> When(Func<T, bool> predicate)
        {
            return
                this.Content
                    .Select(item => this.WhenSome(item, predicate))
                    .DefaultIfEmpty(new NoneNotMatchedAsSome<T>())
                    .Single();
        }

        private IFiltered<T> WhenSome(T value, Func<T, bool> predicate) =>
            predicate(value) ? (IFiltered<T>)new SomeMatched<T>(value) : (IFiltered<T>)new SomeNotMatched<T>(value);

        public IFiltered<T> WhenSome()
        {
            return
                this.Content
                    .Select<T, IFiltered<T>>(item => new SomeMatched<T>(item))
                    .DefaultIfEmpty(new NoneNotMatchedAsSome<T>())
                    .Single();
        }

        public IFilteredNone<T> WhenNone()
        {
            return
                this.Content
                    .Select<T, IFilteredNone<T>>(item => new SomeNotMatchedAsNone<T>(item))
                    .DefaultIfEmpty(new NoneMatched<T>())
                    .Single();
        }        
    }
}
