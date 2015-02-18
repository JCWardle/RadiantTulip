using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadiantTulip.View.ViewModels
{
    public class OberservableGround : Ground, IObservable<Ground>
    {
        public IDisposable Subscribe(IObserver<Ground> observer)
        {
            throw new NotImplementedException();
        }
    }
}
