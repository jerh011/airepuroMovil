using AirePuro.Model;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;
using AirePuro.Views.PantallasMenuHamburgesa;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMhistorial : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        private MHistorial Registros;
        private Ventiladoressim _VENTILADORES = Ventiladoressim.Instancia;//BORRAR
        private HistorialConexion _HistorialConexion = HistorialConexion.Instancia;
        private Logueo _Logueo = Logueo.Instancia;
        #endregion

        #region CONTRUCTOR
        public VMhistorial(INavigation navigation,MHistorial historial)
        {
            Navigation = navigation;
            Lista = historial;
        }
        #endregion

        #region OBJETOS
        public MHistorial Lista
        {
            get { return Registros; }
            set
            {
                SetValue(ref Registros, value);
                OnpropertyChanged();

            }
        }
       


        #endregion
        #region PROCESOS
        
        public void ProcesoSimple()
        {

        }
        
       

        #endregion
        #region COMANDOS
      

        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        #endregion
       
    }
}
