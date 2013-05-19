using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Voronoi_2._0
{
    public class Voronoi : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        public Voronoi()
        {

        }
      
        

        protected override void OnClick()
        {
            frmVoronoi Voronoi = new frmVoronoi();
            Voronoi.ShowDialog();

        }

        protected override void OnUpdate()
        {
        }
    }
}
