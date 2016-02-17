using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.ComponentModel;
using Microsoft.Win32;
using MahApps.Metro.Controls.Dialogs;
using System.IO;

namespace LibretaContactosWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string[] telefonos;
        private string[] correos;
        private ContactoCollection Contactos;
        private int indice = 0;
        private int indice2 = 0;
        

        private void AgregarContacto(object sender, EventArgs e)
        {
            try
            {
                Contacto NuevoContacto = default(Contacto);

                if ((!string.IsNullOrEmpty(txtNombre.Text) & !string.IsNullOrEmpty(txtApellido.Text)))
                {
                    if (dpNacimiento.SelectedDate.HasValue)
                    {
                        TimeSpan ts = DateTime.Now - dpNacimiento.SelectedDate.Value;
                        if (ts.Days <= 0)
                        {
                            this.ShowMessageAsync("Fecha", "Porfavor seleccione una fecha menor a la actual");
                        }
                        else
                        {
                            if ((telefonos != null))
                            {
                                txtTelefono.Text = string.Join(" - ", telefonos);
                            }
                            if ((correos != null))
                            {
                                txtCorreo.Text = string.Join(" - ", correos);
                            }
                            NuevoContacto = new Contacto();
                            NuevoContacto.Nombre = txtNombre.Text;
                            NuevoContacto.Apellido = txtApellido.Text;
                            NuevoContacto.FechaNacimiento = dpNacimiento.SelectedDate.Value;
                            NuevoContacto.Telefono = txtTelefono.Text;
                            NuevoContacto.CorreoElectronico = txtCorreo.Text;
                            Contactos.Add(NuevoContacto);
                            telefonos = null;
                            correos = null;
                            indice = 0;
                            indice2 = 0;
                        }
                    }
                    else
                    {
                        this.ShowMessageAsync("Fecha", "Porfavor seleccione su fecha de nacimiento");
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar el nombre y apellido", "Faltan datos", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Contactos_NuevoContacto(Contacto nContacto)
        {
            lvwContactos.Items.Add(nContacto);
        }

        private void lvwContactos_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AbrirLibretaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrircsv = new OpenFileDialog();
            abrircsv.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            abrircsv.Filter = "Archivos csv|*.csv";
            abrircsv.Title = "Abrir libretas de tipo csv";
            string linea = "";
            string archivo = null;
            int i = 0;
            string[] lista = null;
            if (abrircsv.ShowDialog() == true)
            {
                archivo = abrircsv.FileName;
                StreamReader leercsv = new StreamReader(archivo);
                linea = leercsv.ReadLine();
                Contacto NuevoContacto = default(Contacto);
                do
                {
                    lista = linea.Split(',');
                    NuevoContacto = new Contacto();
                    NuevoContacto.Nombre = lista[0];
                    NuevoContacto.Apellido = lista[1];
                    NuevoContacto.FechaNacimiento = DateTime.Parse(lista[2]);
                    NuevoContacto.Telefono = lista[3];
                    NuevoContacto.CorreoElectronico = lista[4];
                    Contactos.Add(NuevoContacto);
                    lista = null;
                    linea = leercsv.ReadLine();
                } while (linea != null);
            }
        }

        private void GuardarLibretaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardarlibretacsv = new SaveFileDialog();
            guardarlibretacsv.Filter = "Archivo csv(.csv)|*.csv";
            guardarlibretacsv.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            if (guardarlibretacsv.ShowDialog() == true)
            {
                StreamWriter newcsv = new StreamWriter(guardarlibretacsv.FileName);
                for (int i = 0; i < Contactos.Count; i++)
			    {
			        Contacto conta = Contactos.Item(i);
                    string linea = conta.Nombre + "," + conta.Apellido + "," + conta.FechaNacimiento + "," + conta.Telefono + ","+ conta.CorreoElectronico;
                    newcsv.Write(linea);
                    newcsv.Write(newcsv.NewLine);
			    }
                newcsv.Close();   
            }
        }

        private void btnAddTelefono_Click(object sender, EventArgs e)
        {
            Array.Resize(ref telefonos, indice + 1);
            telefonos[indice] = txtTelefono.Text.ToString();
            indice += 1;
            txtTelefono.Clear();
        }

        private void btnAddCorreo_Click(object sender, EventArgs e)
        {
            Array.Resize(ref correos, indice2 + 1);
            correos[indice2] = txtCorreo.Text.ToString();
            indice2 += 1;
            txtCorreo.Clear();
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            telefonos = null;
            correos = null;
            indice = 0;
            indice2 = 0;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            lvwContactos.Items.Clear();
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            Contactos = new ContactoCollection();
            btnAgregar.Click += AgregarContacto;
            Contactos.NuevoContacto += Contactos_NuevoContacto;
            btnDeshacer.Click += btnDeshacer_Click;
            btnClean.Click += btnClean_Click;
        }
        void Salir(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
