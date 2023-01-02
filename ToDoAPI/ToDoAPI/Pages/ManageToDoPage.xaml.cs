//using Android.OS;
using System.Diagnostics;
using ToDoApi.DataServices;
using ToDoApi.Models;

namespace ToDoApi.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
	private readonly IRestDataService _dataService;
	ToDo _toDo;
	bool _isNew;

	public ToDo ToDo
	{
		get => _toDo;
		set
		{
			_isNew = IsNew(value);
			_toDo = value;
			OnPropertyChanged();
		}
	}

	public ManageToDoPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;
		BindingContext = this;
	}

	bool IsNew(ToDo toDo)
	{ 
		if(toDo.Id == 0)
		{
			return true;
		}

		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
            System.Diagnostics.Debug.WriteLine("--> Add new Item");
			await _dataService.AddTodoAsync(ToDo);
		}
		else
		{
            System.Diagnostics.Debug.WriteLine("--> Update an Item");
            await _dataService.AddTodoAsync(ToDo);
        }

        await Shell.Current.GoToAsync("..");
    }

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteTodoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}