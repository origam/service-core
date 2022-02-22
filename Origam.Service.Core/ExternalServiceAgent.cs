using System.Collections;

namespace Origam.Service.Core
{
    public abstract class ExternalServiceAgent : IExternalServiceAgent
    {
        public abstract object Result { get; }
        public abstract void Run();

        public virtual Hashtable Parameters { get; } = new Hashtable();
        public virtual string MethodName { get; set; }
        public virtual string TransactionId { get; set; }
    }
}