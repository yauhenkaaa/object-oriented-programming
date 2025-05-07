using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GraphicalEditor.Model.Services
{
    public class PluginLoader
    {
        public List<IPlugin> LoadPlugins(string dllPath)
        {
            var plugins = new List<IPlugin>();
            var assembly = Assembly.LoadFrom(dllPath);

            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface)
                {
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    plugins.Add(plugin);
                }
            }
            return plugins;
        }
    }
}