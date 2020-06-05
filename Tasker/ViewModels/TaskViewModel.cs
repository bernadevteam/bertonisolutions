using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tasker.Models;

namespace Tasker.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        public ObservableCollection<Models.TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();

        Models.TaskItem TaskItem = new Models.TaskItem();
        public TaskViewModel()
        {
        }
        private string _name;
        private int _id;
        private bool _isDone;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public bool IsDone
        {
            get
            {
                return _isDone;
            }
            set
            {
                SetProperty(ref _isDone, value);
            }
        }

        public Models.TaskItem GetItem()
        {
            TaskItem taskItem = new TaskItem();
            taskItem.ID = ID;
            taskItem.Name = Name;
            taskItem.IsDone = IsDone;
            return taskItem;
        }
    }
}
