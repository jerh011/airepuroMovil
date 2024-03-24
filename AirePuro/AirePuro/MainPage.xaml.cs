using AirePuro.Views.PantallasMenuHamburgesa;
using AirePuro.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AirePuro
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Master = new Nav();
            this.Detail = new NavigationPage(new Monitoreo());
            App.MasterDet = this;

        }
    }
}
