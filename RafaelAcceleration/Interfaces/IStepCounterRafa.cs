using System;
namespace RafaelAcceleration.Interfaces
{
    public interface IStepCounterRafa
    {
        int Steps { get; set; }
        void InitService();
        void StopService();
        bool IsAvailable();
    }
}
