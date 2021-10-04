namespace DataAccessLayer.Services
{
    public class NaukriJobDetailsService
    {
        DataAccessContext dataAccessContext = new DataAccessContext();
        public System.Collections.Generic.IAsyncEnumerable<Entity.NaukriJobDetail> GetAllNaukriDetails()
        {
            var AllNaukriDetails = dataAccessContext.NaukriJobDetails.AsAsyncEnumerable();

            return AllNaukriDetails;

        }
    }
}
