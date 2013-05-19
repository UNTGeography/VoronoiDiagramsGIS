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


namespace Voronoi_2._0
{


    public partial class frmVoronoi : Form
    {

        public frmVoronoi()
        {
   
            

            InitializeComponent();
            cboWeightField.Enabled = false;
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)(ArcMap.Application.Document); //Access current document
            IMap pMap = mxDoc.FocusMap; //set pMap as current document
            AddtoShapefileCombobox(pMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}");
            try
            {
                cboShapefile.SelectedIndex = 0;
                GetLayerInfo(cboShapefile.SelectedItem.ToString());
                
                
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

        

        private void cboShapefile_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grabs point, line or polygon from table of contents. Populate combobox below with NUMERICAL fields of attribute table to select from.
            //upon selection, get the UNITS of the selected shapefile from ArcObjects, and populate field next to Cell size factor
            
         
            GetLayerInfo(cboShapefile.SelectedItem.ToString());
            
       
        }

       

        private void btnOpenShapefile_Click(object sender, EventArgs e) //use ArcCatalog to browse to map if not in TOC
        {
            
            ESRI.ArcGIS.Catalog.IGxObjectFilterCollection pGxFilter; //establish a collection of filters
            ESRI.ArcGIS.Catalog.GxFilterShapefiles pfilter2; //create a filter 
            Boolean notanythingselected;
            ESRI.ArcGIS.Catalog.IGxObject gxObj; //declare an instance of GxObjects
            ESRI.ArcGIS.CatalogUI.IGxDialog pGxDia; //declare an instance of GxDialogs
            pfilter2 = new ESRI.ArcGIS.Catalog.GxFilterShapefiles(); //store a filter for shapefiles only in pfilter2

            pGxDia = new GxDialogClass(); //new dialog box object that shows only shapefiles
            pGxDia.Title = "Choose a Shapefile";

            pGxFilter = (ESRI.ArcGIS.Catalog.IGxObjectFilterCollection)pGxDia; 

            pGxFilter.AddFilter(pfilter2, true);
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
                AddtoShapefileCombobox(pMap, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}");
                cboShapefile.SelectedIndex = cboShapefile.FindStringExact(lyrToAdd.Name);                                                                                                  
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
            if (cboShapefile.SelectedItem != null)
            {

                pGxDialog.Name = cboShapefile.SelectedItem.ToString();
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
            if (cboShapefile.SelectedItem != null)
            {

            pGxDialog.Name = cboShapefile.SelectedItem.ToString();
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

        private void cboWeightField_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void chkWeightField_CheckedChanged(object sender, EventArgs e)
        {
            //if USE WEIGHT FIELD is checked, populate cboWeightField with field names of the attribute table of the selected shapefile
            if (chkWeightField.Checked)
            {
                cboWeightField.Enabled = true;
            }
            else
            {
                cboWeightField.Enabled = false;
            }
        }
           

        private void chkSpatialJoin_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void txtCellSize_TextChanged(object sender, EventArgs e)
        {

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
            //int g_pct = 0;

        

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


            if (!(cboShapefile.SelectedIndex > -1)) //check for no value for input shapefile
            {
                MessageBox.Show("No input shapefile selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }
       
            if (chkWeightField.Checked == true)
            {
                //IF WEIGHT FIELD IS CHECKED,
                //CREATE WEIGHTED 
                MessageBox.Show("Weighted Voronoi diagram functionality is currently disabled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }
            

            ESRI.ArcGIS.ArcMapUI.IMxDocument pDoc;
            pDoc = ArcMap.Document;//get current mxd and set it as pDoc
            ESRI.ArcGIS.Carto.IActiveView activeView;
            activeView = pDoc.ActiveView;
            IMap pMap = pDoc.FocusMap; //set pMap as current document
            ESRI.ArcGIS.Carto.ILayer pLayer; //create a current layer
            ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer; //THIS FEATURE LAYER WILL EVENTUALLY BE USED TO CREATE THE DISTANCE RASTER
            ESRI.ArcGIS.Geodatabase.IFeatureClass pFeatureClass;
            ESRI.ArcGIS.Carto.IEnumLayer pEnumLayer; //set up an array to hold the layers in map
            pEnumLayer = pDoc.FocusMap.Layers; //get all layers in the map
            pLayer = pEnumLayer.Next();

            while (pLayer != null)
            {
                if (String.Compare(pLayer.Name, cboShapefile.SelectedItem.ToString()) == 0) //set current layer to currently selected combobox value and end the looping of layers
                {
                    break;

                }
                pLayer = pEnumLayer.Next();

            }

     

            

            pFeatureLayer = (IFeatureLayer)pLayer; //***IMPORTANT*** THIS CODE CASTS THE CURRENT SELECTED LAYER IN COMBOBOX TO pFeatureLayer, WHICH WILL BE USED TO CONDUCT THE DISTANCE OPERATION
            pFeatureClass = pFeatureLayer.FeatureClass; //get current feature class from feature layer



            if (pFeatureLayer.Selectable == false) //check and make sure layer is selectable
            {
                MessageBox.Show("Please choose an input shapefile with selectable features", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }

     

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
           
            ////////////
            //ONCE ALL CHECKS ARE DONE AND OK, BEGIN PROCESS OF CREATING RASTER
            ////////////

            
            
            string strOutDistRasterFile = fsoDist.Name;
            string strOutVoronoiFile = fsoVoronoi.Name;
            string strOutRasPath = dsoDist.FullName;
            string strOutVoronoiPath = dsoVoronoi.FullName;

            string g_tempFolder = CreateTempFolder(strOutRasPath);
            
            ESRI.ArcGIS.SpatialAnalyst.IDistanceOp pDistanceOP; //Create raster DistanceOp object
            pDistanceOP = new RasterDistanceOpClass(); //define the newly created object as a new raster distance operation object
            ESRI.ArcGIS.GeoAnalyst.IRasterAnalysisEnvironment pEnv; //Create a raster analysis environment. This sets up an area to do raster operations in
            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF;//create a workspace factory for raster. The workspace factory is used to create new workspaces
            ESRI.ArcGIS.DataSourcesRaster.IRasterWorkspace pTempRWS; //create a workspace for temporary raster
            ESRI.ArcGIS.DataSourcesRaster.IRasterWorkspace pFinalRWS; //create a workspace for final raster
           
            pWSF = new RasterWorkspaceFactoryClass(); //define the newly created workspace factory as an object
     
            pTempRWS = (IRasterWorkspace)pWSF.OpenFromFile(g_tempFolder, 0);
            pFinalRWS = (IRasterWorkspace)pWSF.OpenFromFile(strOutRasPath, 0);
            ESRI.ArcGIS.Geodatabase.IWorkspace pWS;
            pWS = (IWorkspace)pFinalRWS;
          

            double width;
            double height;
            double cellsize;

            ESRI.ArcGIS.Geometry.IEnvelope pExt; //create a new envelope to hold the shapefile size information
            pExt = new EnvelopeClass(); //instantiate the envelope as an object
            ESRI.ArcGIS.Geodatabase.IGeoDataset pGeodataset; //create a geodataset to hold envelope information
            pGeodataset = (IGeoDataset)pFeatureLayer; //HERE pFeatureLayer is CAST TO GEODATASET
            width = pGeodataset.Extent.Envelope.Width; //GETS THE WIDTH OF THE GEODATASET USED TO CREATE RASTER
            height = pGeodataset.Extent.Envelope.Height; //GETS THE HEIGHT OF THE GEODATASET USED TO CREATE RASTER
     

         
            //long cellSizeFactor; //USED IN WEIGHTED VORONOI CALCULATION

            bool containsInvalidCharacter = false; //flag for bad cellsize value

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

            pExt.XMin = pGeodataset.Extent.Envelope.XMin;
            pExt.YMin = pGeodataset.Extent.Envelope.YMin;
            pExt.XMax = pGeodataset.Extent.Envelope.XMax;
            pExt.YMax = pGeodataset.Extent.Envelope.YMax;

            
            ////////////
            //Loop through all points:
            ////////////

            ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSFact; //create general workspace factory (this is the directory you will be working in)
            //pWSFact = new ShapefileWorkspaceFactoryClass(); //set general workspace as a shapefile workspace. WARNING: USING THIS CAUSES A STRANGE COM REWRAPPING ERROR.
            
            Type t = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
            System.Object obj2 = Activator.CreateInstance(t);

            pWSFact = (ShapefileWorkspaceFactory)obj2;
           
            ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFeatureWorkspace; //create a feature workspace to handle features
            pFeatureWorkspace = (IFeatureWorkspace)pWSFact.OpenFromFile(g_tempFolder, 0); //set feature workspace as the shapefile workspace defined earlier, and specify a file location

            ESRI.ArcGIS.Geodatabase.IFeatureCursor pFCursor1; //use to loop through the feature class. You can think of this as a marker that grabs each feature as it loops through the array of features
            pFCursor1 = pFeatureLayer.Search(null, false);

            long recNum; //store the position you are at in the array. This is the array counter
            recNum = pFeatureLayer.FeatureClass.FeatureCount(null); //set our counter at 0
          
            if (recNum == 0) //check for no features in shapefile
            {
                MessageBox.Show("Cannot create diagram. Layer has no features.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOK.Enabled = true;
                return;
            }
            if (recNum == 1) //check for only one feature in shapefile
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





            ////////////
            //CODE TO SET FACTORS FOR WEIGHTED VORONOI ENDS HERE
            ////////////
        

            //ESRI.ArcGIS.Geodatabase.IGeoDataset pTempMinRaster;
            ESRI.ArcGIS.Geodatabase.IGeoDataset pMinimumRaster;
            //ESRI.ArcGIS.Carto.IFeatureLayer pTempFeatureLayer;
            ESRI.ArcGIS.Geodatabase.IGeoDataset pTempGeodataset;
            
            ////////////
            //Loop through all features.
            ////////////
        
            ESRI.ArcGIS.GeoAnalyst.IConversionOp pConvOp;
            pConvOp = new RasterConversionOpClass();

            ESRI.ArcGIS.Carto.IFeatureSelection pFeatureSelection;
            pFeatureSelection = (IFeatureSelection)pFeatureLayer; // ***IMPORTANT*** THIS SETS THE CURRENT FEATURE LAYER IN COMBOBOX TO THE FEATURE SELECTION TO BE USED FOR CONVERSION TO RASTER 
            ESRI.ArcGIS.Geodatabase.ISelectionSet pSelectionSet;
            pSelectionSet = pFeatureSelection.SelectionSet; //Defines a selection set of features. The pFeatureLayer information is stored here.
          
            ESRI.ArcGIS.Geodatabase.IFeatureCursor pFCursor2;
            ESRI.ArcGIS.Geodatabase.IFeature pFeature2;
            ESRI.ArcGIS.Geodatabase.IQueryFilter pQueryFilter;
           
            pQueryFilter = new QueryFilterClass();
            pFCursor2 = pFeatureLayer.Search(null, false);
            pSelectionSet = pFeatureSelection.SelectionSet;
            pFeature2 = pFCursor2.NextFeature();

            pEnv = (IRasterAnalysisEnvironment)pDistanceOP;
            pEnv.OutWorkspace = (IWorkspace)pTempRWS;
            pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, cellsize);
            pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, pExt);

            try
            {
                pTempGeodataset = (IGeoDataset)pFeatureLayer.FeatureClass;
                pMinimumRaster = pDistanceOP.EucDistanceFull(pTempGeodataset, true, false, false);

                pFeatureSelection.Clear();
                g_pPro.Hide();

                pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, cellsize);
                pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, pExt);

                ESRI.ArcGIS.DataSourcesRaster.IRasterBandCollection pRasBandCol;
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
                //CREATE FLOW DIRECTION AND BASIN RASTERS WHICH WILL BE USED TO GENERATE POLYGON SHAPEFILE
                ////////////

                ESRI.ArcGIS.SpatialAnalyst.IHydrologyOp pHydrologyOp;
                pHydrologyOp = new RasterHydrologyOpClass();

                ESRI.ArcGIS.Geodatabase.IGeoDataset pFlowDirRaster;
                ESRI.ArcGIS.Geodatabase.IGeoDataset pBasinRaster;

                g_pPro.Message = "Generating Voronoi Diagram ...";
                pFlowDirRaster = pHydrologyOp.FlowDirection(pMinimumRaster, false, false); //CREATE FLOW DIRECTION RASTER

                pBasinRaster = pHydrologyOp.Basin(pFlowDirRaster); //CREATE BASIN RASTER



                ////////////
                //RASTER TO POLYGON 
                ////////////

                ESRI.ArcGIS.GeoAnalyst.IConversionOp pConversionOp;
                pConversionOp = new RasterConversionOpClass();
                ESRI.ArcGIS.Geodatabase.IGeoDataset pVoronoiFClassOut;
                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pSWSF;

                Type t1 = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory"); //DO NOT REMOVE. Fixes strange casting error from COM rewrapping. 
                System.Object obj3 = Activator.CreateInstance(t);

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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        ////////////
        //FORM CODING ENDS HERE. IMPLEMENTED METHODS AND EXTENSIONS BEYOND THIS POINT
        ////////////

        public void AddtoShapefileCombobox(ESRI.ArcGIS.Carto.IMap map, System.String layerCLSID)
        //loops through all feature classes in .mxd and populates the values into the Add Shapefile Combobox
        {
            cboShapefile.Items.Clear();
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
                    cboShapefile.Items.Add(layer.Name);

                    layer = enumLayer.Next();
                }
            }
            catch
            {
            }

           
        }

        
        public void GetLayerInfo(string NametoMatch) //Sets current layer to given input string argument and populates several areas of form
        {

            ESRI.ArcGIS.ArcMapUI.IMxDocument pDoc;
            pDoc = ArcMap.Document;//get current mxd and set it as pDoc
            ESRI.ArcGIS.Carto.IActiveView activeView;
            activeView = pDoc.ActiveView;

            IMap pMap = pDoc.FocusMap; //set pMap as current document

            ESRI.ArcGIS.Carto.IEnumLayer pEnumLayer; //get all layers in the map
            pEnumLayer = pDoc.FocusMap.Layers;

            ESRI.ArcGIS.Carto.ILayer pLayer; //create a current layer
            pLayer = pEnumLayer.Next();



            while (pLayer != null)
            {
                if (String.Compare(pLayer.Name, NametoMatch) == 0) //set current layer to currently selected combobox value and end the looping of layers
                {
                    break;

                }
                pLayer = pEnumLayer.Next();

            }
     

            //set the CellSize box to the following formula: (Minimum of the height & width of extent of input shapefile) / 250

            txtCellSize.Text = ((Math.Min((pLayer.AreaOfInterest.Width), (pLayer.AreaOfInterest.Height))) / 250.0).ToString();

            try //this will attempt to display the units of the spatial refererence next to the cell size box, if the units are accessible.
            {

                ESRI.ArcGIS.Geometry.IProjectedCoordinateSystem lyrCoordSystem = (IProjectedCoordinateSystem)pLayer.AreaOfInterest.SpatialReference; //cast to inherited class

                lblSpatialReference.Text = lyrCoordSystem.CoordinateUnit.Name;

            }

            catch
            {
            }

            ESRI.ArcGIS.Carto.IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer; //cast to inherited class. 

            ESRI.ArcGIS.Geodatabase.IFeatureClass fClass = pFeatureLayer.FeatureClass; //get the featureclass information from feature layer
            ESRI.ArcGIS.Geodatabase.IFields pFields = fClass.Fields; //create an array of all the fields of the attribute table of the feature class

            try
            {
                cboWeightField.Items.Clear(); //avoid null exception
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
                    cboWeightField.Items.Add(pField.Name); //if numerical field, add to weight field combobox
                }


            }
     
            try
            {
                cboWeightField.SelectedIndex = 0; //avoid null exception if there are no valid weightable fields
            }
            catch
            {
            }

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
