using System;
using System.Collections.Generic;
using Moq;

namespace BadImageFormatExceptionRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to invoke the method causing the exception");
            Console.ReadKey();

            // In a separate method so that we can see that the exception happens during JIT compilation
            Test();

            Console.WriteLine("Done");
        }

        static void Test()
        {
            var headersHandler = new Mock<IHttpHeadersHandler>();
            headersHandler
                .Setup(handler => handler.OnHeader(It.IsAny<Span<byte>>(), It.IsAny<Span<byte>>()))
                .Callback<Span<byte>, Span<byte>>((name, value) =>
                {
                });
            var obj = headersHandler.Object;
        }
    }

    public interface IHttpHeadersHandler
    {
        void OnHeader(Span<byte> name, Span<byte> value);
    }
}