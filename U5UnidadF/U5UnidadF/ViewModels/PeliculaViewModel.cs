using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using U5UnidadF.Views;
using U5UnidadF.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace U5UnidadF.ViewModels
{
    class PeliculaViewModel : INotifyPropertyChanged
    {

       
        AgregarPeliculaView vistaPelicula;
        DetallesPeliculaView vistaDetalles;
        EditarPeliculaView vistaEditar;

        public IEnumerable<Pelicula> CarteleraOrdenado => Cartelera.OrderBy(x => x.Titulo);
        public ObservableCollection<Pelicula> Cartelera { get; set; } = new ObservableCollection<Pelicula>();

        public Pelicula Pelicula { get; set; } 
        public string Error { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand MostrarDetallesCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }


        public PeliculaViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(Agregar);
            EliminarCommand = new Command<Pelicula>(Eliminar);
            MostrarDetallesCommand = new Command<Pelicula>(MostrarDetalles);
            EditarCommand = new Command<Pelicula>(Editar);
            GuardarCommand = new Command(Guardar);

        }
        int indice;
        private void Editar(Pelicula p)
        {
           
            indice = Cartelera.IndexOf(p);

           Pelicula  = new Pelicula 
            {

             Titulo = p.Titulo,
             DuracionMin= p.DuracionMin,
             Calificacion = p.Calificacion,
             Director = p.Director,
             Año = p.Año,
             Sinopsis = p.Sinopsis,
            Genero =p.Genero,
            Portada =p.Portada,

            };

            vistaEditar = new EditarPeliculaView()
            {
                BindingContext = this
            };

            App.Current.MainPage.Navigation.PushAsync(vistaEditar);
        }

        private void Guardar()
        {
           
            Cartelera[indice] = Pelicula; 
            Serializar();
            App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void MostrarDetalles(Pelicula obj)
        {

            vistaDetalles = new DetallesPeliculaView()
            {
                BindingContext = obj
            };
            App.Current.MainPage.Navigation.PushAsync(vistaDetalles);
        }

        private void Eliminar(Pelicula p)
        {
            if (p != null)
            {
                Cartelera.Remove(p);
                Serializar();
            }
        }

        private void Agregar()
        {
            if (Pelicula != null)
            {
                Error = "";

                if (string.IsNullOrWhiteSpace(Pelicula.Titulo))
                {
                    Error = "Escriba el nombre de la pelicula";
                }

                if (string.IsNullOrWhiteSpace(Pelicula.Director))
                {
                    Error = "Escriba el director de la pelicula";
                }

                if (string.IsNullOrWhiteSpace(Pelicula.Director))
                {
                    Error = "Escriba el director de la pelicula";
                }

                if (string.IsNullOrWhiteSpace(Error)) 
                {
                    Cartelera.Add(Pelicula);
                    CambiarVista("Ver");
                    Serializar();

                }

                Change();

            }
        }

        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void CambiarVista(string vista)
        {
            if (vista == "Agregar")
            {
                Pelicula = new Pelicula(); 
                vistaPelicula = new AgregarPeliculaView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaPelicula);
                Error = "";
            }
            else if (vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
                Error = "";
            }

        }

        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Cartelera));
        }

        void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            if (File.Exists(file))
            {
                Cartelera = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(File.ReadAllText(file));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

