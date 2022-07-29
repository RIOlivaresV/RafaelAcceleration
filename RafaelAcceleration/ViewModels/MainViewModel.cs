using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using RafaelAcceleration.Interfaces;
using RafaelAcceleration.Models;
using Xamarin.Forms;

namespace RafaelAcceleration.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemds { get; }
        public Command onUpdateUI { get; }
        public Command<Item> ItemTap { get; }
        private string _stepCounter;
        public string StepCounter { get => _stepCounter; set => SetProperty(ref _stepCounter, value); }
        public string StepImage { get; set; }
        private string _activity;
        public string ActivityDetector { get => _activity; set => SetProperty(ref _activity, value); }
        public string ActivityImage { get; set; }
        public int Steps { get; set; }
        private string _buttonTitle;
        public string ButtonTitle { get => _buttonTitle; set => SetProperty(ref _buttonTitle, value); }
        
        IStepCounterRafa service;

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();
            Title = "Step Counter";
            Steps = 0;
            ItemTap = new Command<Item>(OnItemSelected);
            service = DependencyService.Get<IStepCounterRafa>();
            _stepCounter = Steps.ToString();
            StepImage = "footprints.png";
            _activity = "";
            ActivityImage = "run.png";
            onUpdateUI = new Command(OnItemSelected);
            ButtonTitle = "Active activity reconizer";
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }


        private async void OnItemSelected(object obj)
        {
            service = DependencyService.Get<IStepCounterRafa>();
            if (!service.IsAvailable())
            {
                Boolean dialog = await App.Current.MainPage.DisplayAlert("Step Counter", "You are starting the step counter", "Accept", "Cancel");
                if (dialog)
                {
                    service.InitService();
                }
                Steps = service.Steps;
                _stepCounter = Steps.ToString();
                ButtonTitle = "Update UI";
            }
            else
            {
                IsBusy = true;
                Steps = service.Steps;
                _stepCounter = Steps.ToString();
                Console.WriteLine(service.Steps);
                IsBusy = false;
            }
        }
    }
}
