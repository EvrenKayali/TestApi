using MoneyBox.Business.Services;
using MoneyBox.DAL;
using System;
using System.Threading.Tasks;

namespace MoneyBox.Business
{
    public class ServiceFactory : IDisposable
    {
        private MoneyBoxDb context = new MoneyBoxDb();
        private CompanyService companyService;
        private AccountService accountService;
        private CategoryService categoryService;
        private BranchService branchService;
        private TransactionService transactionService;
        private UserIdentifierService userIdentifierService;

        public CompanyService CompanyService
        {
            get
            {
                if (this.companyService == null)
                {
                    this.companyService = new CompanyService(context);
                }
                return companyService;
            }
        }

        public AccountService AccountService
        {
            get
            {
                if (this.accountService == null)
                {
                    this.accountService = new AccountService(context);
                }
                return accountService;
            }
        }

        public CategoryService CategoryService
        {
            get
            {
                if (this.categoryService == null)
                {
                    this.categoryService = new CategoryService(context);
                }
                return categoryService;
            }
        }

        public BranchService BranchService
        {
            get
            {
                if (this.branchService == null)
                {
                    this.branchService = new BranchService(context);
                }
                return branchService;
            }
        }

        public UserIdentifierService UserIdentifierService
        {
            get
            {
                if (this.userIdentifierService == null)
                {
                    this.userIdentifierService = new UserIdentifierService(context);
                }
                return userIdentifierService;
            }
        }

        public TransactionService TransactionService
        {
            get
            {
                if (this.transactionService == null)
                {
                    this.transactionService = new TransactionService(context);
                }
                return transactionService;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}