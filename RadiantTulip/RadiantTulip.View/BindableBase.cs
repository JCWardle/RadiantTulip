﻿using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadiantTulip.View
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler _event;

        /// <summary>
        /// Occurs when a property value changes.
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler changedEventHandler = this._event;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._event, comparand + value, comparand);
                }
                while (changedEventHandler != comparand);
            }
            remove
            {
                PropertyChangedEventHandler changedEventHandler = this._event;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this._event, comparand - value, comparand);
                }
                while (changedEventHandler != comparand);
            }
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        ///             notifies listeners only when necessary.
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam><param name="storage">Reference to a property with both getter and setter.</param><param name="value">Desired value for the property.</param><param name="propertyName">Name of the property used to notify listeners. This
        ///             value is optional and can be provided automatically when invoked from compilers that
        ///             support CallerMemberName.</param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the
        ///             desired value.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals((object)storage, (object)value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// 
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        ///             value is optional and can be provided automatically when invoked from compilers
        ///             that support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler changedEventHandler = this._event;
            if (changedEventHandler == null)
                return;
            changedEventHandler((object)this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam><param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.OnPropertyChanged(PropertySupport.ExtractPropertyName<T>(propertyExpression));
        }
    }
}
