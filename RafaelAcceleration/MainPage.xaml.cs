using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RafaelAcceleration.Interfaces;
using RafaelAcceleration.ViewModels;
using Xamarin.Forms;

namespace RafaelAcceleration
{
    public partial class MainPage : ContentPage
    {
        MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainViewModel();   
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
        