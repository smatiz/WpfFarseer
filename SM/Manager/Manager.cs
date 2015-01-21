//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SM
//{
//   public abstract class Manager : BasicManager
//    {

//        protected abstract IMaterialCreator GetMaterialCreator();
//        public Manager()
//            : base()
//        {
//            var context = new Context(zoom);
//            _zoom = zoom;
//            _farseerResultContainers = farseerResultContainers;

//            var canvasCreated = new Action<CanvasId>(canvasId => { farseerResultContainers.Children.Add(canvasId); });

//            // creo un wpf views creator che Views pilotera' e usera' per popolare le sue strutture a partire da info
//            var wpfViewsCreator = new WpfViewsCreator(canvasCreated, context);
//            var wpfViewsShapeCreator = new WpfShapeCreator();

//            World world = new World(gravity);
//            // creo un farseer materials creator che Materials pilotera' e usera' per popolare le sue strutture a partire da info
//            var farseerMaterialsCreator = GetMaterialCreator();
//            var farseerMaterialsShapeCreator = new XamlShapeMaterialCreator(context);

//            // Views e' completamente agnostico 
//            var farseerViews = new Views(wpfViewsCreator, wpfViewsShapeCreator);
//            // Materials e' completamente agnostico 
//            var materials = new Materials(farseerMaterialsCreator, farseerMaterialsShapeCreator);

//            // Synchronizers e' la struttura agnostica per tenere sincronizzato views e materials
//            var synchronizers = new Synchronizers(farseerViews, materials);

//            _worldManager = new FarseerWorldManager(id, synchronizers, new WatchView(), world);

//            _stepViewModel = new StepViewModel(_worldManager);
//        }
        
//    }
//}
