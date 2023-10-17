using Adressbok.ViewModels;

namespace Adressbok.Views;

public partial class ManageContactPage : ContentPage
{
	public ManageContactPage(ManageContactViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}