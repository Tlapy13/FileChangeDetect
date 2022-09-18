using FileChangeDetect.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FileChangeDetect.Data.Data;

namespace FileChangeDetect
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ShowMessage("", Color.Green);
                UpdFilesLbl.Visible = false;
                DelFilesLbl.Visible = false;
            }

        }
        protected void AnalyzeBtn_Click(object sender, EventArgs e)
        {
            string path = PathTxt.Text;
            ShowMessage("", Color.Green);

            if (Directory.Exists(path))
            {
                try
                {
                    AnalyzeDirContent(path);
                }
                catch (Exception ex)
                {
                    SetToDefault();
                    ShowMessage(ex.Message, Color.Red);
                    // Log full message to Event Log or to application log. Only custom message is shown on web, without tech. details                                
                }

            }
            else
            {
                SetToDefault();
                ShowMessage("Invalid directory path!", Color.Red);
            }
        }

        private void SetToDefault()
        {
            UpdFilesLbl.Visible = false;
            DelFilesLbl.Visible = false;
            ClearGrid(DeletedFilesGrid);
            ClearGrid(ModifiedFilesGrid);
        }

        private void AnalyzeDirContent(string path)
        {
            string appPath = HttpContext.Current.Request.PhysicalApplicationPath;
            string hashName = GetPathHashName(path);
            string pathHash = Path.Combine(appPath, JSON, hashName + JSONAtr);

            List<FileObj> savedFileSpecs = new List<FileObj>();
            List<FileObj> foundFiles = new List<FileObj>();

            try
            {
                foundFiles = GetAvaillableFiles(path);
                if (foundFiles.Count > 0)
                {
                    savedFileSpecs = GetFileSpecsFromJson(pathHash);
                    if (savedFileSpecs.Count > 0)
                    {
                        List<FileObj> differenceList = GetDiffenceList(savedFileSpecs, foundFiles);

                        if (differenceList.Count > 0)
                        {
                            List<FileObj> deletedFiles = GetDeletedFiles(foundFiles, differenceList);
                            List<FileObj> modifiedFiles = GetModifiedFiles(differenceList, deletedFiles);

                            BindGrids(deletedFiles, modifiedFiles);

                            WritteJsonData(pathHash, GetUpdatedFoundList(foundFiles, modifiedFiles));
                        }
                        else
                        {
                            ShowMessage("no changes detected", Color.Green);
                            SetToDefault();
                        }
                    }
                    else
                    {
                        SetToDefault();
                        //no specification found - new will be created
                        WritteJsonData(pathHash, foundFiles);
                        ShowMessage("folder was verified and added for change monitoring", Color.Green);
                    }

                }
                else
                    ShowMessage("No files found in selected directory", Color.Red);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindGrids(List<FileObj> deletedFiles, List<FileObj> modifiedFiles)
        {
            try
            {
                if (modifiedFiles.Count > 0)
                {
                    ModifiedFilesGrid.DataSource = modifiedFiles;
                    ModifiedFilesGrid.DataBind();
                    UpdFilesLbl.Visible = true;
                }
                else
                {
                    ClearGrid(ModifiedFilesGrid);
                    UpdFilesLbl.Visible = false;
                }

                if (deletedFiles.Count > 0)
                {
                    DeletedFilesGrid.DataSource = deletedFiles;
                    DeletedFilesGrid.DataBind();
                    DelFilesLbl.Visible = true;
                }
                else
                {
                    ClearGrid(DeletedFilesGrid);
                    DelFilesLbl.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("BindGrids - Error occured during binging grids", ex);
            }
        }

        private void ClearGrid(GridView gv)
        {
            gv.DataSource = null;
            gv.DataBind();
        }

        private void ShowMessage(string text, Color labelColor)
        {
            MessageLbl.Text = text;
            MessageLbl.ForeColor = labelColor;
        }
    }
}