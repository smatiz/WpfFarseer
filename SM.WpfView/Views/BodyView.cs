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
    //[ContentPropertyAttribute("Shapes")]
    public class BodyView : BasicView, IBodyView
    {
        protected Canvas _canvas;
        protected RotateTransform _rotation = new RotateTransform();
        protected TranslateTransform _traslation = new TranslateTransform();

        private BodyInfo _bodyInfo;
        //private List<FlagView> _flags = new List<FlagView>();

        // break constructor
        private BodyView(Canvas parentCanvas, BodyView bodyView, string id, PolygonShapeView shape)
            : base( parentCanvas,bodyView.Context, id)
        {
            BodyType = SM.BodyType.Dynamic;
            RotoTranslation = bodyView.RotoTranslation;
            Shapes = new List<BasicShapeView>() { shape };
            _canvas = new Canvas();
        }
        protected static BodyView Create(Canvas parentCanvas, BodyView bodyView, string id, PolygonShapeView shape) { return new BodyView(parentCanvas, bodyView, id, shape); }

        internal BodyView(Canvas parentCanvas, IContext context, BodyInfo bodyInfo, IShapeViewCreator shapeCreator)
            : base( parentCanvas,context, bodyInfo.Id)
        {
            _bodyInfo = bodyInfo;

            BodyType = bodyInfo.BodyType;
            Shapes = shapeCreator.Create(bodyInfo.Shapes, Context, bodyInfo.Transform.Scale);

            RotoTranslation = bodyInfo.Transform.RotoTranslation;
            _canvas = new Canvas();
            foreach (var flag in _bodyInfo.Flags)
            {
                Flags.Add(new FlagView(Context, _canvas, flag));
            }
        }

        protected override void OnFirstLoad()
        {
            var gt = new TransformGroup();
            gt.Children.Add(_rotation);
            gt.Children.Add(_traslation);
            _canvas.RenderTransform = gt;

            int i = 0;
            foreach (var shape in Shapes)
            {
                var drawable = shape as IDrawable;
                if (drawable != null)
                {
                    _canvas.Children.Insert(i++, drawable.UIElement);
                }
            }

            AddChild(_canvas);

            Update();
        }

        public BodyType BodyType { get; set; }
        public IEnumerable<BasicShapeView> Shapes { get; set; }
        public rotoTranslation RotoTranslation { get; set; }
        public override void Update()
        {
            _traslation.X = RotoTranslation.Translation.X * Context.Zoom;
            _traslation.Y = RotoTranslation.Translation.Y * Context.Zoom;
            _rotation.Angle = RotoTranslation.DegreeAngle;
            //foreach(var x in _flags)
            //{
            //    x.Update();
            //}
        }
    }
}
