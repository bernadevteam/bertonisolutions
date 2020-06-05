using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tasker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ViewModels.TaskViewModel taskItemViewModel = new ViewModels.TaskViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = taskItemViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var tasks = await Services.TaskerDatabaseService.Instance.GetItemsAsync();
            foreach(var task in tasks)
            {
                taskItemViewModel.Tasks.Add(task);
            }
            listView.ItemsSource = taskItemViewModel.Tasks;
        }

        async void MarkComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var task = ((Models.TaskItem)mi.CommandParameter);
            await Services.TaskerDatabaseService.Instance.SaveItemAsync(task);
            taskItemViewModel.Tasks.Remove(task);
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var task = ((Models.TaskItem)mi.CommandParameter);
            await Services.TaskerDatabaseService.Instance.DeleteItemAsync(task);
            taskItemViewModel.Tasks.Remove(task);

        }
        async void TaskButton_Clicked(object sender, EventArgs args)
        {
            var newTask = taskItemViewModel.GetItem();
            await Services.TaskerDatabaseService.Instance.SaveItemAsync(newTask);
            taskItemViewModel.Tasks.Add(newTask);
            taskItemViewModel.Name = "";
        }

    }
}
