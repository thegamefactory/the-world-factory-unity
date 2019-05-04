﻿using UnityEngine;
using System;

public class KeyCombinationPublisher
{
    private KeyCombination keyCombination;
    private Action onActivate;
    private Action onContinuous;
    private Action onDeactivate;
    private bool active;

    private KeyCombinationPublisher(KeyCombination keyCombination, Action onActivate, Action onContinuous,
        Action onDeactivate)
    {
        this.keyCombination = keyCombination;
        this.onActivate = onActivate;
        this.onContinuous = onContinuous;
        this.onDeactivate = onDeactivate;
        this.active = false;
    }

    public class Builder
    {
        private KeyCombination keyCombination;
        private Action onContinuous;
        private Action onActivate;
        private Action onDeactivate;

        public Builder(KeyCombination keyCombination)
        {
            this.keyCombination = keyCombination;
        }

        public Builder OnContinuous(Action action)
        {
            this.onContinuous += action;
            return this;
        }

        public Builder OnActivate(Action action)
        {
            this.onActivate += action;
            return this;
        }

        public Builder OnDeactivate(Action action)
        {
            this.onDeactivate += action;
            return this;
        }

        public KeyCombinationPublisher build()
        {
            return new KeyCombinationPublisher(keyCombination, onActivate, onContinuous, onDeactivate);
        }
    }

    public static Builder builder(KeyCombination keyCombination)
    {
        return new Builder(keyCombination);
    }

    public void Enact()
    {
        if (!active && this.keyCombination.Evaluate())
        {
            this.onActivate?.Invoke();
            active = true;
        }
        if (this.keyCombination.Evaluate()) this.onContinuous?.Invoke();
        if (active && !this.keyCombination.Evaluate())
        {
            this.onDeactivate?.Invoke();
            active = false;
        }
    }
}