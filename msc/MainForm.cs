using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Microsoft.Data.Sqlite;

namespace msc
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "MSC v0.1";
            if (!IsDatabaseFilePresent())
            {
                DatabaseDownloadForm form = new DatabaseDownloadForm();
                form.ShowDialog();
            }
        }


        private bool IsDatabaseFilePresent()
        {
            if (File.Exists("./stars.db"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Dso FindDso(string id)
        {
            Dso dso = new Dso();
            bool found = false;
            using (var con = new SqliteConnection("Data Source=stars.db"))
            {
                string[] splitId = id.Split(' ');
                if (splitId.Length < 2)
                {
                    MessageBox.Show("Please include a space between the catalog and numerical ID");
                    return null;
                }
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText =
                    @"
                    SELECT Ra, Dec, Mag, Name, Cat, CatID, Type, Const, r1, r2, angle
                    FROM dsos
                    WHERE Cat = $cat AND CatID = $catId
                    ";
                cmd.Parameters.AddWithValue("$cat", splitId[0]);
                cmd.Parameters.AddWithValue("$catId", splitId[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        found = true;
                        dso.RA = reader.GetDouble(0);
                        dso.Dec = reader.GetDouble(1);
                        dso.Mag = reader.GetDouble(2);
                        dso.Name = reader.GetString(3);
                        dso.Id = reader.GetString(4) + ' ' + reader.GetString(5);
                        dso.Type = reader.GetString(6);
                        dso.Const = reader.GetString(7);
                        dso.R1 = reader.GetDouble(8);
                        dso.R2 = reader.GetDouble(9);
                        dso.Angle = reader.GetDouble(10);
                    }
                }
            }
            if (!found)
            {
                return null;
            }
            return dso;
        }

        private List<Dso> FindDsos(double ra, double dec, double fov)
        {
            List<Dso> dsos = new List<Dso>();
            using (var con = new SqliteConnection("Data Source=stars.db"))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText =
                    @"
                    SELECT Ra, Dec, Mag, Name, Cat, CatID, Type, Const, r1, r2, angle
                    FROM dsos
                    WHERE Ra BETWEEN $raMin AND $raMax AND Dec BETWEEN $decMin AND $decMax
                    ";
                cmd.Parameters.AddWithValue("$raMin", ra - fov / 2.0);
                cmd.Parameters.AddWithValue("$raMax", ra + fov / 2.0);
                cmd.Parameters.AddWithValue("$decMin", dec - fov / 2.0);
                cmd.Parameters.AddWithValue("$decMax", dec + fov / 2.0);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dsos.Add(new Dso(
                            reader.GetDouble(0), reader.GetDouble(1), reader.GetDouble(2), 
                            reader.GetString(3), reader.GetString(4) + ' ' + reader.GetString(5), 
                            reader.GetString(6), reader.GetString(7), 
                            reader.GetDouble(8), reader.GetDouble(9), reader.GetDouble(10)));
                    }
                }
            }
            Console.WriteLine(String.Format("Found {0} dsos in FOV.", dsos.Count));
            return dsos;
        }

        private List<Star> FindStars(double ra, double dec, double mag, double fov)
        {
            List<Star> stars = new List<Star>();
            using (var con = new SqliteConnection("Data Source=stars.db"))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText =
                    @"
                    SELECT Ra, Dec, Mag
                    FROM stars
                    WHERE Ra BETWEEN $raMin AND $raMax AND Dec BETWEEN $decMin AND $decMax AND Mag <= $magLimit
                    ";
                cmd.Parameters.AddWithValue("$raMin", ra - fov / 2.0);
                cmd.Parameters.AddWithValue("$raMax", ra + fov / 2.0);
                cmd.Parameters.AddWithValue("$decMin", dec - fov / 2.0);
                cmd.Parameters.AddWithValue("$decMax", dec + fov / 2.0);
                cmd.Parameters.AddWithValue("$magLimit", (double)nudLimitingMag.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stars.Add(new Star(reader.GetDouble(0), reader.GetDouble(1), reader.GetDouble(2)));
                    }
                }
            }
            Console.WriteLine(String.Format("Found {0} stars in FOV brighter than magnitude {1}", stars.Count, mag));
            return stars;
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            Dso dso = FindDso(tbObjectID.Text);
            if (dso == null)
            {
                MessageBox.Show(string.Format("Could not find DSO with ID {0}", tbObjectID.Text));
                return;
            }
            Console.WriteLine(dso.ToString());
            List<Star> stars = FindStars(dso.RA, dso.Dec, (double)nudLimitingMag.Value, (double)nudFOV.Value);
            List<Dso> dsos = FindDsos(dso.RA, dso.Dec, (double)nudFOV.Value);
            starChart1.SetData(dso, stars, dsos, (double)nudFOV.Value);
            starChart1.SetLabelObjectsLimitingMag((double)nudObjectsLimitingMag.Value);
        }

        private void cbLabelDsos_CheckedChanged(object sender, EventArgs e)
        {
            starChart1.SetLabelObjectsEnabled(cbLabelDsos.Checked);
        }

        private void nudObjectsLimitingMag_ValueChanged(object sender, EventArgs e)
        {
            starChart1.SetLabelObjectsLimitingMag((double)nudObjectsLimitingMag.Value);
        }
    }
}
