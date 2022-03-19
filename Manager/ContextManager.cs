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
            if (null == context)
            {
                context = new Models.Context();
            }
            return context;
        }
    }
}

