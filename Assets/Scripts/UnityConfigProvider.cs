using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityConfigProvider : MonoBehaviour, TWF.IConfigProvider
{
#pragma warning disable SA1121 // Use built-in type alias
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
    public string[] Values;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1121 // Use built-in type alias

    private readonly Dictionary<string, string> values = new Dictionary<string, string>();
    private readonly Dictionary<string, Action<string>> listeners = new Dictionary<string, Action<string>>();

    public T GetConfig<T>(string key)
    {
        try
        {
            return this.ConvertValue<T>(this.values[key]);
        }
        catch (KeyNotFoundException e)
        {
            throw new ArgumentException("Invalid key: " + key);
        }
    }

    public void RegisterConfigUpdateListener<T>(string key, Action<T> callback)
    {
        void TypedListener(string valueAsString) => callback.Invoke(this.ConvertValue<T>(valueAsString));

        this.listeners.TryGetValue(key, out Action<string> listener);
        if (listener == null)
        {
            this.listeners[key] = TypedListener;
        }
        else
        {
            this.listeners[key] += TypedListener;
        }
    }

    public void Refresh()
    {
        this.values.Clear();
        this.Sync();
    }

    internal void OnValidate()
    {
        this.Sync();
    }

    // Start is called before the first frame update
    internal void Start()
    {
        this.Sync();
    }

    // Update is called once per frame
    internal void Update()
    {
    }

    private void Sync()
    {
        var duplicatesChecker = new HashSet<string>();
        foreach (string v in this.Values)
        {
            var fragments = v.Split(':');
            Debug.Assert(fragments.Length == 2);
            var key = fragments[0];
            var value = fragments[1];
            Debug.Assert(!duplicatesChecker.Contains(key));
            this.values.TryGetValue(key, out string oldValue);
            if (value != oldValue)
            {
                this.values[key] = value;
                this.listeners.TryGetValue(key, out Action<string> listener);
                listener?.Invoke(value);
            }
        }
    }

    private T ConvertValue<T>(string value)
    {
        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (FormatException e)
        {
            throw new ArgumentException("Bad cast: " + value + " cannot be converted to " + typeof(T).Name);
        }
    }
}
