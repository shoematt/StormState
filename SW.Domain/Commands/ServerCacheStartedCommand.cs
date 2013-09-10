using System;

namespace Orca.Domain.Commands
{
    [Serializable]
    public class ServerCacheStartedCommand
    {
        public ServerCacheStartedCommand( )
        {
        }

    }

    [Serializable]
    public class ServerFinishedPopulatingCache
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionComplete"/> class.
        /// </summary>
        public ServerFinishedPopulatingCache( )
        {
        }




    }

      [Serializable]
    public class PauseExternalUpdateService
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionComplete"/> class.
        /// </summary>
        public PauseExternalUpdateService( )
        {
        }




    }


      [Serializable]
    public class StartExternalUpdateService
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="WorkInstructionComplete"/> class.
        /// </summary>
        public StartExternalUpdateService( )
        {
        }




    }

    

}
