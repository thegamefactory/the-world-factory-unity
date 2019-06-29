namespace TWF
{
    using System;

    /// <summary>
    /// Interface to provide a key/value configuration store which enables to register update listeners.
    /// </summary>
    public interface IConfigProvider
    {
        T GetConfig<T>(string key);

        void RegisterConfigUpdateListener<T>(string key, Action<T> callback);

        /// <summary>
        /// Calls all config update listener callbacks with the existing value.
        /// </summary>
        void Refresh();
    }
}
