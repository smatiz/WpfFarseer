using SM;
using SM.Wpf;
using SM.WpfView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SM.WpfView
{
    [ContentPropertyAttribute("Shapes")]
    public class BodyView : BasicView, IBodyView
    {
        protected Canvas _canvas;
        protected RotateTransform _rotation = new RotateTransform();
        protected TranslateTransform _traslation = new TranslateTransform();

        // break constructor
        private BodyView(BodyView bodyView, string id, PolygonShapeView shape)
            : base(bodyView, id)
        {
            BodyType = SM.BodyType.Dynamic;
            RotoTranslation = bodyView.RotoTranslation;
            Shapes = new List<BasicShapeView>() { shape };
        }
        protected static BodyView Create(BodyView bodyView, string id, PolygonShapeView shape) { return new BodyView(bodyView, id, shape); }

        internal BodyView(BasicView parent, BodyInfo body, IShapeViewCreator shapeCreator)
            : base(parent, body.Id)
        {

            foreach (var x in body.Flags)
            {
                new FlagView(parent, x.Id);
            }

            BodyType = body.BodyType;
            Shapes = shapeCreator.Create(body.Shapes, Context);

            RotoTranslation =new rotoTranslation(new float2( body.X * Context.Zoom,body.Y * Context.Zoom),body.Angle);
        }

        protected override void OnFirstLoad()
        {
            _canvas = new Canvas();
            AddChild(_canvas);
            var gt = new TransformGroup();
            gt.Children.Add(_rotation);
            gt.Children.Add(_traslation);
            _canvas.RenderTransform = gt;

            foreach (var shape in Shapes)
            {
                var drawable = shape as IDrawable;
                if (drawable != null)
                {
                    _canvas.Children.Add(drawable.UIElement);
                }
            }
        }

        public BodyType BodyType { get; set; }
        public IEnumerable<BasicShapeView> Shapes { get; set; }
        public rotoTranslation RotoTranslation { get; set; }
        public override void Update()
        {
            _traslation.X = RotoTranslation.Translation.X * Context.Zoom;
            _traslation.Y = RotoTranslation.Translation.Y * Context.Zoom;
            _rotation.Angle = RotoTranslation.DegreeAngle;
        }
    }
}
