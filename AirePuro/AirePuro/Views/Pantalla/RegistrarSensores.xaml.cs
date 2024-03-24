﻿using AirePuro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirePuro.Views.Pantalla
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarSensores : ContentPage
    {
        public RegistrarSensores()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            BindingContext = new VMRegistrarSensores(Navigation);
        }

    }
}