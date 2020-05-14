namespace Dotnet.Expect.Core.Abstractions
{
    using System;

    public class ActionContext
    {
        public ActionContext(IExpectable expectable, ItemsCache itemsCache)
        {
            Expectable = expectable ?? throw new ArgumentNullException(nameof(expectable));
            ItemsCache = itemsCache ?? throw new ArgumentNullException(nameof(itemsCache));
        }

        public IExpectable Expectable { get; }
        public ItemsCache ItemsCache { get; }
    }
}