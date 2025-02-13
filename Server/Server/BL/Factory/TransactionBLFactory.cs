using Server.DAL;
using Server.DALInterfaces;
using Server.Helpers.Service;
using Server.Helpers.ServiceInterfaces;
using Server.ModelsInterfaces.Settings;

namespace Server.BL.Factory
{
    /// <summary>
    /// The class responsible for Implementation of the Factory Design Pattern for the TransactionBL (Business Logic) 
    /// where the factory is responsible for creating an object of type TransactionBL.
    /// </summary>
    public class TransactionBLFactory : IFactory<TransactionBL>
    {
        private readonly ITransactionDAL transactionDAL;
        private readonly ISecurityService securityService;

        // Gets the required dependencies through the constructor.
        public TransactionBLFactory(ITransactionDAL transactionDAL, ISecurityService securityService)
        {
            this.transactionDAL = transactionDAL;
            this.securityService = securityService;
        }

        // Constructs the object and returns it, with all injected dependencies.
        public TransactionBL Create()
        {
            return new TransactionBL(transactionDAL, securityService);
        }
    }
}
