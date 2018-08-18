using System;

namespace MakeMeOO
{
    internal interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdraw(Action subtractFromBalance);
        IAccountState Freeze();
        IAccountState HolderVerified();
        IAccountState Close();
    }

    internal class Active : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Active(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;           
        }

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            subtractFromBalance();
            return this;
        }

        public IAccountState Freeze() => new Frozen(this.OnUnfreeze);
        public IAccountState HolderVerified() => this;
        public IAccountState Close() => new Closed();
    }

    internal class Frozen : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Frozen(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Deposit(Action addToBalance)
        {
            this.OnUnfreeze();
            addToBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            this.OnUnfreeze();
            subtractFromBalance();
            return new Active(this.OnUnfreeze);
        }

        public IAccountState Freeze() => this;
        public IAccountState HolderVerified() => this;
        public IAccountState Close() => new Closed();
    }
    internal class NotVerified : IAccountState
    {
        private Action OnUnfreeze { get; }

        public NotVerified(Action onUnfreeze)
        {
            this.OnUnfreeze = onUnfreeze;
        }

        public IAccountState Close() => new Closed();

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Freeze() => this;

        public IAccountState HolderVerified() => new Active(this.OnUnfreeze);

        public IAccountState Withdraw(Action subtractFromBalance) => this;
    }

    internal class Closed : IAccountState
    {
        public IAccountState Deposit(Action addToBalance) => this;
        public IAccountState Withdraw(Action subtractFromBalance) => this;
        public IAccountState Freeze() => this;
        public IAccountState HolderVerified() => this;
        public IAccountState Close() => this;
    }


}
