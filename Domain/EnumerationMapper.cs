using NPoco;

namespace Domain
{
    public class EnumerationMapper : DefaultMapper
    {
        public override System.Func<object, object> GetToDbConverter(System.Type destType, System.Type SourceType)
        {
            if (SourceType.IsEnumeration())
                return x => SourceType.GetProperty("Value").GetValue(x, new object[] { });
            return base.GetToDbConverter(destType, SourceType);
        }

        public override System.Func<object, object> GetToDbConverter(System.Type destType, System.Reflection.MemberInfo sourceMemberInfo)
        {
            return GetToDbConverter(destType, sourceMemberInfo.GetMemberInfoType());
        }

        public override System.Func<object, object> GetFromDbConverter(System.Type destType, System.Type sourceType)
        {
            if(destType.IsEnumeration())
                return x => destType.BaseType.GetMethod("FromValue").Invoke(null, new[] { x });
            return base.GetFromDbConverter(destType, sourceType);
        }

        public override System.Func<object, object> GetFromDbConverter(System.Reflection.MemberInfo destMemberInfo, System.Type sourceType)
        {
            return GetFromDbConverter(destMemberInfo.GetMemberInfoType(), sourceType);
        }
    }
}