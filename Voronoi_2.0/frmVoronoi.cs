using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Server;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcCatalogUI;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
//using System.Threading;
//using System.Threading.Tasks;






namespace Voronoi_2._0
{
    

    public partial class frmVoronoi : Form
    {
        
        public frmVoronoi()
        {
            
            
            InitializeComponent();
            cboOutputExtent.SelectedIndex = 2;
            cboWeightFieldPoint.Enabled = false;
            cboWeightFieldLine.Enabled = false;
            cboWeightFieldPolygon.Enabled = false;
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)(ArcMap.Application.Document); //Access current document
            IMap pMap = mxDoc.FocusMap; //set pMap as current document
            AddtoShapefileCombobox(pMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"); //get layers in arcmap and populate comboboxes
            try
            {
                cboFCPoint.SelectedIndex = 0; //if point name combobox, select the first layer and display it
            }
            catch
            {
            }
            try
            {
                cboFCLine.SelectedIndex = 0; //if line name combobox is populated, select the first layer and display it
            }
            catch
            {
            }
            try
            {
                cboFCPoly.SelectedIndex = 0; //if polygon name combobox is populated, select the first layer and display it
            }
            catch
            {
            }

            if (cboFCPoint.SelectedIndex == -1) //if not populated, disable the boxes until checked. If populated, check the box
            {
                
                cboFCPoint.Enabled = false;
                chkWeightFieldPoint.Enabled = false;
            }
            else
            {
                chkPointEnabled.Checked = true;
            }
            if (cboFCLine.SelectedIndex == -1)
            {
                cboFCLine.Enabled = false;
                chkWeightFieldLine.Enabled = false;
            }
            else
            {
                chkLineEnabled.Checked = true;
            }
            if (cboFCPoly.SelectedIndex == -1)
            {
                cboFCPoly.Enabled = false;
                chkWeightFieldPolygon.Enabled = false;
            }
            else
            {
                chkPolyEnabled.Checked = true;
            }

            try
            {      
                GetLayerInfo();
            }
            catch
            {
            }
                
            
         
        }

        private void txtOutVoronoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOutDistRaster_TextChanged(object sender, EventArgs e)
        {

        }
    

        private void cboFCPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grabs point feature classes from table of contents. Populates combobox below with NUMERICAL fields of attribute table to select from.
            //upon selection, get the UNITS of the selected shapefile from ArcObjects, and populate field next to Cell size factor 
            GetLayerInfo();
       
        }

