using System;

namespace TWF.Input
{
    /// <summary>
    /// A subject which notifies observers when events related to the given keyCombination happen.
    /// The subject needs to be enacted by regularly calling the enact method.
    /// </summary>
    public class KeyCombinationSubject
    {
        private IKeyCombination keyCombination;
        private Action onActivate;
        private Action onContinuous;
        private Action onDeactivate;
        private bool active;

        private KeyCombinationSubject(IKeyCombination keyCombination, Action onActivate, Action onContinuous,
            Action onDeactivate)
        {
            this.keyCombination = keyCombination;
            this.onActivate = onActivate;
            this.onContinuous = onContinuous;
            this.onDeactivate = onDeactivate;
            this.active = false;
        }

        public class KeyCombinationSubjectBuilder
        {
            private IKeyCombination keyCombination;
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

            public KeyCombinationSubject build()
            {
                return new KeyCombinationSubject(keyCombination, onActivate, onContinuous, onDeactivate);
            }
        }

        public static KeyCombinationSubjectBuilder Builder(IKeyCombination keyCombination)
        {
            return new KeyCombinationSubjectBuilder(keyCombination);
        }

        public void Enact()
        {
            if (!active && this.keyCombination.IsActive())
            {
                this.onActivate?.Invoke();
                active = true;
            }
            if (this.keyCombination.IsActive()) this.onContinuous?.Invoke();
            if (active && !this.keyCombination.IsActive())
            {
                this.onDeactivate?.Invoke();
                active = false;
            }
        }
    }
}