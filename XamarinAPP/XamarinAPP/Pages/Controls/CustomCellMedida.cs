using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinAPP.Pages.Controls
{
    public class CustomCellMedida : ViewCell
    {
        Label lblDescripcion, lblValor, lblComentario;

        public static readonly BindableProperty DescripcionProperty = BindableProperty.Create("descripcion", typeof(string), typeof(CustomCellMedida), "");
        public static readonly BindableProperty ValorProperty = BindableProperty.Create("valor", typeof(string), typeof(CustomCellMedida), "");
        public static readonly BindableProperty ComentarioProperty = BindableProperty.Create("comentario", typeof(string), typeof(CustomCellMedida), "");

        public string descripcion
        {
            get { return (string)GetValue(DescripcionProperty); }
            set { SetValue(DescripcionProperty, value); }
        }

        public string valor
        {
            get { return (string)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        public string comentario
        {
            get { return (string)GetValue(ComentarioProperty); }
            set { SetValue(ComentarioProperty, value); }
        }

        public CustomCellMedida()
        {
            var grid = new Grid { Padding = new Thickness(10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });

            lblDescripcion = new Label { FontAttributes = FontAttributes.Bold };
            lblValor = new Label();
            lblComentario = new Label { HorizontalTextAlignment = TextAlignment.End };

            grid.Children.Add(lblDescripcion);
            grid.Children.Add(lblValor, 1, 0);
            grid.Children.Add(lblComentario, 2, 0);

            View = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                lblComentario.Text = descripcion;
                lblValor.Text = valor;
                lblComentario.Text = comentario;
            }
        }
    }
}
