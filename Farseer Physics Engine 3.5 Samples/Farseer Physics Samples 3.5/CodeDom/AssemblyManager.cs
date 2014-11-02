using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Flame.Dlr
{
    public class AssemblyManager 
    {
        Assembly _assembly = null;
        AssemblyName _assemblyName = null;
        string _location = null;
        string _fullname = null;


        void _createAssembly()
        {
            if (_assembly != null) return;
            if(_assemblyName != null)
                _assembly = System.Reflection.Assembly.Load(_assemblyName);
            if (_location != null)
                _assembly = System.Reflection.Assembly.LoadFile(_location);
            if (_fullname != null)
                _assembly = System.Reflection.Assembly.Load(_fullname);
        }
        void _createAssemblyName()
        {
            if (_assemblyName != null) return;

            
            if (_location != null)
                _assemblyName = AssemblyName.GetAssemblyName(_location);
            if (_assembly != null)
            {
                _createLocation();
                _assemblyName = AssemblyName.GetAssemblyName(_location);
            }
            if (_fullname != null)
                _assemblyName = new AssemblyName(_fullname);
        }

        void _createLocation()
        {
            if (_location != null) return;
            if (_assemblyName != null)
            {
                _createAssembly();
            }
            _location = _assembly.Location;
        }

        void _createFullName()
        {
            if (_fullname != null) return;
            if (_assemblyName != null)
                _fullname = _assemblyName.FullName;
            if (_assembly != null)
                _fullname = _assembly.FullName;
            if (_location != null)
            {
                _createAssemblyName();
                _fullname = _assemblyName.FullName;
            }
        }

        public string Location
        {
            get
            {
                _createLocation();
                return _location;
            }
        }

        public Assembly Assembly
        {
            get
            {
                _createAssembly();
                return _assembly;
            }
        }
        public string FullName
        {
            get
            {
                _createFullName();
                return _fullname;
            }
        }

        

        public AssemblyManager() { }
        public static AssemblyManager FromPath(string path) 
        {
            var aw = new AssemblyManager();
            aw._location = path;
            return aw;
        }

        public static AssemblyManager FromFullName(string fullname)
        {
            var aw = new AssemblyManager();
            aw._fullname = fullname;
            return aw;
        }

        public static IEnumerable<Assembly> GetReferenced(Assembly assembly)
        {
            return from name in assembly.GetReferencedAssemblies() select AssemblyManager.FromFullName(name.FullName).Assembly;
        }

        public static AssemblyManager FromAssemblyWrapper(AssemblyName assemblyName)
        {
            var aw = new AssemblyManager();
            aw._assemblyName = assemblyName;
            return aw;
        }

        public static AssemblyManager FromAssembly(Assembly assembly)
        {
            var aw = new AssemblyManager();
            aw._assembly = assembly;
            return aw;
        }


    }
}
