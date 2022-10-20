using UnityEngine;

namespace Match2.Scripts.Common.Tools
{
    /// <summary>
    /// Interface for pooled instances.
    /// </summary>
    public interface IPooled
    {
        int Id { get; }
        
        GameObject gameObject { get; }
        
        Transform transform { get; }
    }
}