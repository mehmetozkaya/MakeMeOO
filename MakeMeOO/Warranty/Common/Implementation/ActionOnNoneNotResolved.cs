using MakeMeOO.Warranty.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO.Warranty.Common.Implementation
{
    internal class ActionOnNoneNotResolved<T> : IActionable<T>
    {
        public IFilteredActionable<T> When(Func<T, bool> predicate) =>
            new NoneNotMatchedAsSome<T>();

        public IFilteredActionable<T> WhenSome() =>
            new NoneNotMatchedAsSome<T>();

        public IFilteredNoneActionable<T> WhenNone() =>
            new NoneMatched<T>();

        public void Execute() { }
    }

    internal class ActionOnSomeNotResolved<T> : IActionable<T>
    {
        private T Value { get; }

        public ActionOnSomeNotResolved(T value)
        {
            this.Value = value;
        }

        public IFilteredActionable<T> When(Func<T, bool> predicate) =>
            predicate(this.Value) ? (IFilteredActionable<T>)new SomeMatched<T>(this.Value) : new SomeNotMatched<T>(this.Value);

        public IFilteredActionable<T> WhenSome() =>
            new SomeMatched<T>(this.Value);

        public IFilteredNoneActionable<T> WhenNone() =>
            new SomeNotMatched<T>(this.Value);

        public void Execute() { }
    }

    internal class ActionResolved<T> : IActionable<T>, IFilteredActionable<T>, IFilteredNoneActionable<T>
    {
        private Action Action { get; }

        public ActionResolved(Action action)
        {
            this.Action = action;
        }

        public IFilteredActionable<T> When(Func<T, bool> predicate) => this;

        public IFilteredActionable<T> WhenSome() => this;

        public IFilteredNoneActionable<T> WhenNone() => this;

        public IActionable<T> Do(Action<T> action) => this;

        public IActionable<T> Do(Action action) => this;

        public void Execute() => this.Action();
    }

    internal class MappingOnNoneNotResolved<T, TResult> : IMapped<T, TResult>
    {
        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) =>
            new NoneNotMatchedForMapping<T, TResult>();

        public IFilteredMapped<T, TResult> WhenSome() =>
            new NoneNotMatchedForMapping<T, TResult>();

        public IFilteredNoneMapped<T, TResult> WhenNone() =>
            new NoneMatchedForMapping<T, TResult>();

        public TResult Map()
        {
            throw new InvalidOperationException();
        }
    }

    internal class MappingOnSomeNotResolved<T, TResult> : IMapped<T, TResult>
    {

        private T Value { get; }

        public MappingOnSomeNotResolved(T value)
        {
            this.Value = value;
        }

        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) =>
            predicate(this.Value)
                ? (IFilteredMapped<T, TResult>)new SomeMatchedForMapping<T, TResult>(this.Value)
                : new SomeNotMatchedForMapping<T, TResult>(this.Value);

        public IFilteredMapped<T, TResult> WhenSome() =>
            new SomeMatchedForMapping<T, TResult>(this.Value);

        public IFilteredNoneMapped<T, TResult> WhenNone() =>
            new SomeNotMatchedAsNoneForMapping<T, TResult>(this.Value);

        public TResult Map()
        {
            throw new InvalidOperationException();
        }
    }

    internal class MappingResolved<T, TResult> : IMapped<T, TResult>, IFilteredMapped<T, TResult>, IFilteredNoneMapped<T, TResult>
    {
        private TResult Result { get; }

        public MappingResolved(TResult result)
        {
            this.Result = result;
        }

        public IFilteredMapped<T, TResult> When(Func<T, bool> predicate) => this;

        public IFilteredMapped<T, TResult> WhenSome() => this;

        public IFilteredNoneMapped<T, TResult> WhenNone() => this;

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) => this;

        public IMapped<T, TResult> MapTo(Func<TResult> mapping) => this;

        public TResult Map() => this.Result;
    }

    internal class NoneMatched<T> : IFilteredNone<T>, IFilteredNoneActionable<T>
    {
        public IActionable<T> Do(Action action) =>
            new ActionResolved<T>(action);

        public IMapped<T, TResult> MapTo<TResult>(Func<TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping());
    }

    internal class NoneMatchedForMapping<T, TResult> : IFilteredNoneMapped<T, TResult>
    {
        public IMapped<T, TResult> MapTo(Func<TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping());
    }

    internal class NoneNotMatchedAsSome<T> : IFiltered<T>
    {
        public IActionable<T> Do(Action<T> action) =>
            new ActionOnNoneNotResolved<T>();

        public IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping) =>
            new MappingOnNoneNotResolved<T, TResult>();
    }

    internal class NoneNotMatchedForMapping<T, TResult> : IFilteredNoneMapped<T, TResult>, IFilteredMapped<T, TResult>
    {
        public IMapped<T, TResult> MapTo(Func<TResult> mapping) =>
            new MappingOnNoneNotResolved<T, TResult>();

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) =>
            new MappingOnNoneNotResolved<T, TResult>();
    }

    internal class SomeMatched<T> : IFiltered<T>
    {
        private T Value { get; }

        public SomeMatched(T value)
        {
            this.Value = value;
        }

        public IActionable<T> Do(Action<T> action) =>
            new ActionResolved<T>(() => action(this.Value));

        public IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping(this.Value));
    }

    internal class SomeMatchedForMapping<T, TResult> : IFilteredMapped<T, TResult>
    {
        private T Value;

        public SomeMatchedForMapping(T value)
        {
            this.Value = value;
        }

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) =>
            new MappingResolved<T, TResult>(mapping(this.Value));
    }

    internal class SomeNotMatched<T> : IFiltered<T>, IFilteredNoneActionable<T>
    {
        private T Value { get; }

        public SomeNotMatched(T value)
        {
            this.Value = value;
        }

        public IActionable<T> Do(Action<T> action) =>
            new ActionOnSomeNotResolved<T>(this.Value);

        public IActionable<T> Do(Action action) =>
            new ActionOnSomeNotResolved<T>(this.Value);

        public IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);
    }

    internal class SomeNotMatchedAsNone<T> : IFilteredNone<T>
    {
        private T Value { get; }

        public SomeNotMatchedAsNone(T value)
        {
            this.Value = value;
        }

        public IActionable<T> Do(Action action) =>
            new ActionOnSomeNotResolved<T>(this.Value);

        public IMapped<T, TResult> MapTo<TResult>(Func<TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);
    }

    internal class SomeNotMatchedAsNoneForMapping<T, TResult> : IFilteredNoneMapped<T, TResult>
    {
        private T Value { get; }

        public SomeNotMatchedAsNoneForMapping(T value)
        {
            this.Value = value;
        }

        public IMapped<T, TResult> MapTo(Func<TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);
    }

    internal class SomeNotMatchedForMapping<T, TResult> : IFilteredMapped<T, TResult>
    {
        private T Value;

        public SomeNotMatchedForMapping(T value)
        {
            this.Value = value;
        }

        public IMapped<T, TResult> MapTo(Func<T, TResult> mapping) =>
            new MappingOnSomeNotResolved<T, TResult>(this.Value);
    }


}
