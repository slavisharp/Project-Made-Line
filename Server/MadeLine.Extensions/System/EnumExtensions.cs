namespace System
{
    using ComponentModel.DataAnnotations;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0)
            {
                return enumValue.ToString();
            }
            else
            {
                var description = ((DisplayAttribute)attributes[0]).Name;
                return description;
            }
        }
    }
}
