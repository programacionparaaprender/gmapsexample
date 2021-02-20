using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace GMapsExample
{
    public partial class Form1 : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;
        int filaSeleccionada = 0;
        double LatInicial = 20.09688123813906;
        double LngInicial = -89.6250915527344;
        public Form1()
        {
            InitializeComponent();
        }

        private DataTable crearTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Descripcion",typeof(string)));
            dt.Columns.Add(new DataColumn("Latitud", typeof(double)));
            dt.Columns.Add(new DataColumn("Longitud", typeof(double)));

            dt.Rows.Add(1,"Ubicación 1", LatInicial, LngInicial);
            dt.Rows.Add(2,"Ubicación 2", LatInicial, LngInicial);
            dt.Rows.Add(3,"Ubicación 3", LatInicial, LngInicial);
            dt.Rows.Add(4,"Ubicación 4", LatInicial, LngInicial);
            return dt;
        }

        private void inicializarGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = crearTabla();
        }

        private void inicializarMapa()
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            // Marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green);
            markerOverlay.Markers.Add(marker);
            //agregamos un tooltip de texto a los marcadores.
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud:{1} ", LatInicial, LngInicial);
            gMapControl1.Overlays.Add(markerOverlay);
        }


        private void gMapControl1_Load(object sender, EventArgs e)
        {
            //sección del gridview
            inicializarGridView();
            inicializarMapa();


            
        }

        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaSeleccionada = e.RowIndex;
            // Recuperamos los datos del grid
            txtDescripcion.Text = dataGridView1.Rows[filaSeleccionada].Cells["Descripcion"].Value.ToString();
            txtLatitud.Text = dataGridView1.Rows[filaSeleccionada].Cells["Latitud"].Value.ToString();
            txtLongitud.Text = dataGridView1.Rows[filaSeleccionada].Cells["Longitud"].Value.ToString();

            marker.Position = new PointLatLng(Convert.ToDouble(txtLatitud.Text),Convert.ToDouble(txtLongitud.Text));
            gMapControl1.Position = marker.Position;


        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            
            txtLatitud.Text = lat.ToString();
            txtLongitud.Text = lng.ToString();

            marker.Position = new PointLatLng(lat, lng);
            marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud:{1} ", lat, lng);
        }
    }
}
