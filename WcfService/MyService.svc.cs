using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WcfService {
    public class MyService : IService1 { 

        public Dictionary<string, int> GetDataFast(StringBuilder book) {

            var newClassInfo = Type.GetType("LibProcessing.MultiThreading, LibProcessing", true, true);
            object newLibClass = Activator.CreateInstance(newClassInfo, new object[] { book });
            var newMethodInfo = newLibClass.GetType().GetMethod("AllNewMethods");
            return (Dictionary<string, int>)newMethodInfo.Invoke(newLibClass, null);

        }

        public Dictionary<string, int> GetData(StringBuilder book) {

            var classInfo = Type.GetType("LibProcessing.Processing, LibProcessing", true, true);
            object libClass = Activator.CreateInstance(classInfo);
            var methodInfo = libClass.GetType().GetMethod("getResult", BindingFlags.Instance | BindingFlags.NonPublic);
            return(Dictionary<string, int>)methodInfo.Invoke(libClass, new object[] { book });

        }

        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            if (composite == null) {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue) {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
