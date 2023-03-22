using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.ecommerce.Utils.SupportSpecflow
{
    internal class NonDriverAssists
    {
        public static string acquireTestParameter(string parameterName)
        {
            string? parameter;
            parameter = TestContext.Parameters[parameterName];
            //Console.WriteLine(parameter);
            if (parameter is null)
            {
                Console.WriteLine("Mising Context Variables, please include prior to running ");
                Assert.Inconclusive("Test Parameter '" + parameterName + "' not found");
            }
            return parameter;
        }

        public static string acquireEnvironmentParameter(string parameterName) {
            string? parameter;
            parameter = Environment.GetEnvironmentVariable(parameterName);
            //Console.WriteLine(parameter);
            if (parameter is null)
            {
                Console.WriteLine("Mising Context Variables, please include prior to running ");
                Assert.Inconclusive("Environment Parameter '" + parameterName + "' not found");
            }
            return parameter;
        }
    }
}
