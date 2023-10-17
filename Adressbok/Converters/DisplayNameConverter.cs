using Adressbok.Models;
using System.Globalization;

namespace Adressbok.Converters;

public class DisplayNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var contact = value as ContactModel;

        if (string.IsNullOrWhiteSpace(contact.FirstName) && string.IsNullOrWhiteSpace(contact.LastName))
        {
            return "---";
        }

        return $"{contact.FirstName} {contact.LastName}".Trim();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
