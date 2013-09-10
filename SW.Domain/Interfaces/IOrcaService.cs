using System;

namespace Orca.Domain.Interfaces
{
    public interface IOrcaService : IDisposable
    {
        void Start( );
        string Name { get; }
        void Shutdown( );
    }
}
