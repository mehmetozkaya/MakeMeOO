using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO.Warranty.Common.Interfaces
{
    public interface IOption<T>
    {
        IFiltered<T> When(Func<T, bool> predicate);
        IFiltered<T> WhenSome();
        IFilteredNone<T> WhenNone();
    }

    public interface IFiltered<T> : IFilteredActionable<T>
    {
        IMapped<T, TResult> MapTo<TResult>(Func<T, TResult> mapping);
    }

    public interface IFilteredActionable<T>
    {
        IActionable<T> Do(Action<T> action);
    }

    public interface IActionable<T>
    {
        IFilteredActionable<T> When(Func<T, bool> predicate);
        IFilteredActionable<T> WhenSome();
        IFilteredNoneActionable<T> WhenNone();
        void Execute();
    }

    public interface IFilteredNoneActionable<T>
    {
        IActionable<T> Do(Action action);
    }

    public interface IMapped<T, TResult>
    {
        IFilteredMapped<T, TResult> When(Func<T, bool> predicate);
        IFilteredMapped<T, TResult> WhenSome();
        IFilteredNoneMapped<T, TResult> WhenNone();
        TResult Map();
    }

    public interface IFilteredMapped<T, TResult>
    {
        IMapped<T, TResult> MapTo(Func<T, TResult> mapping);
    }

    public interface IFilteredNoneMapped<T, TResult>
    {
        IMapped<T, TResult> MapTo(Func<TResult> mapping);
    }

    public interface IFilteredNone<T>
    {
        IActionable<T> Do(Action action);
        IMapped<T, TResult> MapTo<TResult>(Func<TResult> mapping);
    }
}
