using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Pipl.APIs.Utils
{
    public static class EnumExtensions
    {
        public static Dictionary<Type, Dictionary<string, string>> _maps;

        public static string JsonEnumName<T>(T value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException("Expected a value of an enum type");

            if (!_maps.ContainsKey(typeof(T)))
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                var dict = fields.ToDictionary(f => f.Name, f => (f.GetCustomAttributes(typeof(EnumMemberAttribute), false)[0] as EnumMemberAttribute).Value);                

                _maps.Add(typeof(T), dict);
            }

            var map = _maps[type];

            return map[value.ToString()];
        }

        static EnumExtensions()
        {
            _maps = new Dictionary<Type, Dictionary<string, string>>();
        }
    }
}
