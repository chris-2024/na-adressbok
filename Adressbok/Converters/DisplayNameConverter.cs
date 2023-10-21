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
                // Check if FirstName or LastName is null, empty or whitespace
                if (!string.IsNullOrWhiteSpace(contact.FirstName) || !string.IsNullOrWhiteSpace(contact.LastName))
                {
                    // Return either firstname, lastname or both combined
                    // Trim any leading or trailing whitespace
                    return $"{contact.FirstName} {contact.LastName}".Trim();
                }
            }
        }
        catch { }

        // If contact or firstname + lastname is missing, return "..."
        return "...";
    }    

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "not implemented";
    }
}
