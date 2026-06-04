using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.Utility
{
    public class Utility
    {
        private volatile static RMILCSDbEntities singleTonObject;

        public Utility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);
            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            Array enumArry = Enum.GetValues(enumerationType);
            var dictionary = new Dictionary<int, string>();
            foreach (int value in enumArry)
            {
                string enumDescription = "";
                object enumObj = Enum.Parse(enumerationType, value.ToString());

                string description = GetDescription((Enum)enumObj);
                if (description != null)
                    enumDescription = description;
                else
                    enumDescription = Enum.GetName(enumerationType, value);
                dictionary.Add(value, enumDescription);
            }

            return dictionary;
        }

        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static string GetAttributeValue<T>(Enum e,
        Func<T, object> selector) where T : Attribute
        {

            var output = e.ToString();
            var member = e.GetType().GetMember(output).First();
            var attributes = member.GetCustomAttributes(typeof(T), false);

            if (attributes.Length > 0)
            {
                var firstAttr = (T)attributes[0];
                var str = selector(firstAttr).ToString();
                output = string.IsNullOrWhiteSpace(str) ? output : str;
            }

            return output;
        }


        public static void CopyTo(object S, object T)
        {
            foreach (var pS in S.GetType().GetProperties())
            {
                foreach (var pT in T.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(T, new object[] { pS.GetGetMethod().Invoke(S, null) });
                }
            };
        }


        public static string base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return (encodedData);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error in base64Encode" + ex.Message));
            }
        }

        public static string base64Decode(string sData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error in base64Decode" + ex.Message));
            }

        }
    }
}