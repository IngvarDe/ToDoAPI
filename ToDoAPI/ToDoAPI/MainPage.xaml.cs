//using Android.OS;
using ToDoApi.DataServices;
using System.Diagnostics;

namespace ToDoApi;

public partial class MainPage : ContentPage
{

	private readonly IRestDataService _dataService;

	public MainPage(IRestDataService dataService)
	{
        InitializeComponent();

        _dataService = dataService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _dataService.GetAllToDosAsync();
	}

	async void OnAddToDoClicked(object sender, EventArgs e)
	{
        System.Diagnostics.Debug.WriteLine("Add button clicked!");
	}

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        System.Diagnostics.Debug.WriteLine("Item changed clicked!");
    }
}

