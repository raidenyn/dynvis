#region

using System;
using System.ComponentModel;

#endregion

namespace DevAge.ComponentModel
{
    /// <summary>
    /// Class used to implement an empty ITypeDescriptorContext.
    /// This class seems to be required by the mono framework, ms framework accept null as ITypeDescriptorContext
    /// </summary>
    public class EmptyTypeDescriptorContext : ITypeDescriptorContext
    {
        /// <summary>
        /// Empty ITypeDescriptorContext instance. For now I use null because mono seems to don't like this class (and throw anyway an exception)
        /// </summary>
        public static readonly EmptyTypeDescriptorContext Empty; //new EmptyTypeDescriptorContext();

        private readonly EmptyContainer container = new EmptyContainer();

        #region ITypeDescriptorContext Members

        public IContainer Container
        {
            get { return container; }
        }

        public object Instance
        {
            get { return null; }
        }

        public void OnComponentChanged()
        {
        }

        public bool OnComponentChanging()
        {
            return true;
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }

        public object GetService(Type serviceType)
        {
            return null;
        }

        #endregion
    }

    public class EmptyContainer : IContainer
    {
        #region IContainer Members

        public void Add(IComponent component, string name)
        {
            throw new NotImplementedException();
        }

        public void Add(IComponent component)
        {
            throw new NotImplementedException();
        }

        public ComponentCollection Components
        {
            get { return new ComponentCollection(null); }
        }

        public void Remove(IComponent component)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        #endregion
    }
}