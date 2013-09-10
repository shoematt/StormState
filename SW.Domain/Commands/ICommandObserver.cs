namespace Orca.Domain.Commands
{
    public interface ICommandObserver
    {
        bool IsRecording { get; }

        //    void RecordCommandStatus( call, string status);

        //  IEnumerable<string> GetLog(ActionCall call);
    }
}