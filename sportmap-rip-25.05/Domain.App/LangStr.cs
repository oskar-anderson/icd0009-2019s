using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Domain.Base;

namespace Domain.App
{
    public class LangStr : DomainEntityIdMetadata
    {
        private static string _defaultCulture = "en";
        //public virtual ICollection<TLangStrTranslation>? Translations { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }

        [InverseProperty(nameof(GpsSessionType.Name))]
        public ICollection<GpsSessionType>? GpsSessionTypeNames { get; set; }

        [InverseProperty(nameof(GpsSessionType.Description))]
        public ICollection<GpsSessionType>? GpsSessionTypeDescriptions { get; set; }

        [InverseProperty(nameof(GpsLocationType.Name))]
        public ICollection<GpsLocationType>? GpsLocationTypeNames { get; set; }

        [InverseProperty(nameof(GpsLocationType.Description))]
        public ICollection<GpsLocationType>? GpsLocationTypeDescriptions { get; set; }

        [InverseProperty(nameof(Track.Name))] public ICollection<Track>? TrackNames { get; set; }

        [InverseProperty(nameof(Track.Description))]
        public ICollection<Track>? TrackDescriptions { get; set; }


        #region Constructors

        public LangStr()
        {
        }

        public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
        {
        }

        public LangStr(string value, string culture)
        {
            SetTranslation(value, culture);
        }

        #endregion

        public void SetTranslation(string value)
        {
            SetTranslation(value, Thread.CurrentThread.CurrentUICulture.Name);
        }

        public void SetTranslation(string value, string culture)
        {
            if (Translations == null)
            {
                Translations = new List<LangStrTranslation>();
            }

            var translation = Translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
            {
                Translations.Add(new LangStrTranslation()
                {
                    Value = value,
                    Culture = culture,
                });
            }
            else
            {
                translation.Value = value;
            }
        }


        public string? Translate(string? culture = null)
        {
            if (Translations == null) return null;

            culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

            /*
             in database - en, en-GB
             in query - en, en-GB, en-US
             */

            // do we have exact match - en-GB == en-GB
            var translation = Translations.FirstOrDefault(t => t.Culture == culture);
            if (translation != null)
            {
                return translation.Value;
            }

            // do we have match without the region en-US.StartsWith(en)
            translation = Translations.FirstOrDefault(t => culture.StartsWith(t.Culture));
            if (translation != null)
            {
                return translation.Value;
            }

            // try to find the default culture
            translation = Translations.FirstOrDefault(t => culture.StartsWith(_defaultCulture));
            if (translation != null)
            {
                return translation.Value;
            }

            // just return the first in list or null
            return Translations?.First().Value;
        }


        public override string ToString()
        {
            return Translate() ?? "?????";
        }

        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators
        // define automatic (implicit) conversions when needed
        // "foo" + new LangStr("bar") => "foobar"
        public static implicit operator string(LangStr? l) => l?.ToString() ?? "null";
        // langStrProperty = "foobar"
        public static implicit operator LangStr(string s) => new LangStr(s);
    }
}