namespace TWF.Input
{
    using System;

    public class KeyCombinationSubjectBuilder
    {
        private readonly IKeyCombination keyCombination;
        private Action onContinuous;
        private Action onActivate;
        private Action onDeactivate;

        public KeyCombinationSubjectBuilder(IKeyCombination keyCombination)
        {
            this.keyCombination = keyCombination;
        }

        public KeyCombinationSubjectBuilder OnContinuous(Action action)
        {
            this.onContinuous += action;
            return this;
        }

        public KeyCombinationSubjectBuilder OnActivate(Action action)
        {
            this.onActivate += action;
            return this;
        }

        public KeyCombinationSubjectBuilder OnDeactivate(Action action)
        {
            this.onDeactivate += action;
            return this;
        }

        public KeyCombinationSubject Build()
        {
            return new KeyCombinationSubject(this.keyCombination, this.onActivate, this.onContinuous, this.onDeactivate);
        }
    }
}