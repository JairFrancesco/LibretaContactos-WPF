using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace LibretaContactosWPF
{
public class ContactoCollection : System.Collections.CollectionBase
{
    public event NuevoContactoEventHandler NuevoContacto;
    public delegate void NuevoContactoEventHandler(Contacto nContacto);


    public void Add(Contacto nuevo)
    {
        List.Add(nuevo);
        if (NuevoContacto != null)
        {
            NuevoContacto(nuevo);
        }
    }



    public void Remove(int index)
    {
        if (index > Count - 1 | index < 0)
        {
            MessageBox.Show("Indice no Valido");
        }
        else
        {
            List.RemoveAt(index);
        }
    }

    public Contacto Item(int index)
    {
       return (Contacto)List[index];
    }

}
}