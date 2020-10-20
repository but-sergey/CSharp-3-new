using System;
using System.Threading.Tasks;

using System.Reflection;
using System.Linq;
using System.IO;

namespace TestConsoleCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Assembly asm = Assembly.GetEntryAssembly();

            Type type = asm.GetType("TestConsoleCore.Program");
            Type type2 = asm.GetTypes().First(t => t.Name == "Program");

            var str = "Hello World!";

            var type3 = GetObjectType(str);

            var type_string = typeof(string);

            var test_lib_file = new FileInfo("TestLib.dll");
            var test_lib_assembly = Assembly.LoadFile(test_lib_file.FullName);

            var printer_type = test_lib_assembly.GetType("TestLib.Printer");

            //ConstructorInfo
            //MethodInfo
            //PropertyInfo
            //EventInfo
            //FieldInfo

            foreach (var method in printer_type.GetMethods())
            {
                var return_type = method.ReturnType;
                var parameters = method.GetParameters();

                Console.WriteLine("{0} {1}({2})",
                    return_type.Name,
                    method.Name,
                    string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")));
            }

            object printer = Activator.CreateInstance(printer_type, ">>>");

            var printer_constructor = printer_type.GetConstructor(new[] { typeof(string) });

            var printer2 = printer_constructor.Invoke(new object[] { "<<<" });

            var print_method_info = printer_type.GetMethod("Print");

            print_method_info.Invoke(printer, new object[] { "Hello World!" });

            var prefix_field_info = printer_type.GetField("_Prefix", BindingFlags.NonPublic | BindingFlags.Instance);

            object prefix_value_object = prefix_field_info.GetValue(printer);
            var prefix_value_string = (string)prefix_field_info.GetValue(printer);

            prefix_field_info.SetValue(printer, "123");



            Console.ReadLine();
        }

        private static Type GetObjectType(object obj)
        {
            return obj.GetType();
        }
    }
}
