using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsApplication1
{
    public class MyDataSourceWrapper : ITypedList, IList
    {

        public readonly IList NestedList;
		public ITypedList NestedTypedList { get { return (ITypedList)NestedList; } }
        MyPropertyDescriptor descriptor;


        public MyDataSourceWrapper(ITypedList dataSource, string unboundColumnFieldName, GetValueEventHanlder getValueHandler, SetValueEventHanlder setValueHanlder)
        {
            this.NestedList = (IList)dataSource;
            descriptor = new MyPropertyDescriptor(unboundColumnFieldName);
            descriptor.GetUnboundValue += getValueHandler;
            descriptor.SetUnboundValue += setValueHanlder;
        }

        #region ITypedList Members

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptorCollection propertiesList = NestedTypedList.GetItemProperties(listAccessors);
            propertiesList.Add(descriptor);
            return propertiesList;
        }

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return NestedTypedList.GetListName(listAccessors);
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
           return NestedList.Add(value);
        }

        void IList.Clear()
        {
            NestedList.Clear();
        }

        bool IList.Contains(object value)
        {
            return NestedList.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return NestedList.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            NestedList.Insert(index, value);
        }

        bool IList.IsFixedSize
        {
            get { return  NestedList.IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { return NestedList.IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            NestedList.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            NestedList.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return NestedList[index];
            }
            set
            {
                NestedList[index]= value;
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            NestedList.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return NestedList.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return NestedList.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return NestedList.SyncRoot; }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NestedList.GetEnumerator();
        }

        #endregion
    }

    public class MyPropertyDescriptor: PropertyDescriptor
    {
        protected MyPropertyDescriptor(string name, Attribute[] attrs)
            : base(name, attrs)
        {

        }
        protected MyPropertyDescriptor(MemberDescriptor descr)
            : base(descr)
        {
            
        }
        protected MyPropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
            : base(descr, attrs)
        {
            
        }

        public MyPropertyDescriptor(string name)
            : base(name, null)
        {

        }



        private GetValueEventHanlder onGetValue;

        public event GetValueEventHanlder GetUnboundValue
        {
            add
            {
                onGetValue += value;
            }
            remove
            {
                onGetValue -= value;
            }
        }





        private SetValueEventHanlder onSetValue;

        public event SetValueEventHanlder SetUnboundValue
        {
            add
            {
                onSetValue += value;
            }
            remove
            {
                onSetValue -= value;
            }
        }
  

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(object); }
        }

        public override object GetValue(object component)
        {
            object result = null;
            if (onGetValue != null)
            {
                onGetValue(component, out result);
            }
            return result;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return typeof(object); }
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            if (onSetValue != null)
                onSetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
            public delegate void GetValueEventHanlder(object component, out object result);
         public delegate void SetValueEventHanlder(object component, object value);
}
