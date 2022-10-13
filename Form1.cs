using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Windows.Forms;

namespace HIT_C2_SPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [Serializable]
        class WayPoint
        {

            public float Distance;
            public bool isMin;
            public int DrawX, DrawY;
            public int PrevWayPoint;

            public WayPoint(float _Distance, bool _isMin, int _DrawX, int _DrawY, int _PrevWayPoint)
            {
                this.Distance=_Distance;
                this.isMin=_isMin;

                this.DrawX=_DrawX;
                this.DrawY=_DrawY;
                this.PrevWayPoint=_PrevWayPoint;
            } 
        }

        const float INF = 100000.0f;
        const int NotSet = -1;
        const float MeterPerPixel = 0.734190546f;

        char CurrentMode = 'N';
        bool isCustomOrig, isCustomDest;
        int CustomOrigWptIndex = NotSet, CustomDestWptIndex = NotSet;
        int OrigWptIndex = NotSet, DestWptIndex = NotSet;
        float BeginWayPointDistance = 0.0f, EndWayPointDistance = 0.0f;

        List<WayPoint> _WayPointData = new List<WayPoint>();
        List<List<float>> PathData = new List<List<float>>();
        List<PictureBox> WayPointIcon = new List<PictureBox>();


        void ApplyDijkstra(List<WayPoint> WayPointData,int BeginWayPoint)
        {
            for(int i = 0; i<WayPointData.Count; i++)
            {
                WayPointData[i].Distance=PathData[BeginWayPoint][i];
                if (WayPointData[i].Distance<INF) WayPointData[i].PrevWayPoint=BeginWayPoint;
            }

            WayPointData[BeginWayPoint].isMin=true;
            WayPointData[BeginWayPoint].PrevWayPoint=NotSet;

            int isMinCount = 1;

            while (isMinCount<WayPointData.Count)
            {
                float minDistance=INF;
                int minWayPoint=0;

                for(int i = 0; i<WayPointData.Count; i++)
                    if ((!WayPointData[i].isMin)&&(WayPointData[i].Distance<minDistance))
                    {
                        minDistance=WayPointData[i].Distance;
                        minWayPoint=i;
                    }

                WayPointData[minWayPoint].isMin=true;
                isMinCount++;

                for (int i = 0; i<WayPointData.Count; i++)
                {
                    if ((!WayPointData[i].isMin)&&(minDistance+PathData[minWayPoint][i]<WayPointData[i].Distance))
                    {
                        WayPointData[i].Distance=minDistance+PathData[minWayPoint][i];
                        WayPointData[i].PrevWayPoint=minWayPoint;
                    }
                }
            }
        }

        void AddWayPointIcon(int CenterX,int CenterY,int Index,char IconColor)
        {
            PictureBox pb = new PictureBox();

            switch (IconColor)
            {
                case 'R':
                    pb.Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_red.png");
                    break;
                case 'G':
                    pb.Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_green.png");
                    break;
                case 'B':
                    pb.Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_blue.png");
                    break;
            }

            pb.SizeMode=PictureBoxSizeMode.StretchImage;
            pb.BackColor=Color.Transparent;
            pb.Parent=MapDisplay;
            pb.Size=new Size(15, 15);
            pb.Location=new Point(CenterX-7, CenterY-7);
            pb.Visible=false;

            pb.Tag=Index;

            pb.Click+=WayPointIconOnClick;
            pb.MouseEnter+=ZoomInWayPointIcon;
            pb.MouseLeave+=ZoomOutWayPointIcon;

            WayPointIcon.Add(pb);
            MapDisplay.Controls.Add(pb);
        }

        void DrawPath(Graphics TargetGraphics, Color c,int PathWidth, int X1, int Y1, int X2, int Y2)
        {
            Pen p = new Pen(c, PathWidth);
            TargetGraphics.DrawLine(p, X1, Y1, X2, Y2);
        }

        float DistanceCalculate(int X1,int Y1,int X2,int Y2)
        {
            return (float)Math.Sqrt((X1-X2)*(X1-X2)+(Y1-Y2)*(Y1-Y2));
        }

        void FindNearestWayPoint(int X,int Y,ref int retIndex,ref float retDistance)
        {
            int NearestWaypoint=0;
            float NearestDistance = INF;

            for(int i = 0; i<_WayPointData.Count; i++)
            {
                float tDistance = DistanceCalculate(X, Y, _WayPointData[i].DrawX, _WayPointData[i].DrawY);
                if (tDistance<NearestDistance)
                {
                    NearestWaypoint=i;
                    NearestDistance=tDistance*MeterPerPixel;
                }
            }

            retDistance=NearestDistance;
            retIndex=NearestWaypoint;
        }

        void WayPointIconOnClick(object sender, EventArgs e)
        {
            switch (CurrentMode)
            {
                case 'N':
                    MessageBox.Show($"路点索引：{(sender as PictureBox).Tag}");
                    break;
                case 'O':
                    OrigWptIndex=(int)(sender as PictureBox).Tag;
                    (sender as PictureBox).Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_green.png");
                    isCustomOrig=false;

                    CurrentMode='D';
                    StatusDisplay.ForeColor=Color.RoyalBlue;
                    StatusDisplay.Text="选择终点";
                    break;
                case 'D':
                    DestWptIndex=(int)(sender as PictureBox).Tag;
                    (sender as PictureBox).Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_blue.png");
                    isCustomDest=false;
                    SelectOrigDest.Enabled=false;
                    BeginSPF.Enabled=true;
                    StatusDisplay.ForeColor=Color.Orange;
                    StatusDisplay.Text="寻路就绪";
                    break;
            }

            if (OrigWptIndex!=NotSet&&DestWptIndex!=NotSet) BeginSPF.Enabled=true;
        }

        void ZoomInWayPointIcon(object sender, EventArgs e)
        {
            (sender as PictureBox).Size=new Size(20, 20);
            (sender as PictureBox).Location=
                new Point((sender as PictureBox).Location.X-3, (sender as PictureBox).Location.Y-3);
        }

        void ZoomOutWayPointIcon(object sender, EventArgs e)
        {
            (sender as PictureBox).Size=new Size(15, 15);
            (sender as PictureBox).Location=
                new Point((sender as PictureBox).Location.X+3, (sender as PictureBox).Location.Y+3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MapDisplay.Image=Image.FromFile(Application.StartupPath+@"\uires\map.png");

            StreamReader sr = new StreamReader(Application.StartupPath+@"\data\pathdata.txt");

            int WayPointCount = int.Parse(sr.ReadLine());
 
            for(int i = 0; i<WayPointCount; i++)
            {
                WayPoint tWayPoint = new WayPoint(NotSet, false, NotSet, NotSet, NotSet);
                _WayPointData.Add(tWayPoint);
            }

            for (int i = 0; i<WayPointCount; i++)
            {
                string[] tReadData = sr.ReadLine().Split(' ');
                _WayPointData[int.Parse(tReadData[0])].DrawX=int.Parse(tReadData[1]);
                _WayPointData[int.Parse(tReadData[0])].DrawY=int.Parse(tReadData[2]);
            }

            int PathCount = int.Parse(sr.ReadLine());

            for(int i = 0; i<PathCount; i++)
            {
                List<float> tPathData = new List<float>();
                for (int j = 0; j<PathCount; j++)
                {
                    tPathData.Add(INF);
                }
                PathData.Add(tPathData);
            }

            for (int i=0;i<PathCount; i++)
            {
                PathData[i][i]=0;
                string[] tReadData = sr.ReadLine().Split(' ');
                int a = int.Parse(tReadData[0]), b = int.Parse(tReadData[1]);
                PathData[b][a]=PathData[a][b]=float.Parse(tReadData[2]);
            }

            for(int i = 0; i<WayPointCount; i++) AddWayPointIcon(_WayPointData[i].DrawX, _WayPointData[i].DrawY,i,'R');

            sr.Close();
        }

        private void SelectOrigDest_Click(object sender, EventArgs e)
        {
            CurrentMode='O';

            StatusDisplay.ForeColor=Color.Green;
            StatusDisplay.Text="选择起点";
        }

        private void BeginSPF_Click(object sender, EventArgs e)
        {
            List<WayPoint> WayPointData = new List<WayPoint>();

            using (Stream objectStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, _WayPointData);
                objectStream.Seek(0, SeekOrigin.Begin);
                WayPointData=formatter.Deserialize(objectStream) as List<WayPoint>;
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();

            ApplyDijkstra(WayPointData, OrigWptIndex);

            sw.Stop();

            Graphics g = MapDisplay.CreateGraphics();

            for (int i = DestWptIndex; i!=OrigWptIndex; i=WayPointData[i].PrevWayPoint)
            {
                DrawPath(g,Color.Lime, 5,WayPointData[i].DrawX, WayPointData[i].DrawY,
                    WayPointData[WayPointData[i].PrevWayPoint].DrawX,
                    WayPointData[WayPointData[i].PrevWayPoint].DrawY);
            }

            if (isCustomDest) DrawPath(g, Color.Lime, 5,
                 WayPointIcon[CustomDestWptIndex].Location.X+7,
                 WayPointIcon[CustomDestWptIndex].Location.Y+7,
                 WayPointData[DestWptIndex].DrawX, WayPointData[DestWptIndex].DrawY);

            if (isCustomOrig) DrawPath(g, Color.Lime, 5,
                 WayPointIcon[CustomOrigWptIndex].Location.X+7,
                 WayPointIcon[CustomOrigWptIndex].Location.Y+7,
                 WayPointData[OrigWptIndex].DrawX, WayPointData[OrigWptIndex].DrawY);

            StatusDisplay.ForeColor=Color.Red;
            StatusDisplay.Text="寻路成功";

            CurrentMode='N';

            float TotalDistance = BeginWayPointDistance+EndWayPointDistance+WayPointData[DestWptIndex].Distance;

            DistLabel.Visible=true;
            DistanceDisplay.Text=TotalDistance.ToString("#0.00")+"m";
            DistanceDisplay.Visible=true;
            TimeConsumptionDisplay.Text="算法耗时："+string.Format("{0:f2}",sw.ElapsedTicks/1000.0)+"us";
            TimeConsumptionDisplay.Visible=true;
            WalkingTimeEst.Text=$"走路大约需要{Math.Round(TotalDistance/90)}min";
            WalkingTimeEst.Visible=true;

        }

        private void ClearScreen_Click(object sender, EventArgs e)
        {
            if (isCustomDest)
            {
                WayPointIcon[CustomDestWptIndex].Visible=false;
                WayPointIcon.RemoveAt(CustomDestWptIndex);

                isCustomDest=false;
                CustomDestWptIndex=NotSet;
                EndWayPointDistance=0.0f;
            }

            if (isCustomOrig)
            {
                WayPointIcon[CustomOrigWptIndex].Visible=false;
                WayPointIcon.RemoveAt(CustomOrigWptIndex);
                isCustomOrig=false;
                CustomOrigWptIndex=NotSet;
                BeginWayPointDistance=0.0f;
            }

            if((!isCustomDest)&&DestWptIndex!=NotSet)
                WayPointIcon[DestWptIndex].Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_red.png");

            if ((!isCustomOrig)&&OrigWptIndex!=NotSet)
                WayPointIcon[OrigWptIndex].Image=Image.FromFile(Application.StartupPath+@"\uires\wpt_red.png");

            DestWptIndex=NotSet;
            OrigWptIndex=NotSet;

            if (CurrentMode!='N')
            {
                StatusDisplay.ForeColor=Color.Black;
                StatusDisplay.Text="重新开始";
            }

            CurrentMode='N';

            DistLabel.Visible=false;
            DistanceDisplay.Text="0.00m";
            DistanceDisplay.Visible=false;
            TimeConsumptionDisplay.Text="算法耗时：0ms";
            TimeConsumptionDisplay.Visible=false;
            WalkingTimeEst.Text="走路大约需要0min";
            WalkingTimeEst.Visible=false;

            SelectOrigDest.Enabled=true;
            BeginSPF.Enabled=false;

            if(checkBox1.Checked) checkBox1.Checked=false;
            else
            {
                for (int i = 0; i<WayPointIcon.Count; i++) WayPointIcon[i].Visible=false;
                MapDisplay.Image=Image.FromFile(Application.StartupPath+@"\uires\map.png");
            }
        }

        private void MapDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            switch (CurrentMode)
            {
                case 'N': return;
                case 'O':
                    AddWayPointIcon(e.X, e.Y, WayPointIcon.Count, 'G');
                    CustomOrigWptIndex=WayPointIcon.Count-1;
                    WayPointIcon[CustomOrigWptIndex].Visible=true;
                    isCustomOrig=true;
                    FindNearestWayPoint(e.X, e.Y, ref OrigWptIndex, ref BeginWayPointDistance);
                    SelectOrigDest.Enabled=false;

                    CurrentMode='D';
                    StatusDisplay.ForeColor=Color.RoyalBlue;
                    StatusDisplay.Text="选择终点";
                    break;
                case 'D':
                    AddWayPointIcon(e.X, e.Y, WayPointIcon.Count, 'B');
                    CustomDestWptIndex=WayPointIcon.Count-1;
                    WayPointIcon[CustomDestWptIndex].Visible=true;
                    isCustomDest=true;
                    FindNearestWayPoint(e.X, e.Y, ref DestWptIndex, ref EndWayPointDistance);
                    SelectOrigDest.Enabled=false;
                    BeginSPF.Enabled=true;
                    StatusDisplay.ForeColor=Color.Orange;
                    StatusDisplay.Text="寻路就绪";
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Graphics g = MapDisplay.CreateGraphics();

            if (checkBox1.Checked)
            {
                for (int i = 0; i<WayPointIcon.Count; i++) WayPointIcon[i].Visible=true;
                for (int i = 0; i<_WayPointData.Count; i++)
                    for (int j = 0; j<_WayPointData.Count; j++)
                        if (PathData[i][j]!=INF) DrawPath(g,Color.Red, 1,_WayPointData[i].DrawX, _WayPointData[i].DrawY,
                             _WayPointData[j].DrawX, _WayPointData[j].DrawY);
            }
            else
            {
                for (int i = 0; i<WayPointIcon.Count; i++) WayPointIcon[i].Visible=false;
                MapDisplay.Image=Image.FromFile(Application.StartupPath+@"\uires\map.png");
            }

            GC.Collect();
        }

        private void AboutMe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("【哈工大二校区校内最短路寻找程序】\n\n作者：崔子健(Ernest Cui)\n\n核心算法：朴素Dijkstra算法\n使用框架：C# Winform\n\n"+
                "大事时间表：\nDijkstra算法学习时间：2020.9.19\nGoogle Earth测绘时间：2020.9.21-23\n"+
                "代码开发时间：2020.9.23-25\nRelease版本发布：2020.9.25 15:30\n\n（于轻度感冒中写于军训倒数第三天）");
        }

    }
}
