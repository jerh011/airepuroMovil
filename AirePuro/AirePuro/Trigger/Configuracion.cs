using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AirePuro.Trigger
{
    internal class Configuracion : TriggerAction<ImageButton>
    {
        protected override async void Invoke(ImageButton Imgbutton)
        {
            await Imgbutton.ScaleTo(0.95, 50, Easing.CubicOut);
            await Imgbutton.ScaleTo(1, 50, Easing.CubicIn);
        }
    }
}
