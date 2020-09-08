using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public static class CopyHelper {
        public static T DeepCopy<T>(T obj) {
            if(obj == null) {
                return default(T);
            }

            T targetDeepCopyObj;

            Type targetType = obj.GetType( );
            //值类型  
            if (targetType.IsValueType == true) {
                targetDeepCopyObj = obj;
            }

            //引用类型   
            else {
                targetDeepCopyObj = Activator.CreateInstance<T>( );//创建引用对象   

                MemberInfo[] memberCollection = obj.GetType( ).GetMembers( );

                foreach (MemberInfo member in memberCollection) {
                    if (member.MemberType == MemberTypes.Field) {
                        FieldInfo field = (FieldInfo) member;
                        object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable) {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone( ));
                        } else {
                            field.SetValue(targetDeepCopyObj, DeepCopy(fieldValue));
                        }

                    } else if (member.MemberType == MemberTypes.Property) {
                        PropertyInfo myProperty = (PropertyInfo) member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null) {
                            object propertyValue = myProperty.GetValue(obj, null);
                            if (propertyValue is ICloneable) {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone( ), null);
                            } else {
                                myProperty.SetValue(targetDeepCopyObj, DeepCopy(propertyValue), null);
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }

        public static IEnumerable<T> DeepCopy<T>(IEnumerable<T> obj) {
            List<T> result = new List<T>( );
            foreach (var item in obj) {
                result.Add(DeepCopy(item));
            }
            return result;
        }
    }
}
