using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace agency_transfercenter.EntityDtos.PagedAndSortedDtos
{
    public class GetListPagedAndSortedDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
