using System;

namespace MoneyBox.Business.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Address { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
