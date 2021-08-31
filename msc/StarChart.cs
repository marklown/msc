using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msc
{
    public partial class StarChart : UserControl
    {
        private double Fov;
        private Dso Dso;
        private List<Star> Stars;
        private List<Dso> Dsos;
        private Graphics G = null;
        private Font font = new System.Drawing.Font("Arial Bold", 10);
        private bool LabelObjects = false;
        private double LabelObjectsLimitingMag = 16.0;

        public StarChart()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        public void SetData(Dso dso, List<Star> stars, List<Dso> dsos, double fov)
        {
            Fov = fov;      // Fov to render
            Dso = dso;      // The DSO in the center of the Fov
            Stars = stars;  // The stars found within the Fov
            Dsos = dsos;    // The other DSOs found within the Fov (to optionally label)
            Invalidate();
        }

        public void SetLabelObjectsEnabled(bool enabled)
        {
            LabelObjects = enabled;
            Invalidate();
        }

        public void SetLabelObjectsLimitingMag(double mag)
        {
            LabelObjectsLimitingMag = mag;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            DrawChart();
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected void DrawChart()
        {
            G.DrawEllipse(new Pen(Color.DarkGray), new Rectangle(5, 5, Width-10, Height-10));
            if (Stars != null)
            {
                foreach (var star in Stars)
                {
                    Point p = EqToXy(star.Ra,star.Dec, Dso, Fov);
                    int size = GetSizeFromMag(star);
                    if (size <= 1)
                    {
                        G.FillRectangle(new SolidBrush(Color.White), p.X, p.Y, 1, 1);
                    } else
                    {
                        G.FillEllipse(new SolidBrush(Color.White), p.X, p.Y, size, size);
                    }
                }
            }
            if (Dso != null)
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(150, 0, 0, 0)), 0, 0, 210, 45);
                string header = String.Format(
                    "{0} {10} - {1} in {2}\n" +
                    "RA: {3:0.##}\u00B0, Dec: {4:0.##}\u00B0, Mag: {5:0.##}\n" +
                    "{6:0.##}\u00B0, {7:0.##}\u00B0, {8:0.##}\u00B0\n" +
                    "{9:0.##}\u00B0 FOV", 
                    Dso.Id, GetTypeName(Dso.Type), Dso.Const, Dso.RA, Dso.Dec, Dso.Mag, 
                    ArcminsToDegrees(Dso.R1), ArcminsToDegrees(Dso.R2), ArcminsToDegrees(Dso.Angle), Fov, Dso.Name);
                G.DrawString(header, font, new SolidBrush(Color.Red), 2, 2);
            }
            if (LabelObjects && Dsos != null)
            {
                foreach (var dso in Dsos)
                {
                    if (dso.Mag <= LabelObjectsLimitingMag)
                    {
                        DrawDsoLabel(dso);
                    }
                }
            }
        }

        protected Point EqToXy(double ra, double dec, Dso dso, double fov)
        {
            double x = dso.RA - ra;
            double y = dso.Dec - dec;
            double scaleX = (Width - 10) / fov;
            double scaleY = (Height - 10) / fov;
            x *= scaleX;
            y *= scaleY;
            x = x + (Width - 10) / 2.0;
            y = y + (Height - 10) / 2.0;
            return new Point((int)x, (int)y);
        }

        protected double ArcminsToDegrees(double arcmins)
        {
            return arcmins / 60.0;
        }

        protected int GetSizeFromMag(Star star)
        {
            const double MAX = 16.0;
            double mag = MAX - star.Mag;
            double norm = mag / MAX;
            return norm <= 0 ? 1 : (int)(norm * 12);
        }

        protected void DrawDsoLabel(Dso dso)
        {
            string label = String.Format("{0}", dso.Id);
            Point p = EqToXy(dso.RA, dso.Dec, Dso, Fov);
            double scaleX = (Width - 10) / Fov;
            double scaleY = (Height - 10) / Fov;

            if (dso.Type.Contains("OC"))
            {
                float r1 = (float)(ArcminsToDegrees(dso.R1) * scaleX);
                float r2 = (float)(ArcminsToDegrees(dso.R1) * scaleY);
                Pen pen = new Pen(Color.Red);
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                pen.DashPattern = new float[] { 10.0f, 10.0f, 10.0f, 10.0f };
                G.DrawEllipse(pen, p.X - r1, p.Y - r2, r1 * 2, r2 * 2);
                pen.Dispose();
                G.DrawString(label, font, new SolidBrush(Color.Red), p.X + r1, p.Y - r2);
            }
            if (dso.Type.Contains("GC"))
            {
                float r1 = (float)(ArcminsToDegrees(dso.R1) * scaleX);
                float r2 = (float)(ArcminsToDegrees(dso.R1) * scaleY);
                Pen pen = new Pen(Color.Red);
                pen.Width = 1;
                G.DrawEllipse(pen, p.X - r1, p.Y - r2, r1 * 2, r2 * 2);
                G.DrawLine(pen, p.X, p.Y - r2, p.X, p.Y + r2);
                G.DrawLine(pen, p.X - r1, p.Y, p.X + r1, p.Y);
                pen.Dispose();
                G.DrawString(label, font, new SolidBrush(Color.Red), p.X + r1, p.Y - r2);
            }
            if (dso.Type == "Gxy")
            {
                var origTransform = G.Transform;
                float r1 = (float)(ArcminsToDegrees(dso.R1) * scaleX);
                float r2;
                if (dso.R2 != 0)
                {
                    r2 = (float)(ArcminsToDegrees(dso.R2) * scaleY);
                } else
                {
                    r2 = r1;
                }
                Pen pen = new Pen(Color.Red);
                pen.Width = 1;
                Matrix rot = new Matrix();
                rot.RotateAt((float)dso.Angle + 90, new Point((int)(p.X), (int)(p.Y)));
                G.Transform = rot;
                G.DrawEllipse(pen, p.X - r1, p.Y - r2, r1 * 2, r2 * 2);
                G.Transform = origTransform;
                pen.Dispose();
                G.DrawString(label, font, new SolidBrush(Color.Red), p.X + r1 / 2, p.Y - r2 / 2);
            }
            if (dso.Type.Contains("Neb") || dso.Type.Contains("DN"))
            {
                var origTransform = G.Transform;
                float r1 = (float)(ArcminsToDegrees(dso.R1) * scaleX);
                float r2;
                if (dso.R2 != 0)
                {
                    r2 = (float)(ArcminsToDegrees(dso.R2) * scaleY);
                }
                else
                {
                    r2 = r1;
                }
                Pen pen = new Pen(Color.Red);
                pen.Width = 1;
                if (dso.Type.Contains("DN"))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pen.DashPattern = new float[] { 10.0f, 10.0f, 10.0f, 10.0f };
                }
                Matrix rot = new Matrix();
                rot.RotateAt((float)dso.Angle + 90, new Point((int)(p.X), (int)(p.Y)));
                G.Transform = rot;
                G.DrawRectangle(pen, p.X - r1, p.Y - r2, r1 * 2, r2 * 2);
                G.Transform = origTransform;
                pen.Dispose();
                G.DrawString(label, font, new SolidBrush(Color.Red), p.X + r1 / 2, p.Y - r2 / 2);
            }
            if (dso.Type.Contains("PN"))
            {
                float r1 = (float)(ArcminsToDegrees(dso.R1) * scaleX);
                float r2 = (float)(ArcminsToDegrees(dso.R1) * scaleY);
                Pen pen = new Pen(Color.Red);
                pen.Width = 1;
                G.DrawEllipse(pen, p.X - r1, p.Y - r2, r1 * 2, r2 * 2);
                G.DrawLine(pen, p.X, p.Y - r2, p.X, p.Y - r2 - 10);
                G.DrawLine(pen, p.X, p.Y + r2, p.X, p.Y + r2 + 10);
                G.DrawLine(pen, p.X - r1, p.Y, p.X - r1 - 10, p.Y);
                G.DrawLine(pen, p.X + r1, p.Y, p.X + r1 + 10, p.Y);
                pen.Dispose();
                G.DrawString(label, font, new SolidBrush(Color.Red), p.X + r1, p.Y - r2);
            }
        }

        protected string GetTypeName(string type)
        {
            if (type == "OC") return "Open Cluster";
            if (type == "GC") return "Globular Cluster";
            if (type == "Neb") return "Nebula";
            if (type == "Gxy") return "Galaxy";
            if (type == "**") return "Double Star";
            if (type == "***") return "Triple Star";
            if (type == "GxyCld") return "Star Cloud in Galaxy";
            if (type == "Dn") return "Dark Nubula";
            if (type == "Ast") return "Asterism";
            if (type == "PN") return "Planetary Nebula";
            return type;
        }
    }
}