        private void cboFCLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLayerInfo();
        }

        private void cboFCPoly_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLayerInfo();
        }
 

        private void btnOpenPointFC_Click(object sender, EventArgs e) //use ArcCatalog to browse to map if not in TOC
        {

            AddFeatureClassToMap("Point");
         
        }

        private void btnOpenLineFC_Click(object sender, EventArgs e)
        {
            AddFeatureClassToMap("Line");

        }

        private void btnOpenPolyFC_Click(object sender, EventArgs e)
        {
            AddFeatureClassToMap("Polygon");

        }


        private void btnOpenDistRaster_Click(object sender, EventArgs e)
        {
            //select the filepath for the raster to be created from ArcCatalog
            ESRI.ArcGIS.CatalogUI.IGxDialog pGxDialog;
            ESRI.ArcGIS.Catalog.IGxObjectFilter pFolderFilter;

            ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pFilterCol;

            pGxDialog = new GxDialogClass();
            pFolderFilter = new ESRI.ArcGIS.Catalog.GxFilterRasterDatasets();


            pGxDialog.Title = "Select Output Folder";
            pFilterCol = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDialog; //cast
            pFilterCol.AddFilter(pFolderFilter, true);



            pGxDialog.Title = "Choose a directory to save raster dataset";
            if (cboFCPoint.SelectedItem != null)
            {

                pGxDialog.Name = cboFCPoint.SelectedItem.ToString();
            }

            Boolean notanythingselected;


            notanythingselected = pGxDialog.DoModalSave(this.Handle.ToInt32());
            if (notanythingselected == false) //if user hits cancel or x on the add data menu, do nothing
            {
                return;
            }
            else
            {
                if (pGxDialog.Name.Length > 13)//check for bad raster length
                {
                    MessageBox.Show("Raster names must have a length of 13 characters or less", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    
                }

                //compare the last number of characters of SelectedObject.Fullname string to pGxDialog.Name. This fixes an error where the name of the file is listed twice if you double click the file vs typing something into the box.
                if (String.Equals(Extensions.RightExtract(pGxDialog.InternalCatalog.SelectedObject.FullName, pGxDialog.Name.Length), pGxDialog.Name, System.StringComparison.OrdinalIgnoreCase))
                {
                    txtOutDistRaster.Text = pGxDialog.InternalCatalog.SelectedObject.FullName;
                }
                else
                {
                    txtOutDistRaster.Text = pGxDialog.InternalCatalog.SelectedObject.FullName + @"\" + pGxDialog.Name;
                }

            }

        }
        
    

        private void btnOpenOutVoronoi_Click(object sender, EventArgs e)
        {

            ESRI.ArcGIS.CatalogUI.IGxDialog pGxDialog;
            ESRI.ArcGIS.Catalog.IGxObjectFilter pFolderFilter;

            ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pFilterCol;
            

            pGxDialog = new GxDialogClass();
            pFolderFilter = new ESRI.ArcGIS.Catalog.GxFilterShapefiles();


            pGxDialog.Title = "Select Output Folder";
            pFilterCol = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDialog; //cast
            pFilterCol.AddFilter(pFolderFilter, true);



            pGxDialog.Title = "Choose a directory to save shapefile";
            if (cboFCPoint.SelectedItem != null)
            {

            pGxDialog.Name = cboFCPoint.SelectedItem.ToString();
            }

            Boolean notanythingselected;


            notanythingselected = pGxDialog.DoModalSave(this.Handle.ToInt32());
            if (notanythingselected == false) //if user hits cancel or x on the add data menu, do nothing
            {
                return;
            }
            else
            {
                //compare the last characters of SelectedObject.Fullname string to pGxDialog.Name. This fixes an error where the name of the file is listed twice if you double click the file vs typing something into the box.
                if (String.Equals(Extensions.RightExtract(pGxDialog.InternalCatalog.SelectedObject.FullName, pGxDialog.Name.Length), pGxDialog.Name, System.StringComparison.OrdinalIgnoreCase))
                {
                txtOutVoronoi.Text = pGxDialog.InternalCatalog.SelectedObject.FullName;
                }
                else
                {
                txtOutVoronoi.Text = pGxDialog.InternalCatalog.SelectedObject.FullName + @"\" + pGxDialog.Name;
                }


            }
        }



        private void chkWeightFieldPoint_CheckedChanged(object sender, EventArgs e)
        {
            //if USE WEIGHT FIELD is checked, populate cboWeightField with field names of the attribute table of the selected shapefile
            if (chkWeightFieldPoint.Checked)
            {
                cboWeightFieldPoint.Enabled = true;
            }
            else
            {
                cboWeightFieldPoint.Enabled = false;
            }
        }
      
        private void chkWeightFieldLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWeightFieldLine.Checked)
            {
                cboWeightFieldLine.Enabled = true;
            }
            else
            {
                cboWeightFieldLine.Enabled = false;
            }
        }

        private void chkWeightFieldPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWeightFieldPolygon.Checked)
            {
                cboWeightFieldPolygon.Enabled = true;
            }
            else
            {
                cboWeightFieldPolygon.Enabled = false;
            }
        }
           

        private void chkSpatialJoin_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void chkPointEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPointEnabled.Checked == true)
            {
                cboFCPoint.Enabled = true;
                chkWeightFieldPoint.Enabled = true;
                GetLayerInfo();
            }
            else
            {
                cboFCPoint.Enabled = false;
                chkWeightFieldPoint.Enabled = false;
                GetLayerInfo();
            }
        }

        private void chkLineEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLineEnabled.Checked == true)
            {
                cboFCLine.Enabled = true;
                chkWeightFieldLine.Enabled = true;
                GetLayerInfo();
            }
            else
            {
                cboFCLine.Enabled = false;
                chkWeightFieldLine.Enabled = false;
                GetLayerInfo();
            }
        }

        private void chkPolyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPolyEnabled.Checked == true)
            {
                cboFCPoly.Enabled = true;
                chkWeightFieldPolygon.Enabled = true;
                GetLayerInfo();
            }
            else
            {
                cboFCPoly.Enabled = false;
                chkWeightFieldPolygon.Enabled = false;
                GetLayerInfo();
            }
        }




        private void btnHelp_Click(object sender, EventArgs e)
        {
            //open a PDF document containing help documentation
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.Framework.IApplication g_pApp = ArcMap.Application; //declare an instance of status bar and progress bar
            ESRI.ArcGIS.esriSystem.IStatusBar g_psbar = g_pApp.StatusBar;
            ESRI.ArcGIS.esriSystem.IStepProgressor g_pPro = g_psbar.ProgressBar;
            btnOK.Enabled = false;
            g_pPro.Message = "Calculating Raster Values";
            g_pPro.MinRange = 0;
            g_pPro.MaxRange = 100;
            g_pPro.StepValue = 1;
            g_pPro.Show();
            int g_pct = 0;

           

            ////////////
            //First, check and make sure all input and output filepaths are populated and if the file exists. Exit runtime if any of these qualifications are not met.
            ////////////



            if (txtOutDistRaster.Text.Trim() == "")
            {

                MessageBox.Show("No output distance raster specified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }




            if (txtOutVoronoi.Text.Trim() == "")
            {
                MessageBox.Show("No output Voronoi polygon shapefile specified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

            string strOutVoronoi = txtOutVoronoi.Text.Trim();

            if (String.Equals(Extensions.RightExtract(strOutVoronoi, 4), ".shp", System.StringComparison.OrdinalIgnoreCase))
            {
                strOutVoronoi = Extensions.LeftExtract(strOutVoronoi, strOutVoronoi.Length - 4);

            }




            System.IO.FileInfo fsoDist = new System.IO.FileInfo(txtOutDistRaster.Text); //get output distance raster file info


            if (fsoDist.Exists == true)
            {
                MessageBox.Show("Output distance raster already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

            System.IO.DirectoryInfo dsoDist = new System.IO.DirectoryInfo(fsoDist.DirectoryName); //get output distance raster directory info

            if (Directory.Exists(dsoDist.FullName) == false)
            {
                MessageBox.Show("Output raster directory does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

            if (fsoDist.Name.Length > 13 == true)
            {
                MessageBox.Show("Invalid raster output file name. File name length must be 13 characters or less.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

            var findBadRasterCharacter = fsoDist.Name.IndexOfAny("()\"{}[]\\~'".ToCharArray());

            if (findBadRasterCharacter != -1)
            {
                MessageBox.Show("Invalid raster output file name. File name must not contain any of the following symbols:\n\n\t\u2022Open or close parentheses\n\t\u2022Open or close curly bracket\n\t\u2022Open or close bracket\n\t\u2022Backslash\n\t\u2022Tilde\n\t\u2022Single or double quote\n\t\u2022Comma\n\t\u2022Apostrophe\n\t\u2022Space", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }


            System.IO.FileInfo fsoVoronoi = new System.IO.FileInfo(txtOutVoronoi.Text); //check if output shapefile exists


            if (fsoVoronoi.Exists == true) //check if output shapefile already exists
            {
                MessageBox.Show("Output shapefile already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

            System.IO.DirectoryInfo dsoVoronoi = new System.IO.DirectoryInfo(fsoDist.DirectoryName); //get output Voronoi shapefile directory info

            if (Directory.Exists(dsoVoronoi.FullName) == false)
            {
                MessageBox.Show("Output Voronoi shapefile directory does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }


            if (!(cboFCPoint.SelectedIndex > -1)) //check for no value for input shapefile
            {
                MessageBox.Show("No input shapefile selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }


            //ARCGIS SPATIAL ANALYST IS NEEDED TO EXECUTE RASTER CREATION FUNCTIONS. THIS CODE BELOW CHECKS FOR A SPATIAL ANALYST LICENSE

            Type factoryType = Type.GetTypeFromProgID("esriSystem.ExtensionManager");
            IExtensionManager extensionManager = (IExtensionManager)Activator.CreateInstance
                (factoryType);

            IUID uid = new UIDClass();
            uid.Value = "esriSpatialAnalystUI.SAExtension";
            IExtension extension = extensionManager.FindExtension(uid);
            IExtensionConfig extensionConfig = (IExtensionConfig)extension;

            bool wasEnabled = (extensionConfig.State == esriExtensionState.esriESEnabled);

            if (!wasEnabled)
            {
                if (!(extensionConfig.State == esriExtensionState.esriESUnavailable))
                {
                    //Enable the license.
                    extensionConfig.State = esriExtensionState.esriESEnabled;
                }
                else
                // Handle the case when the license is not available. Provide an error message and exit to avoid running unavailable functionality.
                {
                    MessageBox.Show("Spatial Analyst license unavailable. This addin requires the ArcGIS Spatial Analyst extension.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnOK.Enabled = true;
                    return;
                }
            }

            //END CHECK FOR SPATIAL ANALYST LICENSE
         

            ////////////
            //ONCE ALL CHECKS ARE DONE AND OK, BEGIN PROCESS OF CREATING RASTER(S)
            ////////////
            

            string strOutDistRasterFile = fsoDist.Name;
            string strOutVoronoiFile = fsoVoronoi.Name;
            string strOutRasPath = dsoDist.FullName;
            string strOutVoronoiPath = dsoVoronoi.FullName;
            string g_tempFolder = CreateTempFolder(strOutRasPath);

            ESRI.ArcGIS.ArcMapUI.IMxDocument pDoc;
            pDoc = ArcMap.Document;//get current mxd and set it as pDoc
            ESRI.ArcGIS.Carto.IActiveView activeView;
            activeView = pDoc.ActiveView;
            IMap pMap = pDoc.FocusMap; //set pMap as current document



            ESRI.ArcGIS.Carto.ILayer pLayer; //create a current layer
            ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = null; //THIS FEATURE LAYER WILL EVENTUALLY BE USED TO CREATE THE DISTANCE RASTER
            //ESRI.ArcGIS.Geodatabase.IFeatureClass pFeatureClass = null;
            ESRI.ArcGIS.Carto.IEnumLayer pEnumLayer; //set up an array to hold the layers in map
            pEnumLayer = pDoc.FocusMap.Layers; //get all layers in the map

            List<ILayer> pLayerCollection = new List<ILayer>();
            
            pLayer = pEnumLayer.Next();

            while (pLayer != null) //store the layers from comboboxes in a list
            {
                if (chkPointEnabled.Checked == true && String.Compare(pLayer.Name, cboFCPoint.SelectedItem.ToString()) == 0) //set current layer to currently selected point combobox value and add it to list
                {
                    pLayerCollection.Add(pLayer);
                }
                if (chkLineEnabled.Checked == true && String.Compare(pLayer.Name, cboFCLine.SelectedItem.ToString()) == 0) //set current layer to currently selected line combobox value and add it to list
                {
                    pLayerCollection.Add(pLayer);
                }
                if (chkPolyEnabled.Checked == true && String.Compare(pLayer.Name, cboFCPoly.SelectedItem.ToString()) == 0) //set current layer to currently selected polygon combobox value and add it to list
                {
                    pLayerCollection.Add(pLayer);
                }

                pLayer = pEnumLayer.Next(); 
              
            }

        
            List<IGeoDataset> pGeodatasetCollection = new List<IGeoDataset>(); //create geodataset list to calculate extents of outputs
            List<IFeatureLayer> pFeatureLayerCollection = new List<IFeatureLayer>();
            
            foreach (ILayer layer in pLayerCollection) //convert each layer in collection to an IGeodataset
            {

                ESRI.ArcGIS.Carto.IFeatureLayer pFeatLayer = (IFeatureLayer)layer;
                if (pFeatLayer.Selectable == false) //check and make sure layer is selectable
                {
                    MessageBox.Show("One of the input feature classes does not have selectable features", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnOK.Enabled = true;
                    return;
                }

                pGeodatasetCollection.Add((IGeoDataset)pFeatLayer);
                pFeatureLayerCollection.Add(pFeatLayer);

            }

        

            //double width;
            //double height;
            double cellsize;

            double envXMin = 0;
            double envYMin = 0;
            double envXMax = 0;
            double envYMax = 0;

            ESRI.ArcGIS.SpatialAnalyst.IDistanceOp pDistanceOP; //Create raster DistanceOp object
            pDistanceOP = new RasterDistanceOpClass(); //define the newly created object as a new raster distance operation object
            ESRI.ArcGIS.GeoAnalyst.IRasterAnalysisEnvironment pEnv = null; //Create a raster analysis environment. This sets up an area to do raster operations in
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF;//create a workspace factory for raster. The workspace factory is used to create new workspaces
            ESRI.ArcGIS.DataSourcesRaster.IRasterWorkspace pTempRWS; //create a workspace for temporary raster
            ESRI.ArcGIS.DataSourcesRaster.IRasterWorkspace pFinalRWS; //create a workspace for final raster

            pWSF = new RasterWorkspaceFactoryClass(); //define the newly created workspace factory as an object

            pTempRWS = (IRasterWorkspace)pWSF.OpenFromFile(g_tempFolder, 0);
            pFinalRWS = (IRasterWorkspace)pWSF.OpenFromFile(strOutRasPath, 0);
            ESRI.ArcGIS.Geodatabase.IWorkspace pWS;
            pWS = (IWorkspace)pFinalRWS;
          
            //the three numbers below define the envelope for the output IGeoDataset. They lay out the grid and determine the cell size of it.

            ESRI.ArcGIS.Geometry.IEnvelope pExt; //create a new envelope to hold the feature class' size information
            pExt = new EnvelopeClass(); //instantiate the envelope as an object


            foreach (IGeoDataset geodataset in pGeodatasetCollection)
            {
                if (envXMin == 0)
                {
                    envXMin = geodataset.Extent.Envelope.XMin;
                }
                else
                {
                    envXMin = Math.Min(envXMin, geodataset.Extent.Envelope.XMin);
                }

                if (envYMin == 0)
                {
                    envYMin = geodataset.Extent.Envelope.YMin;
                }
                else
                {
                    envYMin = Math.Min(envYMin, geodataset.Extent.Envelope.YMin);
                }

                if (envXMax == 0)
                {
                    envXMax = geodataset.Extent.Envelope.XMax;
                }
                else
                {
                    envXMax = Math.Max(envXMax, geodataset.Extent.Envelope.XMax);
                }

                if (envYMax == 0)
                {
                    envYMax = geodataset.Extent.Envelope.YMax;
                }
                else
                {
                    envYMax = Math.Max(envYMax, geodataset.Extent.Envelope.YMax);
                }

            }
            bool containsInvalidCharacter = false; //flag for bad output extent value
            double outExtent;

            if (Double.TryParse(cboOutputExtent.Text.Trim(), out outExtent) || outExtent < 1.0) //Check if bad value is entered into CellSize textbox. 
            {

            }
            else
            {
                containsInvalidCharacter = true; //flag for bad value is set to true
            }

            if (containsInvalidCharacter) //if bad value flag is activated, exit runtime
            {
                MessageBox.Show("Invalid output extent value. Output extent should be a whole number or decimal and output extent should be 1x or greater", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }


            pExt.XMin = envXMin;
            pExt.YMin = envYMin;
            pExt.XMax = envXMax;
            pExt.YMax = envYMax;

            pExt.Expand(outExtent, outExtent, true);

            containsInvalidCharacter = false; //flag for bad cellsize value

            if (Double.TryParse(txtCellSize.Text.Trim(), out cellsize)) //Check if bad value is entered into CellSize textbox. 
            {

            }
            else
            {
                containsInvalidCharacter = true; //flag for bad value is set to true
            }

            if (containsInvalidCharacter) //if bad value flag is activated, exit runtime
            {
                MessageBox.Show("Invalid cell size value. Cell size should be a whole number or decimal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }
        

            List<IGeoDataset> pMinimumGeodatasetCollection = new List<IGeoDataset>(); //store output distance rasters
            ESRI.ArcGIS.Geodatabase.IGeoDataset pMinimumRaster = null;

            
            foreach (IFeatureLayer pFLayer in pFeatureLayerCollection)
            //Parallel.ForEach(pFeatureLayerCollection, pFLayer =>
            {

                ////////////
                //Loop through all points:
                ////////////


                bool weightFieldCheckStatus = false; //get whether that respective type has its weight field checked or not
                string weightFieldName = null;

                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint | pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
                {
                    if (chkWeightFieldPoint.Checked == true)
                    {
                        weightFieldCheckStatus = true;
                        weightFieldName = cboWeightFieldPoint.Text;
                    }

                }

                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine | pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {

                    if (chkWeightFieldLine.Checked == true)
                    {
                        weightFieldCheckStatus = true;
                        weightFieldName = cboWeightFieldLine.Text;
                    }

                }

                if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    if (chkWeightFieldPolygon.Checked == true)
                    {
                        weightFieldCheckStatus = true;
                        weightFieldName = cboWeightFieldPolygon.Text;
                    }

                }


                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSFact; //create general workspace factory (this is the directory you will be working in)
                //pWSFact = new ShapefileWorkspaceFactoryClass(); //set general workspace as a shapefile workspace. WARNING: USING THIS CAUSES A STRANGE COM REWRAPPING ERROR. IT IS AN ESRI BUG

                Type t = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE THIS LINE OR THE FOLLOWING TWO LINES. Fixes strange casting error from COM rewrapping. (ESRI BUGFIX)
                System.Object obj2 = Activator.CreateInstance(t);

                pWSFact = (ShapefileWorkspaceFactory)obj2;

                ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFeatureWorkspace; //create a feature workspace to handle features
                pFeatureWorkspace = (IFeatureWorkspace)pWSFact.OpenFromFile(g_tempFolder, 0); //set feature workspace as the shapefile workspace defined earlier, and specify a file location

                ESRI.ArcGIS.Geodatabase.IFeatureCursor pFCursor1; //use to loop through the feature class. You can think of this as a marker that grabs each feature as it loops through the array of features
                pFCursor1 = pFLayer.Search(null, false);
                int weightFldIndex = 0; //used to find the location of weight column for weighted voronoi calculation


                long recNum; //store the number of records
                recNum = pFLayer.FeatureClass.FeatureCount(null); //set counter at 0

                if (recNum == 0) //check for no features in feature class
                {
                    MessageBox.Show("Cannot create diagram. Layer has no features.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnOK.Enabled = true;
                    return;
                }
                if (recNum == 1) //check for only one feature in feature class
                {
                    MessageBox.Show("Cannot create diagram. Layer has only one feature.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnOK.Enabled = true;
                    return;
                }

                ESRI.ArcGIS.Geodatabase.IFeature pFeature1; //declare an instance of feature to store the feature itself 
                pFeature1 = pFCursor1.NextFeature(); //get the feature from the marker as it loops through the array


                if (pFeature1 == null) //check if first feature is null
                {
                    MessageBox.Show("Error: First feature is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnOK.Enabled = true;
                    return;
                }

                ////////////
                //CODE TO SET FACTORS FOR WEIGHTED VORONOI GOES HERE
                ////////////

                double minWeight = 0;
                double maxWeight = 0;
                double minmaxrange = 0;


                if (weightFieldCheckStatus == true)
                {
                    //IF WEIGHT FIELD IS CHECKED,
                    //GET THE MAX AND MIN VALUES USED TO CREATE WEIGHTS
                    string strWeightFieldName = weightFieldName;

                    var maxminpair = GetMaxMinFromNumericalField(pFLayer.FeatureClass, strWeightFieldName); //store the min/max of weight field in a key/value pair

                    minWeight = maxminpair.Key;
                    maxWeight = maxminpair.Value;
                    minmaxrange = maxWeight - minWeight;

                    weightFldIndex = pFCursor1.FindField(weightFieldName); //set the location of weight field to be used by searching for it in attribute table

                    if ((minWeight < 0) | (maxWeight < 0)) //check for negative values in the weight field
                    {
                        MessageBox.Show("All weight values must be non-negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnOK.Enabled = true;
                        return;
                    }


                    if (minWeight == maxWeight) //if max and min are equal, no need for MWVD calculations. Do OVD instead.
                    {

                        weightFieldCheckStatus = false;
                    }
                }





                //***WRITE A METHOD HERE THAT TAKES AS ITS INPUT 1 pTempGeodataset AND OUTPUTS A FINAL WEIGHTED pMinimumRaster*** 
                //***GET 1-3 SEPERATE pTempGeodatasets AND STORE THEM IN AN IENUMERABLE. USE PARALELL.FOREACH ON IENUMERABLE OF 1-3 pTempGeodatasets TO EXECUTE THE MWVD CALCULATIONS IN PARALLEL***

                ////////////
                //CODE TO SET FACTORS FOR WEIGHTED VORONOI ENDS HERE
                ////////////


                ESRI.ArcGIS.Geodatabase.IGeoDataset pDistRaster_0;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pDistRaster_00;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pDistRaster_1;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pDistRaster_11;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pTempMinRaster;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pWeightRaster;

                ESRI.ArcGIS.Geodatabase.IGeoDataset pTempGeodataset;
                ESRI.ArcGIS.Carto.IFeatureLayer pTempFeatureLayer;

                ////////////
                //Loop through all features.
                ////////////

                ESRI.ArcGIS.GeoAnalyst.IConversionOp pConvOp;
                pConvOp = new RasterConversionOpClass();

                ESRI.ArcGIS.Carto.IFeatureSelection pFeatureSelection;
                pFeatureSelection = (IFeatureSelection)pFLayer; // ***IMPORTANT*** THIS SETS THE CURRENT FEATURE LAYER IN COMBOBOX TO THE FEATURE SELECTION TO BE USED FOR CONVERSION TO RASTER 
                ESRI.ArcGIS.Geodatabase.ISelectionSet pSelectionSet;
                pSelectionSet = pFeatureSelection.SelectionSet; //Defines a selection set of features. The pFeatureLayer information is stored here.

                ESRI.ArcGIS.Geodatabase.IFeatureCursor pFCursor2;
                ESRI.ArcGIS.Geodatabase.IFeature pFeature2;
                ESRI.ArcGIS.Geodatabase.IQueryFilter pQueryFilter;

                pQueryFilter = new QueryFilterClass();
                pFCursor2 = pFLayer.Search(null, false);
                pSelectionSet = pFeatureSelection.SelectionSet;
                pFeature2 = pFCursor2.NextFeature();

                pEnv = (IRasterAnalysisEnvironment)pDistanceOP;
                pEnv.OutWorkspace = (IWorkspace)pTempRWS;
                pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, cellsize);
                pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, pExt);

                if (weightFieldCheckStatus == true)//create MWVD if weight field checked
                {
                    int n = 0; //counter
                    double varWeight;
                    while (!(pFeature2 == null)) //loop through each feature in the input feature class
                    {
                        ////////////
                        //Select the feature
                        ////////////

                        pFeatureSelection.Clear();
                        pSelectionSet = pFeatureSelection.SelectionSet;
                        pSelectionSet.Add(pFeature2.OID);
                        pFeatureSelection.SelectionSet = pSelectionSet;

                        ////////////
                        //Convert the feature to a featurelayer and get pTempGeodataset for the feature layer
                        ////////////

                        pTempFeatureLayer = Selection2FeatureLayer(pFLayer, g_tempFolder, "tmpShp" + n.ToString());
                        pTempGeodataset = (IGeoDataset)pTempFeatureLayer.FeatureClass;

                        ////////////
                        //Use math operation to get local minimum
                        ////////////

                        ESRI.ArcGIS.SpatialAnalyst.IMathOp pMathOp = new RasterMathOpsClass();
                        ESRI.ArcGIS.GeoAnalyst.IRasterMakerOp pRasterMakerOp = new RasterMakerOpClass();

                        varWeight = Convert.ToDouble(pFeature2.get_Value(weightFldIndex)); //get the value of the feature's row in weight field
                        if (varWeight == 0)
                        {
                            varWeight = 0.0001;
                        }
                        else
                        {
                            varWeight = (varWeight / maxWeight);
                        }

                        if (n == 0)
                        {
                            pDistRaster_0 = pDistanceOP.EucDistanceFull(pTempGeodataset, true, false, false); //get regular distance raster of point
                            pWeightRaster = pRasterMakerOp.MakeConstant(varWeight, false); //create constant weight raster from weight number
                            pDistRaster_00 = pMathOp.Divide(pDistRaster_0, pWeightRaster); //divide regular distance raster by weight field
                            pMinimumRaster = pDistRaster_00; //store final output raster into pMinimumRaster
                        }
                        if (n > 0)
                        {
                            pDistRaster_1 = pDistanceOP.EucDistanceFull(pTempGeodataset, true, false, false); //get regular distance raster
                            pWeightRaster = pRasterMakerOp.MakeConstant(varWeight, false); //create constant weight raster from weight number
                            pDistRaster_11 = pMathOp.Divide(pDistRaster_1, pWeightRaster); //divide regular distance raster by weight field
                            pTempMinRaster = LocalMinimum2Rasters(pMinimumRaster, pDistRaster_11, g_tempFolder); //compare current raster with new raster and getminimum
                            pMinimumRaster = pTempMinRaster; //store the new minimum raster into pminimumraster
                        }

                        DeleteFeatureDataset(g_tempFolder, "tmpShp" + n.ToString());
                        n = n + 1;
                        g_pct = Convert.ToInt32(n / recNum * 100);
                        g_pPro.Position = g_pct;

                        pFeature2 = pFCursor2.NextFeature();


                    }


                }


                else
                {
                    MessageBox.Show("Marker1");
                    CreateOVDistanceRaster(pFLayer.FeatureClass, pDistanceOP, out pMinimumRaster);
                    MessageBox.Show("Marker2");
                }
                pMinimumGeodatasetCollection.Add(pMinimumRaster);
                pFeatureSelection.Clear();

            }//);
      
            //pTempGeodataset = (IGeoDataset)pFeatureLayer.FeatureClass;
            //pMinimumRaster = pDistanceOP.EucDistanceFull(pTempGeodataset, true, false, false);

            if (pMinimumGeodatasetCollection.Count == 1) //if just one input feature class
            {
                pMinimumRaster = pMinimumGeodatasetCollection[0];
                pFeatureLayer = pFeatureLayerCollection[0];
        
            }
            
            else if (pMinimumGeodatasetCollection.Count == 2) //if two input feature classes, get local minimum
            {
                pMinimumRaster = LocalMinimum2Rasters(pMinimumGeodatasetCollection[0], pMinimumGeodatasetCollection[1], g_tempFolder);
              
            }

            else if (pMinimumGeodatasetCollection.Count == 3) //if three input feature classes, get local minimum
            {
                pMinimumRaster = LocalMinimum3Rasters(pMinimumGeodatasetCollection[0], pMinimumGeodatasetCollection[1], pMinimumGeodatasetCollection[2], g_tempFolder);  
            }

            
            g_pPro.Position = 0;
            g_pPro.Hide();

            pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, cellsize);
            pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, pExt);

      

            ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRasBandCol; //this code block takes an IGeoDataset and converts it to a raster dataset
            pRasBandCol = (IRasterBandCollection)pMinimumRaster;
            ESRI.ArcGIS.DataSourcesRaster.IRasterBand pRasBand;
            pRasBand = pRasBandCol.Item(0);
            ESRI.ArcGIS.Geodatabase.IRasterDataset pRasDataset;
            pRasDataset = pRasBand.RasterDataset;


            if (pRasDataset.CanCopy())
            {
                pRasDataset.Copy(strOutDistRasterFile, pWS);

            }
            else
            {
                MessageBox.Show("Unable to copy raster dataset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

       
            ////////////
            //CREATE FLOW DIRECTION AND BASIN RASTERS WHICH WILL BE USED TO GENERATE POLYGON SHAPEFILE FROM DISTANCE RASTER
            ////////////

            ESRI.ArcGIS.SpatialAnalyst.IHydrologyOp pHydrologyOp;
            pHydrologyOp = new RasterHydrologyOpClass();

            ESRI.ArcGIS.Geodatabase.IGeoDataset pFlowDirRaster;
            ESRI.ArcGIS.Geodatabase.IGeoDataset pBasinRaster;

            g_pPro.Message = "Generating Voronoi Diagram ...";
            pFlowDirRaster = pHydrologyOp.FlowDirection(pMinimumRaster, false, false); //CREATE FLOW DIRECTION RASTER

            pBasinRaster = pHydrologyOp.Basin(pFlowDirRaster); //CREATE BASIN RASTER

  

            ////////////
            //RASTER TO POLYGON OPERATIONS
            ////////////

            ESRI.ArcGIS.GeoAnalyst.IConversionOp pConversionOp;
            pConversionOp = new RasterConversionOpClass();
            ESRI.ArcGIS.Geodatabase.IGeoDataset pVoronoiFClassOut;
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pSWSF;

            Type t1 = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
            System.Object obj3 = Activator.CreateInstance(t1);

            pSWSF = (ShapefileWorkspaceFactory)obj3;


            ESRI.ArcGIS.Geodatabase.IWorkspace pTempWSP;
            pTempWSP = pSWSF.OpenFromFile(g_tempFolder, 0);


            ESRI.ArcGIS.Geodatabase.IWorkspace pFinalWSP;
            pFinalWSP = pSWSF.OpenFromFile(strOutVoronoiPath, 0);



            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pTempFWS;
            pTempFWS = (IFeatureWorkspace)pSWSF.OpenFromFile(g_tempFolder, 0);

            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pTempFWS2;
            pTempFWS2 = (IFeatureWorkspace)pSWSF.OpenFromFile(g_tempFolder, 0);

            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFinalFWS;
            pFinalFWS = (IFeatureWorkspace)pSWSF.OpenFromFile(strOutVoronoiPath, 0);





                g_pPro.Message = "Generating Voronoi Shapefile...";
                ESRI.ArcGIS.Carto.IFeatureLayer pVoronoiLayer;
                pVoronoiLayer = new FeatureLayerClass();
                ESRI.ArcGIS.Carto.IFeatureLayer pJoinedVoronoiLayer;
                pJoinedVoronoiLayer = new FeatureLayerClass();
                ESRI.ArcGIS.Carto.IFeatureLayer pTransferVoronoiLayer;
                pTransferVoronoiLayer = new FeatureLayerClass();
  
            if (chkSpatialJoin.Checked == true)
                {
                    pVoronoiFClassOut = pConversionOp.RasterDataToPolygonFeatureData(pBasinRaster, pTempWSP, "temp" + strOutVoronoiFile, true); //CREATE TEMPORARY POLYGON HERE FROM BASIN RASTER TO BE SPATIALLY JOINED
                    ESRI.ArcGIS.Geodatabase.IFeatureClass newVoroFClass;
                    pVoronoiLayer.FeatureClass = pTempFWS.OpenFeatureClass("temp" + strOutVoronoiFile);
                    newVoroFClass = FeatureToPolygonSpatialJoin(pFeatureLayer, pVoronoiLayer, strOutVoronoiPath, strOutVoronoiFile);
                    pTransferVoronoiLayer.FeatureClass = pTempFWS2.OpenFeatureClass(strOutVoronoiFile);
                    ConvertFeatureClass(pTransferVoronoiLayer.FeatureClass, pFinalWSP, strOutVoronoiFile);
                    pJoinedVoronoiLayer.FeatureClass = pFinalFWS.OpenFeatureClass(strOutVoronoiFile); //set the layer to get the newly spatially joined shapefile

                }
                else
                {
                    pVoronoiFClassOut = pConversionOp.RasterDataToPolygonFeatureData(pBasinRaster, pFinalWSP, strOutVoronoiFile, true);
                    pVoronoiLayer.FeatureClass = pFinalFWS.OpenFeatureClass(strOutVoronoiFile);

                }


                g_pPro.Hide();

                //once the raster and shapefile have been created, if ADD OUTPUT TO ARCMAP is checked, add the layers to ArcMap through Arcobjects.


                if (chkAddDistRaster.Checked == true)
                {

                    ESRI.ArcGIS.Geodatabase.IRasterDataset pRasFinalDataset;
                    pRasFinalDataset = OpenFileRasterDataset(strOutRasPath, strOutDistRasterFile);
                    AddRasterLayer(pMap, pRasFinalDataset);
                }

                if (chkAddVoronoi.Checked == true)
                {
                    if (chkSpatialJoin.Checked == true)
                    {
                        pJoinedVoronoiLayer.Name = Extensions.LeftExtract(strOutVoronoiFile, strOutVoronoiFile.Length - 4);
                        pMap.AddLayer(pJoinedVoronoiLayer);
                    }
                    else
                    {
                        pVoronoiLayer.Name = Extensions.LeftExtract(strOutVoronoiFile, strOutVoronoiFile.Length - 4);
                        pMap.AddLayer(pVoronoiLayer);
                    }
                }
            
        }


        ////////////
        //FORM CODING ENDS HERE. IMPLEMENTED METHODS AND EXTENSIONS BEYOND THIS POINT
        ////////////

        public IGeoDataset CreateOVDistanceRaster(IFeatureClass InputFeatureClass, IDistanceOp DistanceOP, out IGeoDataset OVDistanceRaster) //Generates OVDistanceRaster from which to create OVD polygons
        {

            ESRI.ArcGIS.Geodatabase.IGeoDataset temporaryGeodataset = (IGeoDataset)InputFeatureClass;
            return OVDistanceRaster = DistanceOP.EucDistanceFull(temporaryGeodataset, true, false, false);
        }

        public KeyValuePair<double,double> GetMaxMinFromNumericalField(IFeatureClass featureClass, string fieldname)
        {
            ICursor cursor = (ICursor)featureClass.Search(null, false);

            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = fieldname;
            dataStatistics.Cursor = cursor;

            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            enumerator.Reset();

            while (enumerator.MoveNext())
            {
                object myObject = enumerator.Current;
         
            }

            cursor = (ICursor)featureClass.Search(null, false);
            dataStatistics.Cursor = cursor;
            ESRI.ArcGIS.esriSystem.IStatisticsResults statisticsResults = dataStatistics.Statistics;
            double min = statisticsResults.Minimum;
            double max = statisticsResults.Maximum;

            return new KeyValuePair<double, double>(min, max); //In C#, use KeyValuePair to return two values from a method.
       
        }

        public IGeoDataset LocalMinimum2Rasters(IGeoDataset pRaster1, IGeoDataset pRaster2, string strOutPath)
        {
            
            ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRBCollTmp;
            ESRI.ArcGIS.DataSourcesRaster.IRasterBand pRBand;

            ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRBColl;
            pRBColl = new RasterClass();
            pRBCollTmp = (IRasterBandCollection)pRaster1;
            pRBand = pRBCollTmp.Item(0);
            pRBColl.AppendBand(pRBand);
            pRBCollTmp = (IRasterBandCollection)pRaster2;
            pRBand = pRBCollTmp.Item(0);
            pRBColl.AppendBand(pRBand);
            
            //Create a RasterLocalOp operator
            ESRI.ArcGIS.SpatialAnalyst.ILocalOp pLocalOp = new RasterLocalOpClass();

            ESRI.ArcGIS.Geodatabase.IWorkspace pWs;
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF = new RasterWorkspaceFactoryClass();
            pWs = pWSF.OpenFromFile(strOutPath, 0);

            return pLocalOp.LocalStatistics((IGeoDataset)pRBColl, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMinimum);

        }

        public IGeoDataset LocalMinimum3Rasters(IGeoDataset pRaster1, IGeoDataset pRaster2, IGeoDataset pRaster3, string strOutPath)
        {

            ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRBCollTmp;
            ESRI.ArcGIS.DataSourcesRaster.IRasterBand pRBand;

            ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRBColl;
            pRBColl = new RasterClass();
            pRBCollTmp = (IRasterBandCollection)pRaster1;
            pRBand = pRBCollTmp.Item(0);
            pRBColl.AppendBand(pRBand);
            pRBCollTmp = (IRasterBandCollection)pRaster2;
            pRBand = pRBCollTmp.Item(0);
            pRBColl.AppendBand(pRBand);
            pRBCollTmp = (IRasterBandCollection)pRaster3;
            pRBand = pRBCollTmp.Item(0);
            pRBColl.AppendBand(pRBand);

            //Create a RasterLocalOp operator
            ESRI.ArcGIS.SpatialAnalyst.ILocalOp pLocalOp = new RasterLocalOpClass();

            ESRI.ArcGIS.Geodatabase.IWorkspace pWs;
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF = new RasterWorkspaceFactoryClass();
            pWs = pWSF.OpenFromFile(strOutPath, 0);

            return pLocalOp.LocalStatistics((IGeoDataset)pRBColl, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMinimum);

        }

        public IFeatureClass CreateNewShapefile(ISpatialReference pSpatialReference, IGeometryDef OutputGeomDef, string strOutFolder, string strOutName)
        {
            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFWS;
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWorkspaceFactory;

            Type t = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
            System.Object obj = Activator.CreateInstance(t);
            pWorkspaceFactory = (ShapefileWorkspaceFactory)obj;
            pFWS = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(strOutFolder, 0);

            
            ESRI.ArcGIS.Geodatabase.IFieldsEdit pFields;
            ESRI.ArcGIS.Geodatabase.IFieldEdit pField;
            ESRI.ArcGIS.Geodatabase.IGeometryDef pGeomDef;
            ESRI.ArcGIS.Geodatabase.IGeometryDefEdit pGeomDefEdit;
    
            pField = new FieldClass();
            pField.Name_2 = "Shape";
            pField.Type_2 = esriFieldType.esriFieldTypeGeometry; //define the geometry of the shape field
            pGeomDef = OutputGeomDef;
            pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
            pGeomDefEdit.SpatialReference_2 = pSpatialReference;
            pField.GeometryDef_2 = pGeomDef;
            pFields = new FieldsClass();
            pFields.AddField(pField);
            
            //create the new feature class and return it
            ESRI.ArcGIS.Geodatabase.IFeatureClass pFeatClass = pFWS.CreateFeatureClass(strOutName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            return pFeatClass;
                  
        }

        public IFeatureLayer Selection2FeatureLayer(IFeatureLayer pInputFeatLayer, string strOutFolder, string strOutName)
        {
            ESRI.ArcGIS.Geodatabase.IGeoDataset pGeoDataset;
            ESRI.ArcGIS.Geometry.ISpatialReference pSpatialReference;
            ESRI.ArcGIS.Geodatabase.IFeatureClass pOutputFeatClass;
            ESRI.ArcGIS.Geodatabase.IFeatureClass pInputFeatureClass;

            pGeoDataset = (IGeoDataset)pInputFeatLayer;
            pInputFeatureClass = pInputFeatLayer.FeatureClass;
            pSpatialReference = pGeoDataset.SpatialReference;

            //Call the CreateNewShapefile function to create the output feature class
            ESRI.ArcGIS.Geodatabase.IGeometryDef OutputGeomDef;
            ESRI.ArcGIS.Geodatabase.IFields pFields;
            ESRI.ArcGIS.Geodatabase.IField aField;
            pFields = pInputFeatureClass.Fields;
            int i = pFields.FindField("Shape");
            aField = pFields.get_Field(i);
            OutputGeomDef = aField.GeometryDef;

            ESRI.ArcGIS.Geodatabase.ICursor pFCursor;
            ESRI.ArcGIS.Geodatabase.IFeature pInputFeature;
            ESRI.ArcGIS.Geodatabase.IFeature pOutputFeature;
            ESRI.ArcGIS.Geodatabase.ISelectionSet pSelSet;
            ESRI.ArcGIS.Carto.IFeatureSelection pFeatureSelection;

            pFeatureSelection = (IFeatureSelection)pInputFeatLayer;
            pSelSet = pFeatureSelection.SelectionSet;
            pSelSet.Search(null, false, out pFCursor);

            pOutputFeatClass = CreateNewShapefile(pSpatialReference, OutputGeomDef, strOutFolder, strOutName); //create shapefile
            pInputFeature = (IFeature)pFCursor.NextRow();

            while (!(pInputFeature == null))
            {
                pOutputFeature = pOutputFeatClass.CreateFeature();
                pOutputFeature.Shape = pInputFeature.Shape;
                pOutputFeature.Store();
                pInputFeature = (IFeature)pFCursor.NextRow();
            }

            ESRI.ArcGIS.Carto.IFeatureLayer pOutFeatLayer = new FeatureLayerClass();
            pOutFeatLayer.FeatureClass = pOutputFeatClass;
            return pOutFeatLayer;


        }

        public void DeleteFeatureDataset(string strPath, string strFileName)
        {
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF;
            Type t = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
            System.Object obj = Activator.CreateInstance(t);
            pWSF = (ShapefileWorkspaceFactory)obj;

            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFWS = (IFeatureWorkspace)pWSF.OpenFromFile(strPath, 0);
            ESRI.ArcGIS.Geodatabase.IDataset pDs = (IDataset)pFWS.OpenFeatureClass(strFileName);

            try
            {
                if (!(pDs == null))
                {
                    pDs.Delete();
                }
                pWSF = null;
                pFWS = null;
                pDs = null;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }          

        }
    

        public void AddtoShapefileCombobox(ESRI.ArcGIS.Carto.IMap map, System.String layerCLSID)
        //loops through all feature classes in .mxd and populates the values into the Add Shapefile Comboboxes
        {
            cboFCPoint.Items.Clear();
            cboFCLine.Items.Clear();
            cboFCPoly.Items.Clear();

            if (map == null || layerCLSID == null)
            {
                return;
            }

           
            ESRI.ArcGIS.esriSystem.IUID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = layerCLSID; // Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
            try
            {
                ESRI.ArcGIS.Carto.IEnumLayer enumLayer = map.get_Layers(((ESRI.ArcGIS.esriSystem.UID)(uid)), true); // Explicit Cast 
                enumLayer.Reset();
                ESRI.ArcGIS.Carto.ILayer layer = enumLayer.Next();
                while (!(layer == null))
                {
                    ESRI.ArcGIS.Carto.IFeatureLayer pFLayer = (IFeatureLayer)layer;
               
                    if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint | pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint) //if point, add to point combobox
                    {
                        chkPointEnabled.Checked = true;
                        cboFCPoint.Items.Add(layer.Name);
                        cboFCPoint.SelectedIndex = cboFCPoint.FindStringExact(layer.Name);
                    }

                    if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine | pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline) //if line, add to line combobox
                    {
                        chkLineEnabled.Checked = true;
                        cboFCLine.Items.Add(layer.Name);
                        cboFCLine.SelectedIndex = cboFCLine.FindStringExact(layer.Name);
                    }

                    if (pFLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon) //if polygon, add to polygon combobox
                    {
                        chkPolyEnabled.Checked = true;
                        cboFCPoly.Items.Add(layer.Name);
                        cboFCPoly.SelectedIndex = cboFCPoly.FindStringExact(layer.Name);
                    }

                    layer = enumLayer.Next();

                
                }
            }
            catch
            {
            }

           
        }

        
        public void GetLayerInfo() //Sets current layer to given input string argument and populates several areas of form
        {

            ESRI.ArcGIS.ArcMapUI.IMxDocument pDoc;
            pDoc = ArcMap.Document;//get current mxd and set it as pDoc
            ESRI.ArcGIS.Carto.IActiveView activeView;
            activeView = pDoc.ActiveView;

          

            IMap pMap = pDoc.FocusMap; //set pMap as current document

            List<double> whvalues = new List<double>(); //stores width and height values of input datasets to calculate cellsize 
            List<double> distances = new List<double>(); //stores nearest distance to feature for use in calculating cellsize


            ESRI.ArcGIS.Carto.IEnumLayer pEnumLayer; //get all layers in the map
            pEnumLayer = pDoc.FocusMap.Layers;
            

            ESRI.ArcGIS.Carto.ILayer pLayer; //create a layer to store looped values of point combobox layers
            pLayer = pEnumLayer.Next();

            int layercounter = 0;
            ESRI.ArcGIS.Geometry.IGeometryBag pGeometryBag = new GeometryBagClass();
            ESRI.ArcGIS.Geometry.IGeometryCollection pGeometryCollection = (IGeometryCollection)pGeometryBag;

         
            while (pLayer != null)
            {
                if (chkPointEnabled.Checked == true && String.Compare(pLayer.Name, cboFCPoint.Text) == 0) //set current layer to currently selected combobox value 
                {
                   
                    ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer; //cast to inherited class. 
                    
                    ESRI.ArcGIS.Geodatabase.IFeatureClass fClass = pFeatureLayer.FeatureClass; //get the featureclass information from feature layer
                    
                    ESRI.ArcGIS.Geodatabase.IFields pFields = fClass.Fields; //create an array of all the fields of the attribute table of the feature class
                   

                    try
                    {
                        cboWeightFieldPoint.Items.Clear(); //avoid null exception
                    }

                    catch
                    {
                    }

                    for (int n = 0; n <= fClass.Fields.FieldCount - 1; n++) //loop through selected feature class' attribute table fields
                    {

                        ESRI.ArcGIS.Geodatabase.IField pField; //instantiate a variable to be checked for numerical field
                        pField = pFields.get_Field(n); //store a value in pfield to check for numerical field

                        if (pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSmallInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSingle | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeDouble)
                        {
                            cboWeightFieldPoint.Items.Add(pField.Name); //if numerical field, add to weight field combobox
                        }
                    }
                   

                    try
                    {
                        cboWeightFieldPoint.SelectedIndex = 0; //avoid null exception if there are no valid weightable fields
                    }
                    catch
                    {
                    }

                    whvalues.Add(pLayer.AreaOfInterest.Width);
                    whvalues.Add(pLayer.AreaOfInterest.Height);
                    layercounter = layercounter + 1;
                    
                    ITable pTable = (ITable)pFeatureLayer.FeatureClass;
                    List<int> OIDList = new List<int>();
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.SubFields = pFeatureLayer.FeatureClass.OIDFieldName;
                    ICursor pCursor = pTable.Search(queryFilter, false);
                    IRow pRow = null;
                    
                    while ((pRow = pCursor.NextRow()) != null)
                    {
                        OIDList.Add(pRow.OID);
                    }

                    IFeatureCursor blocksCursor = pFeatureLayer.FeatureClass.GetFeatures(OIDList.ToArray(), false);
                    IFeature blockFeature = null;
                    
                    while ((blockFeature = blocksCursor.NextFeature()) != null)
                    {
                        pGeometryCollection.AddGeometry(blockFeature.Shape);
                    }
                    
                   

                }

                if (chkLineEnabled.Checked == true && String.Compare(pLayer.Name, cboFCLine.Text) == 0) //set current layer to currently selected combobox value
                {
                    ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer; //cast to inherited class. 

                    ESRI.ArcGIS.Geodatabase.IFeatureClass fClass = pFeatureLayer.FeatureClass; //get the featureclass information from feature layer
                    ESRI.ArcGIS.Geodatabase.IFields pFields = fClass.Fields; //create an array of all the fields of the attribute table of the feature class

                    try
                    {
                        cboWeightFieldLine.Items.Clear(); //avoid null exception
                    }

                    catch
                    {
                    }



                    for (int n = 0; n <= fClass.Fields.FieldCount - 1; n++) //loop through selected feature class' attribute table fields
                    {

                        ESRI.ArcGIS.Geodatabase.IField pField; //instantiate a variable to be checked for numerical field
                        pField = pFields.get_Field(n); //store a value in pfield to check for numerical field

                        if (pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSmallInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSingle | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeDouble)
                        {
                            cboWeightFieldLine.Items.Add(pField.Name); //if numerical field, add to weight field combobox
                        }
                    }

                    try
                    {
                        cboWeightFieldLine.SelectedIndex = 0; //avoid null exception if there are no valid weightable fields
                    }
                    catch
                    {
                    }

                    whvalues.Add(pLayer.AreaOfInterest.Width);
                    whvalues.Add(pLayer.AreaOfInterest.Height);
                    layercounter = layercounter + 1;

                    ITable pTable = (ITable)pFeatureLayer.FeatureClass;
                    List<int> OIDList = new List<int>();
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.SubFields = pFeatureLayer.FeatureClass.OIDFieldName;
                    ICursor pCursor = pTable.Search(queryFilter, false);
                    IRow pRow = null;
                    while ((pRow = pCursor.NextRow()) != null)
                    {
                        OIDList.Add(pRow.OID);
                    }

                    IFeatureCursor blocksCursor = pFeatureLayer.FeatureClass.GetFeatures(OIDList.ToArray(), false);
                    IFeature blockFeature = null;
                    while ((blockFeature = blocksCursor.NextFeature()) != null)
                    {
                        pGeometryCollection.AddGeometry(blockFeature.Shape);
                    }

                }
                if (chkPolyEnabled.Checked == true && String.Compare(pLayer.Name, cboFCPoly.Text) == 0) //set current layer to currently selected combobox value 
                {
                    ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer; //cast to inherited class. 

                    ESRI.ArcGIS.Geodatabase.IFeatureClass fClass = pFeatureLayer.FeatureClass; //get the featureclass information from feature layer
                    ESRI.ArcGIS.Geodatabase.IFields pFields = fClass.Fields; //create an array of all the fields of the attribute table of the feature class

                    try
                    {
                        cboWeightFieldPolygon.Items.Clear(); //avoid null exception
                    }

                    catch
                    {
                    }

                    for (int n = 0; n <= fClass.Fields.FieldCount - 1; n++) //loop through selected feature class' attribute table fields
                    {

                        ESRI.ArcGIS.Geodatabase.IField pField; //instantiate a variable to be checked for numerical field
                        pField = pFields.get_Field(n); //store a value in pfield to check for numerical field

                        if (pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSmallInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeInteger | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeSingle | pField.Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeDouble)
                        {
                            cboWeightFieldPolygon.Items.Add(pField.Name); //if numerical field, add to weight field combobox
                        }
                    }

                    try
                    {
                        cboWeightFieldPolygon.SelectedIndex = 0; //avoid null exception if there are no valid weightable fields
                    }
                    catch
                    {
                    }

                    whvalues.Add(pLayer.AreaOfInterest.Width);
                    whvalues.Add(pLayer.AreaOfInterest.Height);
                    layercounter = layercounter + 1;

                    ITable pTable = (ITable)pFeatureLayer.FeatureClass;
                    List<int> OIDList = new List<int>();
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.SubFields = pFeatureLayer.FeatureClass.OIDFieldName;
                    ICursor pCursor = pTable.Search(queryFilter, false);
                    IRow pRow = null;
                    while ((pRow = pCursor.NextRow()) != null)
                    {
                        OIDList.Add(pRow.OID);
                    }

                    IFeatureCursor blocksCursor = pFeatureLayer.FeatureClass.GetFeatures(OIDList.ToArray(), false);
                    IFeature blockFeature = null;
                    while ((blockFeature = blocksCursor.NextFeature()) != null)
                    {
                        pGeometryCollection.AddGeometry(blockFeature.Shape);
                    }

                }
                pLayer = pEnumLayer.Next();

            }

            ISpatialIndex spatialIndex = (ISpatialIndex)pGeometryBag;
            spatialIndex.AllowIndexing = true;
            spatialIndex.Invalidate();
           
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pGeometryBag;
           
            ESRI.ArcGIS.Geometry.IEnumGeometry pEnumGeometry = (IEnumGeometry)pGeometryBag;
            pEnumGeometry.Reset();
            ESRI.ArcGIS.Geometry.IGeometry pGeometry = pEnumGeometry.Next();


            while (pGeometry != null)
            {
                IProximityOperator pProximity = (IProximityOperator)pGeometry;

                ESRI.ArcGIS.Geometry.IEnumGeometry pEnumGeomBag = (IEnumGeometry)pGeometryBag;
                ESRI.ArcGIS.Geometry.IGeometry pGeomBagIterable = pEnumGeomBag.Next();

                while (pGeomBagIterable != null)
                {
                    double distance = pProximity.ReturnDistance(pGeomBagIterable);
                    if (distance != 0)
                    {
                        distances.Add(distance);
                    }
                    pGeomBagIterable = pEnumGeomBag.Next();
                }
                pGeometry = pEnumGeometry.Next();
            }
                




            if (layercounter > 1)
            {
                chkSpatialJoin.Checked = false;
                chkSpatialJoin.Enabled = false;
            }

            else
            {
                chkSpatialJoin.Enabled = true;
            }

            try
            {

                double minimumdistbetweenfeatures = distances.Min()/2;
                double widthheight250 = whvalues.Min() / 250.0;

                if (widthheight250 > minimumdistbetweenfeatures)
                {
                    
 
                    txtCellSize.Text = minimumdistbetweenfeatures.ToString();
                }
                else
                {
 
                    txtCellSize.Text = widthheight250.ToString();
                }
            }

            catch
            {
            }

            //txtCellSize.Text = (whvalues.Min() / 250.0).ToString(); //get minimum of all widths and heights and divide it by 250 to create the cellsize value
         
            
            /*
            try //this will attempt to display the units of the spatial refererence next to the cell size box, if the units are accessible.
            {

                ESRI.ArcGIS.Geometry.IProjectedCoordinateSystem lyrCoordSystem = (IProjectedCoordinateSystem)pLayer.AreaOfInterest.SpatialReference; //cast to inherited class

                lblSpatialReference.Text = lyrCoordSystem.CoordinateUnit.Name;

            }
            catch
            {
            }
            */

        } 

        void AddRasterLayer(IMap map, IRasterDataset rasterDataset)
        {
            // rasterDataset represents a RasterDataset from raster workspace, access workspace or sde workspace.
            // map represents the Map to add the layer to once it is created

            // Create a raster layer. Use CreateFromRaster method when creating from a Raster.
            IRasterLayer rasterLayer = new RasterLayerClass();
            rasterLayer.CreateFromDataset(rasterDataset);

            // Add the raster layer to Map 
            map.AddLayer(rasterLayer);

            // QI for availabilty of the IActiveView interface for a screen update
            IActiveView activeView = map as IActiveView;

            if (activeView != null)
                activeView.Refresh();
        }

        public void AddFeatureClassToMap(string typeOfFeatureClass)
        {
            ESRI.ArcGIS.Catalog.IGxObject gxObj = null; //declare an instance of GxObjects
            Boolean notanythingselected;
            ESRI.ArcGIS.CatalogUI.IGxDialog pGxDia = null; //declare an instance of GxDialogs
            if (typeOfFeatureClass == "Point")
            {
                ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pGxFilter; //establish a collection of filters 
                ESRI.ArcGIS.Catalog.GxFilterPointFeatureClasses pfilter2; //create a filter for point files   
                pfilter2 = new ESRI.ArcGIS.Catalog.GxFilterPointFeatureClasses(); //store a filter for point feature classes in pfilter2
                pGxDia = new GxDialogClass(); //new dialog box object that shows only shapefiles
                pGxDia.Title = "Choose a Point Feature Class";
                pGxFilter = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDia;
                pGxFilter.AddFilter(pfilter2, true);
            }

            else if (typeOfFeatureClass == "Line")
            {
                ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pGxFilter; //establish a collection of filters 
                ESRI.ArcGIS.Catalog.GxFilterPolylineFeatureClasses pfilter2; //create a filter for point files
                pfilter2 = new ESRI.ArcGIS.Catalog.GxFilterPolylineFeatureClasses(); //store a filter for point feature classes in pfilter2
                pGxDia = new GxDialogClass(); //new dialog box object that shows only shapefiles
                pGxDia.Title = "Choose a Line Feature Class";
                pGxFilter = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDia;
                pGxFilter.AddFilter(pfilter2, true);
            }

            else if (typeOfFeatureClass == "Polygon")
            {
                ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pGxFilter; //establish a collection of filters 
                ESRI.ArcGIS.Catalog.GxFilterPolygonFeatureClasses pfilter2; //create a filter for point files
                pfilter2 = new ESRI.ArcGIS.Catalog.GxFilterPolygonFeatureClasses(); //store a filter for point feature classes in pfilter2
                pGxDia = new GxDialogClass(); //new dialog box object that shows only shapefiles
                pGxDia.Title = "Choose a Polygon Feature Class";
                pGxFilter = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDia;
                pGxFilter.AddFilter(pfilter2, true);
            }


            ESRI.ArcGIS.Catalog.IEnumGxObject gxEnum = null;
            notanythingselected = pGxDia.DoModalOpen(this.Handle.ToInt32(), out gxEnum);
            if (notanythingselected == false) //if user hits cancel or x on the add data menu, do nothing
            {
                return;
            }
            else //add a shapefile to the map by this long process down here
            {
                gxEnum.Reset();
                gxObj = gxEnum.Next();



                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory wksFact; //identify and create a shapefile workspace factory to access the file from.

                Type t = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
                System.Object obj = Activator.CreateInstance(t);

                wksFact = (ShapefileWorkspaceFactory)obj;

                ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featWrk; //create a new feature class object from selected workspace

                featWrk = (IFeatureWorkspace)wksFact.OpenFromFile(gxObj.Parent.FullName, 0);
                ESRI.ArcGIS.Geodatabase.IFeatureClass fClass;
                fClass = featWrk.OpenFeatureClass(gxObj.Name);

                ESRI.ArcGIS.Carto.IFeatureLayer lyrToAdd; //create a feature layer from featureclass object
                lyrToAdd = new FeatureLayer();
                lyrToAdd.FeatureClass = fClass;
                lyrToAdd.Name = lyrToAdd.FeatureClass.AliasName;


                ESRI.ArcGIS.ArcMapUI.IMxDocument mxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)(ArcMap.Application.Document); //Access current document
                IMap pMap = mxDoc.FocusMap;

                ESRI.ArcGIS.Carto.IEnumLayer pEnumLayer;
                pEnumLayer = mxDoc.FocusMap.Layers;
                pMap.AddLayer(lyrToAdd); //add the layer to the current .mxd  
                if (typeOfFeatureClass == "Point") //populate point combobox values
                {
                    chkPointEnabled.Checked = true;
                    cboFCPoint.Items.Add(lyrToAdd.Name);
                    cboFCPoint.SelectedIndex = cboFCPoint.FindStringExact(lyrToAdd.Name);
                
                }
                
                else if (typeOfFeatureClass == "Line") //populate line combobox values
                {
                    chkLineEnabled.Checked = true;   
                    cboFCLine.Items.Add(lyrToAdd.Name);
                    cboFCLine.SelectedIndex = cboFCLine.FindStringExact(lyrToAdd.Name);
                   
                }
                else if (typeOfFeatureClass == "Polygon") //populate polygon combobox values
                {
                    chkPolyEnabled.Checked = true;
                    cboFCPoly.Items.Add(lyrToAdd.Name);
                    cboFCPoly.SelectedIndex = cboFCPoly.FindStringExact(lyrToAdd.Name);
                }
                
                
                
            }
        }


        public IFeatureClass FeatureToPolygonSpatialJoin(IFeatureLayer pInFeatLayer, IFeatureLayer pPolyLayer, String strPath, String strName)
        {
            ESRI.ArcGIS.Geodatabase.IDataset pWkSpDataset;
            ESRI.ArcGIS.Geodatabase.IWorkspaceName pWkSpName;
            ESRI.ArcGIS.Geodatabase.IDataset pDataset;
            pDataset = (IDataset)pPolyLayer.FeatureClass;
            pWkSpDataset = (IDataset)pDataset.Workspace;
            pWkSpName = (IWorkspaceName)pWkSpDataset.FullName;

            //create the name object for the output join by location
            ESRI.ArcGIS.Geodatabase.IFeatureClassName pFCName;
            ESRI.ArcGIS.Geodatabase.IDatasetName pOutDSName;

            pFCName = (IFeatureClassName)new FeatureClassName();
            pFCName.FeatureType = esriFeatureType.esriFTSimple;
            pFCName.ShapeFieldName = "Shape";
            pFCName.ShapeType = esriGeometryType.esriGeometryPolygon;

            pOutDSName = (IDatasetName)pFCName;
            pOutDSName.Name = strName;
            pOutDSName.WorkspaceName = pWkSpName;

            ESRI.ArcGIS.esriSystem.IName pName;
            pName = (IName)pOutDSName;

            //Do a join by location that joins the attributes of the point point cantained within each polygon
            ESRI.ArcGIS.ArcMapUI.ISpatialJoin pSpatialJoin;
            pSpatialJoin = new ESRI.ArcGIS.ArcMapUI.SpatialJoinClass();
            pSpatialJoin.JoinTable = (ITable)pInFeatLayer.FeatureClass; //input
            pSpatialJoin.SourceTable = (ITable)pPolyLayer.FeatureClass; //output
            pSpatialJoin.set_ShowProcess(false, 1);
            pSpatialJoin.LeftOuterJoin = false;


            return pSpatialJoin.JoinNearest(pName, 0);



        }
        
        public string CreateTempFolder(string rootFolder)
        {
            System.IO.DirectoryInfo dso = new System.IO.DirectoryInfo(rootFolder); //get file info from specified output pathway
            
            if (Directory.Exists(rootFolder + "\\tmpVoro0001"))
            {
                Directory.Delete(rootFolder + "\\tmpVoro0001", true);
            }
            System.IO.Directory.CreateDirectory(rootFolder + "\\tmpVoro0001");

            return rootFolder + "\\tmpVoro0001";

        }



  

        public void ConvertFeatureClass(IFeatureClass inFeatureClass, IWorkspace outWorkspace, string newName)
        {
            // get FeatureClassName for input
            IDataset inDataset = inFeatureClass as IDataset;
            IFeatureClassName inFeatureClassName = inDataset.FullName as IFeatureClassName;
            IWorkspace inWorkspace = inDataset.Workspace;

            // get WorkSpaceName for output
            IDataset outDataset = outWorkspace as IDataset;
            IWorkspaceName outWorkspaceName = outDataset.FullName as IWorkspaceName;

            // Create new FeatureClassName
            IFeatureClassName outFeatureClassName = new FeatureClassNameClass();
            // Assign it a name and a workspace
            IDatasetName datasetName = outFeatureClassName as IDatasetName;
            datasetName.Name = newName == "" ? (inFeatureClassName as IDatasetName).Name : newName;
            datasetName.WorkspaceName = outWorkspaceName;

            
            // Check for field conflicts.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields inFields = inFeatureClass.Fields;
            IFields outFields;
            IEnumFieldError enumFieldError;
            fieldChecker.InputWorkspace = inWorkspace;
            fieldChecker.ValidateWorkspace = outWorkspace;
            fieldChecker.Validate(inFields, out enumFieldError, out outFields);
            // Check enumFieldError for field naming confilcts

            //Convert the data.
            IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
            featureDataConverter.ConvertFeatureClass(inFeatureClassName, null, null,
            outFeatureClassName, null, outFields, "", 100, 0);
        }
        static IRasterDataset OpenFileRasterDataset(string folderName, string datasetName)
        {
            //Open raster file workspace.
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(folderName, 0);

            //Open file raster dataset. 
            IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(datasetName);
            return rasterDataset;
        }

     
  
    }

    public static class Extensions
    {

        public static string RightExtract(this string value, int length) //right function to extract text
        {
            return value.Substring(value.Length - length);
        }

        public static string LeftExtract(this string value, int length) //left function to extract text
        {
            return value.Substring(0, length);
        }
    } 
}
