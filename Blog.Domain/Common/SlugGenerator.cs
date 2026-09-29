using System.Globalization;
using System.Text;

namespace Blog.Domain.Common
{
    public static class SlugGenerator
    {
        public static string Generate(string value)
        {
            value = DomainGuard.Required(value, nameof(value));

            var normalized = value.ToLowerInvariant().Trim();

            normalized = RemoveAccents(normalized);

            var characters = normalized.Select(character => char.IsLetterOrDigit(character) ? character : '-').ToArray();

            var slug = new string(characters);

            while (slug.Contains("--"))
            {
                slug = slug.Replace("--", "-");
            }

            return slug.Trim('-');
        }

        private static string RemoveAccents(string value)
        {
            var normalized = value.Normalize(NormalizationForm.FormD);

            var characters = normalized.Where(character => CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(characters).Normalize(NormalizationForm.FormC);
        }
    }
}
