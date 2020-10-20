using Dynamitey;
using GraphLibrary.Attributes;
using GraphLibrary.Extensions.SystemTypeExtensions;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace GraphLibrary.Info
{
    [Serializable]
    public sealed class Info<TSource> : DynamicObject
    {
        private readonly Dictionary<string, object> members;

        public Info(TSource obj)
        {
            members = new Dictionary<string, object>();
            foreach (var property in obj.GetMarkedProperties<InfoMemberAttribute>(true))
                Dynamic.InvokeSet(this, property.Name, property.GetValue(obj));
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            members[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (members.ContainsKey(binder.Name))
            {
                result = members[binder.Name];
                return true;
            }
            return false;
        }

        public static explicit operator Dictionary<string, object>(Info<TSource> dto)
        {
            return dto.members;
        }
    }
}
