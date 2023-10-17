using Adressbok.Models;
using System.Globalization;

namespace Adressbok.Converters;

public class DisplayNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var contact = value as ContactModel;

            if (contact != null)
            {
                if (!string.IsNullOrWhiteSpace(contact.FirstName) || !string.IsNullOrWhiteSpace(contact.LastName))
                {
                    return $"{contact.FirstName} {contact.LastName}".Trim();
                }
            }
        }
        catch { }

        return "---";
    }    

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "not implemented";
    }
}
