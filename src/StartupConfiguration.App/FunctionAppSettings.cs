using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StartupConfiguration.App
{
    /*
    Json configuration not working with Dictionary    
    https://github.com/Azure/azure-functions-core-tools/issues/1472
    https://github.com/Azure/azure-functions-core-tools/issues/1473
    */
    public class FunctionAppSettings
    {
        public FunctionAppSettings()
        {

        }

        public string RequiredSetting { get; set; }

        [MustHaveKey("en", ErrorMessage = "The English Setting is required")]
        public string RequiredRegionalizedSetting { get; set; }
        
        public Dictionary<string, string> GetRegionalizedSettingDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(RequiredRegionalizedSetting);
        }

        public class MustHaveKeyAttribute : ValidationAttribute
        {
            private readonly string _key;

            public MustHaveKeyAttribute(string key)
            {
                _key = key;
            }

            public override bool IsValid(object value)
            {
                var json = value.ToString();

                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                return dictionary != null && dictionary.ContainsKey(_key);
            }
        }
    }
}
