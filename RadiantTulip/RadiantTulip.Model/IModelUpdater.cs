using System;

namespace RadiantTulip.Model
{
    public interface IModelUpdater
    {
        void Update();
        Game Game { get; }
        TimeSpan Time { set; get; }
        TimeSpan Increment { get; }
        TimeSpan MaxTime { get; }
    }
}
