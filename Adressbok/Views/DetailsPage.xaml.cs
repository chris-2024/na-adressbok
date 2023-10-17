using Adressbok.ViewModels;

namespace Adressbok.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}