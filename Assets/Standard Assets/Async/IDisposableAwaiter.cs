using System;

namespace Cubie.Extensions.Async
{
  internal interface IDisposableAwaiter : IAwaiter, IDisposable
  {
    
  }
  internal interface IDisposableAwaiter<out T> : IAwaiter<T>, IDisposableAwaiter
  {
    
  }
}