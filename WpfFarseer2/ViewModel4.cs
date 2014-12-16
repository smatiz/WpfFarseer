using SM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using WpfApplicationTelerik._4_treeview;

namespace SMS
{
    public interface Item
    {
        string Name { get; set; }
    }

    public class Node : Item
    {
        public string Name { get; set; }
        public Collection<Item> Children { get; set; }

    }

    public class Leaf : Item
    {
        public string Name { get; set; }
    }

    public class CanvasW
    {
        public Canvas Canvas { get; set; }
        //public List<Item> Children { get { return Canvas.Children.Cast<Canvas>().Select(x => new CanvasW() { Canvas = x }).ToList(); } }

    }


    public class ViewModel4 : NotifyObjectViewer
    {
        public object UserControlChildren { get; set; }
        public object UserControlChildren2 { get; set; }
        public object XML { get; set; }

        public ViewModel4()
        {
           UserControlChildren = new Collection<Item>()
           {
               new Leaf(){ Name= "B1" },
               new Leaf(){ Name= "B2" },
               new Leaf(){ Name= "B3" },
               new Node()
               { 
                   Name= "C1",
                   Children = new Collection<Item>()
                   {
                       new Leaf(){ Name= "C1_B1" },
                       new Leaf(){ Name= "C1_B2" },
                       new Leaf(){ Name= "C1_B3" },
                       new Node()
                       { 
                           Name= "C2",
                           Children = new Collection<Item>()
                           {
                               new Leaf(){ Name= "C2_B1" },
                               new Leaf(){ Name= "C2_B2" },
                               new Leaf(){ Name= "C2_B3" },
                           }
                       }
                   }
               }
           };
           //var u = new UserControl4ForRead();
           //u.Name = "ciao";
           //UserControlChildren2 = ((Grid)(u).Content).Children.Cast<Canvas>().Select(x => x).ToList();

           //XmlDocument xmlDoc = new XmlDocument();
           //xmlDoc.LoadXml(u.WriteXaml());
           //XmlDataProvider provider = new XmlDataProvider() { Document = xmlDoc, XPath = "Header" };
            

          // XML = XElement.Parse(u.WriteXaml()).Nodes().ToArray();
           //XML = provider;
           // XML.ToArray()[0].
           //u.WriteXaml();
            //VisualTreeHelper.
           //XNode x;x.NodeType


           //((Grid)(u).Content).Children.Clear();
        }
    }
}
