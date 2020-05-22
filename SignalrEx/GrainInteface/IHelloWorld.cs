using System;
using System.Threading.Tasks;

namespace GrainInteface
{
    public interface IHelloWorld:Orleans.IGrainWithStringKey
    {
        Task<String> SayHello(string name);
    }
}
