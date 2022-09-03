using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class ProvinceStack : INotifyPropertyChanged
    {

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public virtual void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the PropertyChange event for the property specified
        /// </summary>
        /// <param name="propertyName">Property name to update. Is case-sensitive.</param>
        public virtual void RaisePropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        private List<Province> OnStack = new List<Province>();

        public void RemoveAndAddToStack(List<Province> toremove, List<Province> toadd)
        {
            OnStack.RemoveAll(x => toremove.Contains(x));
            foreach (Province pr in toadd.ToList())
            {
                if (!OnStack.Contains(pr))
                    OnStack.Add(pr);
            }
            UpdateChanges();
        }

        public void AddToStack(List<Province> p)
        {
            foreach(Province pr in p.ToArray())
            {
                if(!OnStack.Contains(pr))
                    OnStack.Add(pr);
            }
            UpdateChanges();
        }

        public void RemoveFromStack(List<Province> p)
        {
            foreach (Province pr in p.ToArray())
            {
                OnStack.Remove(pr);
            }
            UpdateChanges();
        }

        public void UpdateChanges()
        {
            OnPropertyChanged("Tax");
        }

        public double Tax
        {
            get
            {
                if (OnStack.Any())
                    return Math.Round(OnStack.Average(x => x.Tax), 1);
                else
                    return 0;
            }
            set
            {
                if (OnStack.Any())
                {
                    double diff = Tax - value;
                    OnStack.ForEach(x => x.Tax -= (int)diff);
                }
            }
        }
        public double Production
        {
            get
            {
                if (OnStack.Any())
                    return Math.Round(OnStack.Average(x => x.Production), 1);
                else
                    return 0;
            }
            set
            {
                if (OnStack.Any())
                {
                    double diff = Production - value;
                    OnStack.ForEach(x => x.Production -= (int)diff);
                }
            }
        }
        public double Manpower
        {
            get
            {
                if (OnStack.Any())
                    return Math.Round(OnStack.Average(x => x.Manpower), 1);
                else
                    return 0;
            }
            set
            {
                if (OnStack.Any())
                {
                    double diff = Manpower - value;
                    OnStack.ForEach(x => x.Manpower -= (int)diff);
                }
            }
        }

        public Religion Religion
        {
            get
            {
                if (OnStack.Any())
                    return OnStack[0].Religion;
                else
                    return Religion.NoReligion;
            }
            set
            {
                if (OnStack.Any())
                {
                    OnStack.ForEach(x => x.Religion = value);
                }
            }
        }
        public Culture Culture
        {
            get
            {
                if (OnStack.Any())
                    return OnStack[0].Culture;
                else
                    return Culture.NoCulture;
            }
            set
            {
                if (OnStack.Any())
                {
                    OnStack.ForEach(x => x.Culture = value);
                }
            }
        }

        public Country Owner
        {
            get
            {
                if (OnStack.Any())
                    return OnStack[0].OwnerCountry;
                else
                    return Country.NoCountry;
            }
            set
            {
                if (OnStack.Any())
                {
                    if(value != Country.NoCountry)
                        OnStack.ForEach(x => x.OwnerCountry = value);
                    else
                        OnStack.ForEach(x => x.OwnerCountry = null);
                }
            }
        }
        public Country Controller
        {
            get
            {
                if (OnStack.Any())
                    return OnStack[0].Controller;
                else
                    return Country.NoCountry;
            }
            set
            {
                if (OnStack.Any())
                {
                    if (value != Country.NoCountry)
                        OnStack.ForEach(x => x.Controller = value);
                    else
                        OnStack.ForEach(x => x.Controller = null);
                }
            }
        }

        public int onstackam
        {
            get
            {
                return OnStack.Count;
            }
        }
    }
}
