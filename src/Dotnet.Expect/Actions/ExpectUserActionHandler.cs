namespace Dotnet.Expect.Actions
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Abstractions;

    public class ExpectUserActionHandler : ActionHandler<ExpectUserAction>
    {
        public override Task Handle(ExpectUserAction request, CancellationToken cancellationToken)
        {
            var context = request.Context;
            var input = request.MaskedInput ? ReadLineMasked() : Console.ReadLine();

            var match = Regex.Match(input ?? string.Empty, request.Pattern);
            if (match.Success)
            {
                context.ItemsCache[request.CacheKey] = match.Value;

                //var type = Type.GetType(request.InputType);
                //if (type == null || !type.IsValueType)
                //{
                //    type = typeof(string);
                //}

                //var converter = TypeDescriptor.GetConverter(type);
                //if (converter.IsValid(match.Value))
                //{
                //    context.ItemsCache[request.CacheKey] = converter.ConvertFromString(match.Value);
                //}
                //else
                //{
                //    context.ItemsCache[request.CacheKey] = match.Value;
                //}
            }

            return Task.CompletedTask;
        }

        public static string ReadLineMasked()
        {
            var pwd = new StringBuilder();
            while (true)
            {
                var i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.Length -= 1;
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000'
                ) // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
                {
                    pwd.Append(i.KeyChar);
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return pwd.ToString();
        }
    }
}