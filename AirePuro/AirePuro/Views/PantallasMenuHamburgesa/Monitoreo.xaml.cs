﻿using AirePuro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirePuro.Views.PantallasMenuHamburgesa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Monitoreo : ContentPage
    {
        public Monitoreo()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new VMMonitoreo(Navigation);
        }

    }
}
