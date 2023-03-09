using System;
using UnityEngine;

namespace UniTools.Build
{
    /// <summary>
    /// Any value that can used inside a build pipeline and can be overriden from the command line
    /// </summary>
    public abstract class ScriptableBuildParameter<TValue> : ScriptableObject
    {
        // --awsprofile test --projectdefines DEV

        [SerializeField] private TValue m_value = default;

        /// <summary>
        /// The name of the parameter that can be used inside a command line
        /// </summary>
        public string Name => $"--{name.ToLower()}";

        public TValue Value
        {
            get
            {
                if (Application.isBatchMode && TryParseFromCommandLine(Environment.CommandLine, out TValue v))
                {
                    return v;
                }

                return m_value;
            }
        }

        protected abstract bool TryParseFromCommandLine(string commandLine, out TValue v);
    }
}