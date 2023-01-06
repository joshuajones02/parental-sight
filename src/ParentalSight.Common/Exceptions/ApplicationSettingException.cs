namespace ParentalSight
{
    using System;

    public class ApplicationSettingException : Exception
    {
        public const string Template = "The appsetting `{key}` must not be null, default, or empty";

        public ApplicationSettingException(string key)
            : base(Template.Replace("{key}", key))
        {
        }
    }
}