namespace CompanyAgreement.Manager
{
    public class ContextManager
    {
        private static Models.Context context;
        private ContextManager()
        {

        }
        public static Models.Context GetContext()
        {
            if (context == null)
                context = new Models.Context();

            return context;
        }
    }
}

