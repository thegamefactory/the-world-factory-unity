namespace TWF.Input
{
    using System;

    /// <summary>
    /// A subject which notifies observers when events related to the given keyCombination happen.
    /// The subject needs to be enacted by regularly calling the enact method.
    /// </summary>
    public class KeyCombinationSubject
    {
        private readonly IKeyCombination keyCombination;
        private readonly Action onActivate;
        private readonly Action onContinuous;
        private readonly Action onDeactivate;
        private bool active;

        public KeyCombinationSubject(IKeyCombination keyCombination, Action onActivate, Action onContinuous, Action onDeactivate)
        {
            this.keyCombination = keyCombination;
            this.onActivate = onActivate;
            this.onContinuous = onContinuous;
            this.onDeactivate = onDeactivate;
            this.active = false;
        }

        public static KeyCombinationSubjectBuilder Builder(IKeyCombination keyCombination)
        {
            return new KeyCombinationSubjectBuilder(keyCombination);
        }

        public void Enact()
        {
            if (!this.active && this.keyCombination.IsActive())
            {
                this.onActivate?.Invoke();
                this.active = true;
            }

            if (this.keyCombination.IsActive())
            {
                this.onContinuous?.Invoke();
            }

            if (this.active && !this.keyCombination.IsActive())
            {
                this.onDeactivate?.Invoke();
                this.active = false;
            }
        }
    }
}