namespace Framework.Configuration
{
    using Framework.Constant;
    using System;
    public abstract class ConfigurationOptions : IConfigurationOptions
    {
        private const string Suffix = ConfigurationConstant.ConfigurationOptionsSuffix;

        private string _sectionName;

        public string SectionName
        {
            get => GetSectionName();

            set => _sectionName = value;
        }

        private string GetSectionName()
        {
            if (string.IsNullOrWhiteSpace(_sectionName))
            {
                var className = GetType().Name;

                if (className.EndsWith(Suffix, StringComparison.OrdinalIgnoreCase))
                {
                    _sectionName = className.Substring(0, className.Length - Suffix.Length);
                }
            }

            return _sectionName;
        }
    }
}
