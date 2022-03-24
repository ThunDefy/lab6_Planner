using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using ReactiveUI;
using System.Text;
using lab6_Planner.Models;

namespace lab6_Planner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //public List<Plans> AllPlans { get; set; }
        public ObservableCollection<Plans>Items{ get; set;}
        private Dictionary<DateTimeOffset, List<Plans>> PlansByDay;

        
        ViewModelBase content;
        bool Edit = false;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value );
        }


        string title = "";
        string description = "";



        public void ChangeView() // Переключение окон
        {
            if (Content is MainViewModel) Content = new AddViewModel();

            else
            {
                Content = new MainViewModel();
                Edit = false;
                Title = "";
                Description = "";
            }
        }

        
        DateTimeOffset date = DateTimeOffset.Now.Date;
        public void AddNew( DateTimeOffset date, Plans item ) // Добавить новую запись в словарь
        {
            if (!PlansByDay.ContainsKey(date))
            {
                PlansByDay.Add(date, new List<Plans>());
            }
            PlansByDay[date].Add(item);
            Change_Items(Date);
        } 


        public String Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }      
        public DateTimeOffset Date
        {
            get { return date; }
            set { this.RaiseAndSetIfChanged(ref date, value); Change_Items(date); }
        }
        public String Description
        {
            get { return description; }
            set { this.RaiseAndSetIfChanged(ref description, value); }
        }
        

        public MainWindowViewModel ()
        {
            PlansByDay = new Dictionary<DateTimeOffset, List<Plans>>();
            Items = new ObservableCollection<Plans>();
               Content = new MainViewModel();
        }
        public void ViewExItem( Plans item ) // Просмотор существующего
        {
            Edit = true;
            Title = item.Title;
            Description = item.Description;
            ChangeView();
        }

        public void SavePlan( Plans item ) 
        {
            if (Title != "")
            {
                if (Edit)
                {
                    var exitem = PlansByDay[date].Find(i => i.Equals(item));
                    exitem.Title = Title;
                    exitem.Description = Description;
                    Edit = false;
                }
                else
                {
                    /*var newItem = new Plans(Title, Description, Date);
                    AllPlans.Add(newItem);*/
                    AddNew(Date, new Plans(Title, Description, date));
                }
                ChangeView();
            }
        }

        public void DeletePlan(Plans item)
        {
            PlansByDay[date].Remove(item);
            Change_Items(date);
        }



        public void Change_Items( DateTimeOffset date ) // Обновить отображаеммые планы
        {
            if (PlansByDay.ContainsKey(date)==false) Items.Clear();

            else
            {
                Items.Clear(); // дабы не дублировались
                foreach ( var item in PlansByDay[date] ) Items.Add( item ); // заполняем контрол данными из словаря
            }
            //if (exitem.Date.Equals(item)) Items.Add(exitem);
        }


    }
}
