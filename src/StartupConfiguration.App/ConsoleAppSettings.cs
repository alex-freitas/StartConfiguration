using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StartupConfiguration.App
{
    public class ConsoleAppSettings
    {
        [Required]
        public string RequiredSetting { get; set; }

        [MustHaveKey("Lorem", ErrorMessage = "The English Setting is required")]
        public Dictionary<string, string> RequiredRegionalizedSetting { get; set; }

        public class MustHaveKeyAttribute : ValidationAttribute
        {
            private readonly string _key;

            public MustHaveKeyAttribute(string key)
            {
                _key = key;
            }

            public override bool IsValid(object value)
            {
                var dictionary = value as Dictionary<string, string>;

                return dictionary != null && dictionary.ContainsKey(_key);
            }
        }
    }


   
}
