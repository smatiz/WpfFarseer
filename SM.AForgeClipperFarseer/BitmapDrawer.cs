using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.AForgeClipperFarseer
{
    class BitmapDrawer
    {
        string _path;
        string _name;
        Bitmap _bitmap;
        int i = 1;
        public BitmapDrawer(string path, string name, int w, int h)
        {
            _path = path;
            _name = name;
            _bitmap = new Bitmap(w, h);
            Graphics = Graphics.FromImage(_bitmap);
        }

        public Graphics Graphics { get; private set; }

        public void Commit()
        {
            _bitmap.Save(_path + "_" + _name + "__" + i.ToString() + "__.png", System.Drawing.Imaging.ImageFormat.Png);
            i++;
        }

        public void Save()
        {
            _bitmap.Save(_path + _name + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    class D
    {
        public static Pen Pen(Color color, byte a)
        {
            return new System.Drawing.Pen(Brush(color, a), 2);
        }
        public static Brush Brush(Color color, byte a)
        {
            return new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(a, color.R, color.G, color.B));
        }
        public static Graphics Graphics(string name)
        {
            return BitmapDrawerManager.Instance.GetBitmapDrawer(name).Graphics;
        }
        public static void Commit(string name)
        {
            BitmapDrawerManager.Instance.GetBitmapDrawer(name).Commit();
        }
        public static void Save(string name)
        {
            BitmapDrawerManager.Instance.GetBitmapDrawer(name).Save();
        }
    }

    class BitmapDrawerManager
    {

        #region Singleton
        static BitmapDrawerManager _instance;

        public static string Path
        {
            private get;
            set;
        }
        public static Size Size
        {
            private get;
            set;
        }
       
        public  static BitmapDrawerManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BitmapDrawerManager(Path, Size.Width, Size.Height);
                }
                return _instance;
            }
        }
        #endregion
        string _path;
        int _w, _h;
        Dictionary<string, BitmapDrawer> _map = new Dictionary<string, BitmapDrawer>();
        private BitmapDrawerManager(string path, int w, int h)
        {
            _path = path;
            _w = w;
            _h = h;
        }

        public BitmapDrawer GetBitmapDrawer(string name)
        {
            if (_map.ContainsKey(name))
            {
                return _map[name];
            }
            var bitmapDrawer = new BitmapDrawer(_path, name, _w, _h);
            _map.Add(name, bitmapDrawer);
            return bitmapDrawer;
        }
    }
}
