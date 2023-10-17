using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using DynVis.Core.IO;
using DynVis.EventsLog;

namespace DynVis.Core.Plugin
{
    public static class Plugin
    {
        /// <summary>
        /// Папка поиска расчетных модулей
        /// </summary>
        public const string PluginFolder = "plugin";

        /// <summary>
        /// Название интерфейса расяета поверхности
        /// </summary>
        public const string IntefaceSurfacePluginName = "IDynVisSurfacePlugin";
        /// <summary>
        /// Название интерфейса расяета пути
        /// </summary>
        public const string IntefacePathPluginName = "IDynVisPathPlugin";
        /// <summary>
        /// Название интерфейса расяета точек
        /// </summary>
        public const string IntefacePointsPluginName = "IDynVisPointsPlugin";

        private static readonly List<IDynVisSurfacePlugin> _SurfacePluginList = new List<IDynVisSurfacePlugin>();
        private static readonly List<IDynVisPathPlugin> _PathPluginList = new List<IDynVisPathPlugin>();
        private static readonly List<IDynVisPointsPlugin> _PointsPluginList = new List<IDynVisPointsPlugin>();

        /// <summary>
        /// Список всех удачно загруженых сборок
        /// </summary>
        private static readonly List<Assembly> LoadedAssembly = new List<Assembly>();

        /// <summary>
        /// Список всех модулей расчета поверхности
        /// </summary>
        public static IEnumerable<IDynVisSurfacePlugin> SurfacePluginList
        {
            get { return _SurfacePluginList; }
        }

        /// <summary>
        /// Список всех модулей расчета пути
        /// </summary>
        public static IEnumerable<IDynVisPathPlugin> PathPluginList
        {
            get { return _PathPluginList; }
        }

        /// <summary>
        /// Список всех модулей расчета точек
        /// </summary>
        public static IEnumerable<IDynVisPointsPlugin> PointsPluginList
        {
            get { return _PointsPluginList; }
        }

        /// <summary>
        /// Загружает все модули
        /// </summary>
        public static void Load()
        {
            var DllList = IOFile.GetDllsFromFolder(PluginFolder);
            
            foreach (var dll in DllList)
            {
                LoadModule(dll.FullName);
            }
        }

        /// <summary>
        /// Загружает все модули в указанной сборке
        /// </summary>
        /// <param name="modulName">Путь к файлу DLL</param>
        /// <returns></returns>
        public static int LoadModule(string modulName)
        {
            var LoadedModuleCount = 0;
            
            try
            {
                var modul = Assembly.LoadFile(modulName);

                if (GetLoadedAssemblyByName(modul.FullName) != null)
                {
                    Log.LogEvent(String.Format("Modules in ({0}) already loaded", modul.GetName()));
                    return LoadedModuleCount;
                }

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                var allTypesInModule = modul.GetExportedTypes();

                LoadedModuleCount += LoadSurfacePlugin(modul, allTypesInModule);
                LoadedModuleCount += LoadPathPlugin(modul, allTypesInModule);
                LoadedModuleCount += LoadPointsPlugin(modul, allTypesInModule);

                if (LoadedModuleCount == 0)
                {
                    Log.LogError(String.Format("Could not find module in {0}", modulName));
                } 
                else
                {
                    LoadedAssembly.Add(modul);
                }
            }
            catch (IOException e)
            {
                Log.LogError(String.Format("Loding module {0} failed. Error message: {1}", modulName, e.Message));
            }

            return LoadedModuleCount;
        }

        /// <summary>
        /// Необхадима для того, чтобы разрешить путь к загружнной сборке.
        /// Например, при десериализации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return GetLoadedAssemblyByName(args.Name);
        }

        static Assembly GetLoadedAssemblyByName(string assemblyName)
        {
            var result = from assembly in LoadedAssembly where assembly.FullName == assemblyName select assembly;

            return result.FirstOrDefault();
        }


        /// <summary>
        /// Загружает модули расчета поврехности
        /// </summary>
        /// <param name="modul">Сборка</param>
        /// <param name="allTypesInModule">Все типы в сборке</param>
        /// <returns>Число удачно загруженых типов</returns>
        private static int LoadSurfacePlugin(Assembly modul, IEnumerable<Type> allTypesInModule)
        {
            return LoadPlugin(modul, allTypesInModule, IntefaceSurfacePluginName, typeof(DynVisSurfacePlugin), _SurfacePluginList, "surface");
        }

        /// <summary>
        /// Загружает модули расчета пути
        /// </summary>
        /// <param name="modul">Сборка</param>
        /// <param name="allTypesInModule">Все типы в сборке</param>
        /// <returns>Число удачно загруженых типов</returns>
        private static int LoadPathPlugin(Assembly modul, IEnumerable<Type> allTypesInModule)
        {
            return LoadPlugin(modul, allTypesInModule, IntefacePathPluginName, typeof(DynVisPathPlugin), _PathPluginList, "path");
        }

        /// <summary>
        /// Загружает модули расчета критических точек
        /// </summary>
        /// <param name="modul">Сборка</param>
        /// <param name="allTypesInModule">Все типы в сборке</param>
        /// <returns>Число удачно загруженых типов</returns>
        private static int LoadPointsPlugin(Assembly modul, IEnumerable<Type> allTypesInModule)
        {
            return LoadPlugin(modul, allTypesInModule, IntefacePointsPluginName, typeof(DynVisPointsPlugin), _PointsPluginList, "points");
        }

        /// <summary>
        /// Загружает указанный плагин
        /// </summary>
        /// <typeparam name="DynVisPluginType"></typeparam>
        /// <param name="modul"></param>
        /// <param name="allTypesInModule"></param>
        /// <param name="IntefacePluginName"></param>
        /// <param name="attributeType"></param>
        /// <param name="pluginList"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static int LoadPlugin<DynVisPluginType>(Assembly modul, IEnumerable<Type> allTypesInModule, string IntefacePluginName, Type attributeType,
                                      ICollection<DynVisPluginType> pluginList, string name) where DynVisPluginType: IDynVisPlugin
        {
            var pluginTypes = from type in allTypesInModule
                              where
                                  type.GetInterface(IntefacePluginName, true) != null &&
                                  type.GetCustomAttributes(attributeType, false).Length > 0
                              select type;

            var LoadedModuleCount = 0;

            foreach (var type in pluginTypes)
            {
                var Result = false;
                try
                {
                    var plugin = modul.CreateInstance(type.FullName);
                    pluginList.Add((DynVisPluginType)plugin);
                    Result = true;
                    LoadedModuleCount++;
                }
                catch (ArgumentException) { }
                Log.LogResult(String.Format("Loding {2} calculation module {0} from {1}", type.FullName, modul.GetName(), name), Result);
            }

            return LoadedModuleCount;
        }
    }

    
}
