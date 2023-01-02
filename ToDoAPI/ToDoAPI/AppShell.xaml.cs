using ToDoApi.Pages;

namespace ToDoApi;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ManageToDoPage), typeof(ManageToDoPage));
	}
}
