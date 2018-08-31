using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Features.Users
{
    public class UserListModel
    {
        public IEnumerable<UserListItem> Users { get; set; } = new List<UserListItem>();
    }

    public class UserListItem
    {
        [Display(Name = "Id", Order = 1)]
        public long Id { get; set; }

        [Display(Name = "Random", Order = 100)]
        public string RandomId { get; set; } = Guid.NewGuid().ToString();

        [Display(Order = 2)]
        public string FirstName { get; set; }

        [Display(Order = 3)]
        public string LastName { get; set; }
    }
}