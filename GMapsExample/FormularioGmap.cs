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
    public partial class FormularioGmap : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;
        int filaSeleccionada = 0;
        double LatInicial = 20.09688123813906;
        double LngInicial = -89.6250915527344;
        List<PointLatLng> coordenadas = new List<PointLatLng>();


        public FormularioGmap()
        {
            InitializeComponent();
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

            //// Marcador
            markerOverlay = new GMapOverlay("Marcador");
            //marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green);
            //markerOverlay.Markers.Add(marker);
            ////agregamos un tooltip de texto a los marcadores.
            //marker.ToolTipMode = MarkerTooltipMode.Always;
            //marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud:{1} ", LatInicial, LngInicial);
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void FormularioGmap_Load(object sender, EventArgs e)
        {

            inicializarMapa();
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            // Marcador
            //markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.green);
            markerOverlay.Markers.Add(marker);
            //agregamos un tooltip de texto a los marcadores.
            marker.ToolTipMode = MarkerTooltipMode.Always;
            //marker.ToolTipText = string.Format("Ubicación: \n Latitud:{0} \n Longitud:{1} ", LatInicial, LngInicial);
            //gMapControl1.Overlays.Add(markerOverlay);
            PointLatLng coor = new PointLatLng();
            coor.Lat = lat;
            coor.Lng = lng;
            coordenadas.Add(coor);
            markerOverlay.Polygons.Clear();

            GMapPolygon poligono = new GMapPolygon(coordenadas, "Poligono");
            markerOverlay.Polygons.Add(poligono);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            markerOverlay.Polygons.Clear();
            markerOverlay.Markers.Clear();
            coordenadas.Clear();
            
        }
    }
}
