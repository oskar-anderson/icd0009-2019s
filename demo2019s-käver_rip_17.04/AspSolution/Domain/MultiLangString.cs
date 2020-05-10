using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class MultiLangString : MultiLangString<Guid>, IDomainBaseEntity
    {
    }

    public class MultiLangString<TKey> : DomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>

    {
        private static string _defaultCulture = "en";

        public virtual ICollection<Translation>? Translations { get; set; }


        public MultiLangString()
        {
        }

        public MultiLangString(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
        {
        }

        public MultiLangString(string value, string culture)
        {
            SetTranslation(value, culture);
        }


        public void SetTranslation(string value)
        {
            SetTranslation(value, Thread.CurrentThread.CurrentUICulture.Name);
        }

        public void SetTranslation(string value, string culture)
        {
            if (Translations == null)
            {
                Translations = new List<Translation>();
            }

            var translation = Translations.FirstOrDefault(t => t.Culture == culture);
            if (translation != null)
            {
                translation.Value = value;
            }
            else
            {
                Translations.Add(new Translation()
                {
                    Value = value,
                    Culture = culture
                });
            }
        }

        public string? Translate(string? culture = null)
        {
            if (culture == null)
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }

            var translation = Translations?
                .FirstOrDefault(t => t.Culture == culture);
            if (translation != null)
            {
                return translation.Value;
            }

            translation = Translations?.FirstOrDefault(t => culture.StartsWith(t.Culture));
            if (translation != null)
            {
                return translation.Value;
            }

            translation = Translations?.FirstOrDefault(t => culture.StartsWith(_defaultCulture));

            if (translation != null)
            {
                return translation.Value;
            }

            return Translations?.First().Value;
        }

        public override string ToString()
        {
            return Translate() ?? "?????";
        }

        // automatic casting to string when needed
        public static implicit operator string(MultiLangString<TKey> m) => m.ToString();

        // automatic casting from string when needed
        public static implicit operator MultiLangString<TKey>(string s) => new MultiLangString<TKey>(s);
    }
}